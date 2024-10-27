using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class LastNameTests
    {
        [Fact]
        public void Constructor_ValidLastName_ShouldSetLastName()
        {
            // Arrange
            var validLastName = "Silva";

            // Act
            var lastName = new LastName(validLastName);

            // Assert
            Assert.Equal(validLastName, lastName.lastName);
        }

        [Fact]
        public void Constructor_NullLastName_ShouldSetToNull()
        {
            // Arrange
            string nullLastName = null;

            // Act
            var lastName = new LastName(nullLastName);

            // Assert
            Assert.Null(lastName.lastName);
        }

        [Fact]
        public void ChangeLastName_ValidNewLastName_ShouldUpdateLastName()
        {
            // Arrange
            var initialLastName = "Silva";
            var newLastName = "Costa";
            var lastName = new LastName(initialLastName);

            // Act
            lastName.changeLastName(newLastName);

            // Assert
            Assert.Equal(newLastName, lastName.lastName);
        }

        [Fact]
        public void ChangeLastName_NullNewLastName_ShouldAllowNullValue()
        {
            // Arrange
            var initialLastName = "Silva";
            var lastName = new LastName(initialLastName);

            // Act
            lastName.changeLastName(null);

            // Assert
            Assert.Null(lastName.lastName);
        }
    }
}
