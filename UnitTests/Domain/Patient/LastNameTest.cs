namespace TodoApi.Models.Patient;
using Xunit;

using TodoApi.Models.Shared;

public class LastNameTests {
    [Fact]
    public void Constructor_ShouldSetLastName()
    {
        var lastName = new LastName("Doe");

        Assert.Equal("Doe", lastName.lastName);
    }

    [Fact]
    public void ChangeLastName_ShouldUpdateLastName()
    {
        var lastName = new LastName("Doe");
        lastName.ChangeLastName("Smith");

        Assert.Equal("Smith", lastName.lastName);
    }
}