using System.Text.Json.Serialization;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using Xunit;
public class MedicalRecordNumberTests {

    [Fact]
    public void Constructor_WithGuid_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var medicalRecordNumber = new MedicalRecordNumber(guid);

        // Assert
        Assert.Equal(guid, medicalRecordNumber.Value);
    }

    [Fact]
    public void Constructor_WithValidString_ShouldSetValueCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();

        // Act
        var medicalRecordNumber = new MedicalRecordNumber(guid);

        // Assert
        Assert.Equal(guid, medicalRecordNumber.AsString());
    }
}