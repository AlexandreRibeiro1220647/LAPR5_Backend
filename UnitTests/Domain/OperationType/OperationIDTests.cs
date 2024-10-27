using System;
using TodoApi.Models.OperationType;
using Xunit;

public class OperationTypeIDTests
{
    [Fact]
    public void Constructor_WithGuid_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var operationId = new OperationTypeID(guid);

        // Assert
        Assert.Equal(guid, operationId.Value);
    }

    [Fact]
    public void Constructor_WithValidString_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();

        // Act
        var operationId = new OperationTypeID(guid);

        // Assert
        Assert.Equal(guid, operationId.AsString());
    }

}
