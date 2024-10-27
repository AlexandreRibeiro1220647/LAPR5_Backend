using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class LicenseNumberTests
    {
        [Fact]
        public void Constructor_ValidValue_ShouldSetValue()
        {
            // Arrange
            var validLicenseNumber = "12345";

            // Act
            var licenseNumber = new LicenseNumber(validLicenseNumber);

            // Assert
            Assert.Equal(validLicenseNumber, licenseNumber.Value);
        }

        [Fact]
        public void Constructor_EmptyValue_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyValue = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LicenseNumber(emptyValue));
        }

        [Fact]
        public void Constructor_NullValue_ShouldThrowArgumentException()
        {
            // Arrange
            string nullValue = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LicenseNumber(nullValue));
        }

        [Fact]
        public void DefaultConstructor_ShouldGenerateNewGuid()
        {
            // Act
            var licenseNumber = new LicenseNumber();

            // Assert
            Assert.False(string.IsNullOrEmpty(licenseNumber.Value));
            Guid guidValue;
            Assert.True(Guid.TryParse(licenseNumber.Value, out guidValue));
        }

        [Fact]
        public void AsString_ShouldReturnLicenseNumberValue()
        {
            // Arrange
            var validLicenseNumber = "12345";
            var licenseNumber = new LicenseNumber(validLicenseNumber);

            // Act
            var result = licenseNumber.AsString();

            // Assert
            Assert.Equal(validLicenseNumber, result);
        }
    }
}
