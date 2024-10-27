using System;
using TodoApi.Models.Shared;
using Xunit;

public class UniqueIDTests
{
    [Fact]
    public void Constructor_WithGuid_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var uniqueId = new UniqueID(guid);

        // Assert
        Assert.Equal(guid, uniqueId.Value);
    }

    [Fact]
    public void Constructor_WithValidString_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();

        // Act
        var uniqueId = new UniqueID(guid);

        // Assert
        Assert.Equal(guid, uniqueId.AsString());
    }

    [Fact]
    public void Equals_SameValues_ShouldReturnTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = new UniqueID(guid);
        var id2 = new UniqueID(guid);

        // Act & Assert
        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
    }

    [Fact]
    public void CompareTo_SameValues_ShouldReturnZero()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = new UniqueID(guid);
        var id2 = new UniqueID(guid);

        // Act & Assert
        Assert.Equal(0, id1.CompareTo(id2));
    }
}
