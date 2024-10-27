using System;
using TodoApi.Models;
using TodoApi.Models.User;
using Xunit;

public class UserTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var email = new UserEmail("test@example.com");
        var name = "Test User";
        var role = UserRoles.Admin;

        // Act
        var user = new User(email, name, role);

        // Assert
        Assert.Equal(email, user.Email);
        Assert.Equal(name, user.Name);
        Assert.Equal(role, user.Role);
        Assert.NotNull(user.Id); // Ensure ID is generated
    }

    [Fact]
    public void Constructor_WithNullOrEmptyName_ShouldThrowArgumentException()
    {
        // Arrange
        var email = new UserEmail("test@example.com");
        var role = UserRoles.Admin;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new User(email, null, role));
        Assert.Throws<ArgumentException>(() => new User(email, "", role));
        Assert.Throws<ArgumentException>(() => new User(email, " ", role));
    }

    [Fact]
    public void Constructor_WithoutParameters_ShouldNotSetProperties()
    {
        // Act
        var user = new User();

        // Assert
        Assert.Null(user.Email);
        Assert.Null(user.Name);
        Assert.Equal(default, user.Role);
        Assert.Null(user.Id); // Ensure ID is not set by default
    }

    [Fact]
    public void Constructor_WithValidEmail_ShouldSetEmailCorrectly()
    {
        // Arrange
        var email = new UserEmail("valid@example.com");
        var name = "Valid User";
        var role = UserRoles.Admin;

        // Act
        var user = new User(email, name, role);

        // Assert
        Assert.Equal(email, user.Email);
    }
}
