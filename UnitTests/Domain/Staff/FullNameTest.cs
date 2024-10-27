using Xunit;
using TodoApi.Models.Staff;

public class FullNameTests
{
    [Fact]
    public void Constructor_ShouldSetFullName()
    {
        var fullName = new FullName("John Doe");

        Assert.Equal("John Doe", fullName.fullName);
    }

    [Fact]
    public void ChangeFullName_ShouldUpdateFullName()
    {
        var fullName = new FullName("John Doe");
        fullName.ChangeFullName("Jane Doe");

        Assert.Equal("Jane Doe", fullName.fullName);
    }
}
