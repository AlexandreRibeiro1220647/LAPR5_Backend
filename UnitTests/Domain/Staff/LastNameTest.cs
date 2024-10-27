using Xunit;
using TodoApi.Models.Staff;

public class LastNameTests
{
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
        lastName.changeLastName("Smith");

        Assert.Equal("Smith", lastName.lastName);
    }
}
