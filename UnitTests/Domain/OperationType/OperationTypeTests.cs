/*using System;
using System.Collections.Generic;
using TodoApi.Models.OperationType;
using Xunit;

public class OperationTypeTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var name = "Surgery";
        var requiredStaff = new List<string> { "Surgeon", "Nurse" };
        var duration = TimeSpan.FromHours(2);

        // Act
        var operationType = new OperationType(name, requiredStaff, duration);

        // Assert
        Assert.Equal(name, operationType.Name);
        Assert.Equal(requiredStaff, operationType.RequiredStaffBySpecialization);
        Assert.Equal(duration, operationType.EstimatedDuration);
        Assert.True(operationType.IsActive);
        Assert.NotNull(operationType.Id); // Ensure ID is generated
    }

    [Fact]
    public void Constructor_WithoutParameters_ShouldNotSetProperties()
    {
        // Act
        var operationType = new OperationType();

        // Assert
        Assert.Null(operationType.Name);
        Assert.Null(operationType.RequiredStaffBySpecialization);
        Assert.Equal(TimeSpan.Zero, operationType.EstimatedDuration); // Default value
        Assert.True(operationType.IsActive); // Default value is true
        Assert.Null(operationType.Id); // Ensure ID is not set by default
    }

    [Fact]
    public void Constructor_WithEmptyStaffList_ShouldSetRequiredStaffBySpecializationToEmptyList()
    {
        // Arrange
        var name = "Consultation";
        var requiredStaff = new List<string>(); // Empty staff list
        var duration = TimeSpan.FromMinutes(30);

        // Act
        var operationType = new OperationType(name, requiredStaff, duration);

        // Assert
        Assert.Empty(operationType.RequiredStaffBySpecialization);
        Assert.Equal(name, operationType.Name);
        Assert.Equal(duration, operationType.EstimatedDuration);
    }

    [Fact]
    public void Constructor_WithNegativeDuration_ShouldThrowArgumentException()
    {
        // Arrange
        var name = "Physical Therapy";
        var requiredStaff = new List<string> { "Therapist" };
        var negativeDuration = TimeSpan.FromHours(-1);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new OperationType(name, requiredStaff, negativeDuration));
    }

    [Fact]
    public void IsActive_DefaultValue_ShouldBeTrue()
    {
        // Act
        var operationType = new OperationType();

        // Assert
        Assert.True(operationType.IsActive);
    }
}
*/