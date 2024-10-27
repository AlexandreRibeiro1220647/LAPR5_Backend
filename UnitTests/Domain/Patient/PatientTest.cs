using Microsoft.AspNetCore.Routing.Tree;
using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Models.Patient;

public class PatientTests {
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        var fullName = new FullName("John Doe");
        var dateOfBirth = new DateOfBirth(new DateOnly(2000, 10, 10));
        var gender = Gender.Male;
        var email = new UserEmail("john.doe@example.com");
        var contactInformation = new ContactInformation(new Phone("912345123"));
        var emergencyContact = new EmergencyContact(new Phone("912345125"));
        var medicalConditions = new MedicalConditions(new List<string>{"medicalcondition"});
        var appointmentHistory = new AppointmentHistory(new List<string>{"appointment"});

        var patient = new Patient(fullName, dateOfBirth, gender, contactInformation, email, medicalConditions, emergencyContact, appointmentHistory);

        Assert.Equal(fullName, patient.fullName);
        Assert.Equal(dateOfBirth, patient.dateOfBirth);
        Assert.Equal(gender, patient.gender);
        Assert.Equal(contactInformation, patient.contactInformation);
        Assert.Equal(email, patient.email);
        Assert.Equal(medicalConditions, patient.medicalConditions);
        Assert.Equal(emergencyContact, patient.emergencyContact);
        Assert.Equal(appointmentHistory, patient.appointmentHistory);
    }

    [Fact]
    public void UpdateEmail_ShouldUpdateEmail()
    {
        var patient = CreatePatient();
        var newEmail = "new.email@example.com";

        patient.UpdateEmail(newEmail);

        Assert.Equal(newEmail, patient.email.Value);
    }

    [Fact]
    public void UpdatePhone_ShouldUpdateContactInformation()
    {
        var patient = CreatePatient();
        var newPhone = "917654356";

        patient.UpdateContactInformation(newPhone);

        Assert.Equal(newPhone, patient.contactInformation.contactInformation.phoneNumber);
    }

    [Fact]
    public void UpdatePhone_ShouldUpdateEmergencyContact()
    {
        var patient = CreatePatient();
        var newPhone = "917654353";

        patient.UpdateEmergencyContact(newPhone);

        Assert.Equal(newPhone, patient.emergencyContact.emergencyContact.phoneNumber);
    }

    private Patient CreatePatient()
    {
        var fullName = new FullName("John Doe");
        var dateOfBirth = new DateOfBirth(new DateOnly(2000, 10, 10));
        var gender = Gender.Male;
        var email = new UserEmail("john.doe@example.com");
        var contactInformation = new ContactInformation(new Phone("912345123"));
        var emergencyContact = new EmergencyContact(new Phone("912345125"));
        var medicalConditions = new MedicalConditions(new List<string>{"medicalcondition"});
        var appointmentHistory = new AppointmentHistory(new List<string>{"appointment"});
        return new Patient(fullName, dateOfBirth, gender, contactInformation, email, medicalConditions, emergencyContact, appointmentHistory);
    }
}