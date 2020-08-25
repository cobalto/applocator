using System;
using Xunit;


namespace AppLocator.Domain.Tests
{
    public class Domain_ValueObjects_Test
    {
        [Fact]
        public void New_AppId_Should_Be_An_Int()
        {
            // Arrange
            int id = 1234;

            // Act
            AppId appId = new AppId(id);

            // Assert
            Assert.Equal(1234, appId);
            Assert.Equal("1234", appId.ToString());
        }

        [Fact]
        public void New_AppUrl_Should_Be_A_Absolute_Uri()
        {
            // Arrange
            Uri uri = new Uri("http://www.cobalto.dev");

            // Act
            AppUrl appUrl = new AppUrl(uri);

            // Assert
            Assert.Equal("http://www.cobalto.dev/", appUrl.ToString());
        }

        [Fact]
        public void New_AppPath_Should_Be_A_Valid_Path()
        {
            // Arrange
            string path = Environment.CurrentDirectory;

            // Act
            AppPath appPath = new AppPath(path);

            // Assert
            Assert.Equal(path, appPath.ToString());
        }

        [Fact]
        public void New_AppPath_Should_Raise_An_Exception_For_Invalid_Path()
        {
            // Arrange
            string path = "**__OH+YEAH^SYMBOLS#IN#AN✈️";

            // Act
            Action act = () => new AppPath(path);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Equal($"{path} does not exist", exception.Message);
        }

        [Fact]
        public void New_AppMode_Should_Be_Debugging()
        {
            // Arrange
            AvailableMode mode = AvailableMode.Debug;

            // Act
            AppMode appMode = new AppMode(mode);

            // Assert

            Assert.True(appMode.IsDebugging);
            Assert.Equal(AvailableMode.Debug, appMode);
            Assert.Equal("Debug", appMode.ToString());
        }
    }
}
