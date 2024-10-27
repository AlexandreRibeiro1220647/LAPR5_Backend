using System;
using Xunit;
using TodoApi.Models.Staff;

public class SpecializationTests
{
    [Fact]
    public void Constructor_WithValidSpecialization_ShouldSetArea()
    {
        var specialization = new Specialization("Dermatology");

        Assert.Equal("Dermatology", specialization.Area);
    }

    [Fact]
    public void Constructor_WithInvalidSpecialization_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Specialization("InvalidArea"));
    }

    [Fact]
    public void ChangeSpecialization_WithValidArea_ShouldUpdateArea()
    {
        var specialization = new Specialization("Dermatology");
        specialization.ChangeSpecialization("Neurology");

        Assert.Equal("Neurology", specialization.Area);
    }
}
