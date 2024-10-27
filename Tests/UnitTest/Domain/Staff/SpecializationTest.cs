using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class SpecializationTests
    {
        [Theory]
        [InlineData("Dermatology")]
        [InlineData("Neurology")]
        [InlineData("Cardiology")]
        [InlineData("Orthopedics")]
        public void Constructor_ValidSpecialization_ShouldSetArea(string validArea)
        {
            // Act
            var specialization = new Specialization(validArea);

            // Assert
            Assert.Equal(validArea, specialization.Area);
        }

        [Theory]
        [InlineData("Pediatrics")]
        [InlineData("Oncology")]
        [InlineData("")]
        public void Constructor_InvalidSpecialization_ShouldThrowArgumentException(string invalidArea)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Specialization(invalidArea));
        }

        [Theory]
        [InlineData("Dermatology")]
        [InlineData("Neurology")]
        public void ChangeSpecialization_ValidSpecialization_ShouldUpdateArea(string validArea)
        {
            // Arrange
            var specialization = new Specialization("Dermatology");

            // Act
            specialization.ChangeSpecialization(validArea);

            // Assert
            Assert.Equal(validArea, specialization.Area);
        }

        [Theory]
        [InlineData("Pediatrics")]
        [InlineData("Oncology")]
        [InlineData("")]
        public void ChangeSpecialization_InvalidSpecialization_ShouldThrowArgumentException(string invalidArea)
        {
            // Arrange
            var specialization = new Specialization("Dermatology");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => specialization.ChangeSpecialization(invalidArea));
        }
    }
}
