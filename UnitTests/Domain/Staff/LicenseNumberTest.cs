using System;
using Xunit;
using TodoApi.Models.Staff;

public class LicenseNumberTests
{
    [Fact]
    public void Constructor_ShouldSetLicenseNumber()
    {
        var licenseNumber = new LicenseNumber("ABC123");

        Assert.Equal("ABC123", licenseNumber.Value);
    }

    [Fact]
    public void Constructor_WithEmptyValue_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new LicenseNumber(string.Empty));
    }

    [Fact]
    public void DefaultConstructor_ShouldGenerateUniqueId()
    {
        var licenseNumber = new LicenseNumber();

        Assert.False(string.IsNullOrEmpty(licenseNumber.Value));
    }
}
