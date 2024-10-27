using System;
using TodoApi.Models;
using Xunit;

public class UserEmailTests
{
    [Fact]
    public void Constructor_WithValidEmail_ShouldSetEmailValue()
    {
        // Arrange
        var email = "test@example.com";

        // Act
        var userEmail = new UserEmail(email);

        // Assert
        Assert.Equal(email, userEmail.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_WithNullOrEmptyEmail_ShouldThrowArgumentNullException(string email)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new UserEmail(email));
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("user@domain")]
    [InlineData("user@domain.")]
    public void Constructor_WithInvalidEmailFormat_ShouldThrowArgumentException(string email)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new UserEmail(email));
    }

    [Fact]
    public void Constructor_WithoutParameters_ShouldNotSetValue()
    {
        // Act
        var userEmail = new UserEmail();

        // Assert
        Assert.Null(userEmail.Value);
    }
}
