using System;
using Xunit;

namespace AppLocator.Domain.Tests
{
    public class Domain_Application_Test
    {
        [Fact]
        public void New_Application_Should_Be_Fully_Set()
        {
            // Arrange
            AppId id = new AppId(27);
            AppUrl url = new AppUrl(new Uri("http://www.cobalto.dev"));
            AppPath path = new AppPath(Environment.CurrentDirectory);
            AppMode mode = new AppMode(AvailableMode.Debug);

            // Act
            var app = new Application(id, url, path, mode);

            // Assert

            Assert.Equal("27", app.ApplicationId.ToString());
            Assert.Equal("http://www.cobalto.dev/", app.Url.ToString());
            Assert.Equal(Environment.CurrentDirectory, app.Path.ToString());
            Assert.True(app.Mode.IsDebugging);
        }
    }
}
