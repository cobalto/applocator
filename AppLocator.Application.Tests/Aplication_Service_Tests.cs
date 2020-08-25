using System;
using Xunit;
using Moq;
using AppLocator.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using AppLocator.Application.Repositories;
using System.Collections.Generic;
using AutoMapper;

namespace AppLocator.Application.Tests
{
    public class Aplication_Service_Tests
    {
        IApplicationService _appService;

        public Aplication_Service_Tests()
        {
            var service = new ServiceCollection();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
                //cfg.AddProfile(new InfrastructureProfile());
            });

            IMapper mapper = config.CreateMapper();
            service.AddSingleton(mapper);

            service.AddTransient<IApplicationService, ApplicationService>();

            service.AddTransient(_ =>
            {
                Mock<IApplicationRepository> mockClient = new Mock<IApplicationRepository>();

                mockClient.Setup(x => x.GetApplicationList()).ReturnsAsync(new List<Domain.Application>()
                {
                    new Domain.Application(new Domain.AppId(1), 
                        new Domain.AppUrl(new Uri("http://wwww.mynumberis1.com/")), 
                        new Domain.AppPath(Environment.CurrentDirectory), 
                        new Domain.AppMode(Domain.AvailableMode.Debug)),
                    new Domain.Application(new Domain.AppId(2), 
                        new Domain.AppUrl(new Uri("http://wwww.mynumberis2.com/")), 
                        new Domain.AppPath(Environment.CurrentDirectory), 
                        new Domain.AppMode(Domain.AvailableMode.Debug)),
                    new Domain.Application(new Domain.AppId(3), 
                        new Domain.AppUrl(new Uri("http://wwww.mynumberis3.com/")), 
                        new Domain.AppPath(Environment.CurrentDirectory), 
                        new Domain.AppMode(Domain.AvailableMode.Release))
                });

                mockClient.Setup(x => x.GetApplication(It.IsAny<int>())).ReturnsAsync((int numb) => new Domain.Application(new Domain.AppId(numb), 
                    new Domain.AppUrl(new Uri($"http://wwww.mynumberis{numb}.com/")), 
                    new Domain.AppPath(Environment.CurrentDirectory), 
                    new Domain.AppMode(Domain.AvailableMode.Debug)));

                mockClient.Setup(x => x.SaveApplication(It.IsAny<Domain.Application>())).ReturnsAsync((Domain.Application app) => app);

                mockClient.Setup(x => x.UpdateApplication(It.IsAny<int>(), It.IsAny<Domain.Application>())).ReturnsAsync((int numb, Domain.Application app) => app);

                mockClient.Setup(x => x.DeleteApplication(It.IsAny<int>()));

                return mockClient.Object;
            });

            var provider = service.BuildServiceProvider();
            _appService = provider.GetService<IApplicationService>();
        }

        [Fact]
        public async void Should_Get_A_List_Of_Applications()
        {
            // Arrange

            // Act
            var appList = await _appService.GetApplicationList();

            // Assert
            Assert.Equal(3, appList.Count);
            Assert.Contains("2", appList[1].Url);
            Assert.False(appList[2].DebuggingMode);
        }

        [Theory]
        [InlineData(27)]
        [InlineData(42)]
        [InlineData(999)]
        public async void Should_Get_An_Application_With_Selected_Id(int appId)
        {
            // Arrange

            // Act
            var appResult = await _appService.GetApplication(appId);

            // Assert
            Assert.Equal(appId, appResult.Application);
            Assert.Contains(appId.ToString(), appResult.Url);
        }

        [Fact]
        public async void Should_Return_The_Application_After_Saving()
        {
            // Arrange
            var app = new Models.ApplicationModel()
            {
                Application = 1,
                Url = "http://wwww.mynumberis27.com/",
                PathLocal = Environment.CurrentDirectory,
                DebuggingMode = true
            };

            // Act
            var appResult = await _appService.SaveApplication(app);

            // Assert
            Assert.Equal(app.Application, appResult.Application);
            Assert.Equal(app.Url, appResult.Url);
            Assert.Equal(app.PathLocal, appResult.PathLocal);
            Assert.Equal(app.DebuggingMode, appResult.DebuggingMode);
        }

        [Fact]
        public async void Should_Return_The_Application_After_Updating()
        {
            // Arrange
            int applicationUpdateId = 27;
            var app = new Models.ApplicationModel()
            {
                Application = 1,
                Url = "http://wwww.mynumberis27.com/",
                PathLocal = Environment.CurrentDirectory,
                DebuggingMode = true
            };

            // Act
            var appResult = await _appService.UpdateApplication(applicationUpdateId, 
                new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Models.ApplicationModel>());

            // Assert
            Assert.Equal(applicationUpdateId, appResult.Application);
            Assert.Equal(app.Url, appResult.Url);
            Assert.Equal(app.PathLocal, appResult.PathLocal);
            Assert.Equal(app.DebuggingMode, appResult.DebuggingMode);
        }
    }
}
