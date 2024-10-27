using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class FullNameTests
    {
        [Fact]
        public void Constructor_ValidFullName_ShouldSetFullName()
        {
            // Arrange
            var validFullName = "John Doe";

            // Act
            var fullName = new FullName(validFullName);

            // Assert
            Assert.Equal(validFullName, fullName.fullName);
        }

        [Fact]
        public void Constructor_NullFullName_ShouldSetToNull()
        {
            // Arrange
            string nullFullName = null;

            // Act
            var fullName = new FullName(nullFullName);

            // Assert
            Assert.Null(fullName.fullName);
        }

        [Fact]
        public void ChangeFullName_ValidNewFullName_ShouldUpdateFullName()
        {
            // Arrange
            var initialFullName = "John Doe";
            var newFullName = "Jane Smith";
            var fullName = new FullName(initialFullName);

            // Act
            fullName.ChangeFullName(newFullName);

            // Assert
            Assert.Equal(newFullName, fullName.fullName);
        }

        [Fact]
        public void ChangeFullName_NullNewFullName_ShouldAllowNullValue()
        {
            // Arrange
            var initialFullName = "John Doe";
            var fullName = new FullName(initialFullName);

            // Act
            fullName.ChangeFullName(null);

            // Assert
            Assert.Null(fullName.fullName);
        }
    }
}
