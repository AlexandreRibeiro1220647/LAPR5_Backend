using System;
using TodoApi.Models.Shared;
using Xunit;

public class PhoneTests
{
    [Fact]
    public void Constructor_WithValidPhoneNumber_ShouldSetPhoneNumber()
    {
        // Arrange
        var phoneNumber = "912345678";

        // Act
        var phone = new Phone(phoneNumber);

        // Assert
        Assert.Equal(phoneNumber, phone.phoneNumber);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_WithNullOrEmptyPhoneNumber_ShouldThrowArgumentNullException(string phoneNumber)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Phone(phoneNumber));
    }

    [Theory]
    [InlineData("800123456")] // Invalid number
    [InlineData("35191234567")] // Not enough digits
    [InlineData("1234567890")] // Invalid start
    public void Constructor_WithInvalidPhoneNumberFormat_ShouldThrowArgumentException(string phoneNumber)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Phone(phoneNumber));
    }
}
