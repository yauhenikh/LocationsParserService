using LocationsParser.Services;
using LocationsParser.Services.Entities;
using System;
using Xunit;

namespace LocationsParser.Tests
{
    public class LocationStringParserServiceTests
    {
        [Theory]
        [InlineData("Rooms Minsk, Z29", "Minsk")]
        [InlineData("Rooms Zurich", "Zurich")]
        [InlineData("Rooms ", "")]
        public void GetCityName_Test(string locationName, string expected)
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = locationName
            };

            // Act
            var result = LocationStringParserService.GetCityName(locationDTO);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Rooms")]
        [InlineData("Test")]
        [InlineData("Minsk Z29")]
        public void GetCityName_Test_ThrowsArgumentException(string locationName)
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = locationName
            };

            // Act
            Action result = () => LocationStringParserService.GetCityName(locationDTO);

            // Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Fact]
        public void GetCityName_Test_ThrowsNullReferenceException()
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = null
            };

            // Act
            Action result = () => LocationStringParserService.GetCityName(locationDTO);

            // Assert
            Assert.Throws<NullReferenceException>(result);
        }

        [Theory]
        [InlineData("Rooms Minsk, Z29", "Z29")]
        [InlineData("Rooms Zurich", null)]
        [InlineData("Rooms ", null)]
        public void GetOfficeName_Test(string locationName, string expected)
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = locationName
            };

            // Act
            var result = LocationStringParserService.GetOfficeName(locationDTO);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Rooms Minsk,Z29")]
        [InlineData("Rooms,")]
        public void GetOfficeName_Test_ThrowsArgumentException(string locationName)
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = locationName
            };

            // Act
            Action result = () => LocationStringParserService.GetOfficeName(locationDTO);

            // Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Fact]
        public void GetOfficeName_Test_ThrowsNullReferenceException()
        {
            // Arrange
            var locationDTO = new LocationDTO
            {
                Name = null
            };

            // Act
            Action result = () => LocationStringParserService.GetOfficeName(locationDTO);

            // Assert
            Assert.Throws<NullReferenceException>(result);
        }
    }
}
