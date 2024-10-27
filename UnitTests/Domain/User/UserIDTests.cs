using System;
using TodoApi.Models;
using Xunit;

public class UserIDTests
{
    [Fact]
    public void Constructor_WithGuid_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var userId = new UserID(guid);

        // Assert
        Assert.Equal(guid, userId.Value);
    }

    [Fact]
    public void Constructor_WithValidString_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();

        // Act
        var userId = new UserID(guid);

        // Assert
        Assert.Equal(guid, userId.AsString());
    }
}
