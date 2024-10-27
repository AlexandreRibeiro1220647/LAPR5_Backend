using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Models.Patient;

public class DateOfBirthTest {
    public void Constructor_ShouldSetDateOfBirth()
    {
        var dateOfBirth = new DateOfBirth(new DateOnly(2000, 10, 10));

        Assert.Equal("2000-10-10", dateOfBirth.dateOfBirth.ToString());
    }
}