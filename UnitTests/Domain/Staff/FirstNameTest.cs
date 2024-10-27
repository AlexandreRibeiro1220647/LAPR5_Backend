using Xunit;
using TodoApi.Models.Staff;

public class FirstNameTests
{
    [Fact]
    public void Constructor_ShouldSetFirstName()
    {
        var firstName = new FirstName("John");

        Assert.Equal("John", firstName.firstName);
    }

    [Fact]
    public void ChangeFirstName_ShouldUpdateFirstName()
    {
        var firstName = new FirstName("John");
        firstName.ChangeFirstName("Jane");

        Assert.Equal("Jane", firstName.firstName);
    }
}
