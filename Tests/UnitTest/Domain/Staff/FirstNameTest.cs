using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class FirstNameTests
    {
        [Fact]
        public void Constructor_ValidFirstName_ShouldSetFirstName()
        {
            // Arrange
            var validFirstName = "John";

            // Act
            var firstName = new FirstName(validFirstName);

            // Assert
            Assert.Equal(validFirstName, firstName.firstName);
        }

        [Fact]
        public void Constructor_NullFirstName_ShouldSetToNull()
        {
            // Arrange
            string nullFirstName = null;

            // Act
            var firstName = new FirstName(nullFirstName);

            // Assert
            Assert.Null(firstName.firstName);
        }

        [Fact]
        public void ChangeFirstName_ValidNewFirstName_ShouldUpdateFirstName()
        {
            // Arrange
            var initialFirstName = "John";
            var newFirstName = "Jane";
            var firstName = new FirstName(initialFirstName);

            // Act
            firstName.ChangeFirstName(newFirstName);

            // Assert
            Assert.Equal(newFirstName, firstName.firstName);
        }

        [Fact]
        public void ChangeFirstName_NullNewFirstName_ShouldAllowNullValue()
        {
            // Arrange
            var initialFirstName = "John";
            var firstName = new FirstName(initialFirstName);

            // Act
            firstName.ChangeFirstName(null);

            // Assert
            Assert.Null(firstName.firstName);
        }
    }
}
