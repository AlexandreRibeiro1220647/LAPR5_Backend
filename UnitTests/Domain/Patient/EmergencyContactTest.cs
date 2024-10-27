using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Models.Patient;

public class EmergencyContactTest {
    [Fact]
    public void Constructor_ShouldSetEmergencyContact()
    {
        var emergencyContact = new EmergencyContact(new Phone("911234567"));

        Assert.Equal("911234567", emergencyContact.emergencyContact.phoneNumber);
    }

    [Fact]
    public void ChangeFirstName_ShouldUpdateEmergencyContact()
    {
        var emergencyContact = new EmergencyContact(new Phone("911234567"));
        emergencyContact.ChangeEmergencyContact(new Phone("911234565"));

        Assert.Equal("911234565", emergencyContact.emergencyContact.phoneNumber);
    }
}