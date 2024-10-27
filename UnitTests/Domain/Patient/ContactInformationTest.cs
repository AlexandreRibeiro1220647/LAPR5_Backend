using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Models.Patient;

public class ContactInformationTest {
    [Fact]
    public void Constructor_ShouldSetContactInformation()
    {
        var contactInformation = new ContactInformation(new Phone("911234567"));

        Assert.Equal("911234567", contactInformation.contactInformation.phoneNumber);
    }

    [Fact]
    public void ChangeFirstName_ShouldUpdateContactInformation()
    {
        var contactInformation = new ContactInformation(new Phone("911234567"));
        contactInformation.ChangeContactInformation(new Phone("911234565"));

        Assert.Equal("911234565", contactInformation.contactInformation.phoneNumber);
    }
}