using Microsoft.AspNetCore.Routing.Tree;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class Patient : Entity<MedicalRecordNumber> {
    public FullName fullName { get; private set; }
    public DateOfBirth dateOfBirth { get; private set; }
    public Gender gender { get; private set; }
    public ContactInformation contactInformation { get; private set; }
    public UserEmail email { get; private set; }
    public MedicalConditions medicalConditions { get; private set; }
    public EmergencyContact emergencyContact { get; private set; }
    public AppointmentHistory appointmentHistory { get; private set; }


    public Patient() { }

    public Patient(FullName fullName, DateOfBirth dateOfBirth, Gender gender, ContactInformation contactInformation, UserEmail email, MedicalConditions medicalConditions, EmergencyContact emergencyContact, AppointmentHistory appointmentHistory) {
        this.fullName = fullName;
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        Id = new MedicalRecordNumber(Guid.NewGuid());
        this.contactInformation = contactInformation;
        this.email = email;
        this.medicalConditions = medicalConditions;
        this.emergencyContact = emergencyContact;
        this.appointmentHistory = appointmentHistory;
    }

    public Patient(FullName fullName, DateOfBirth dateOfBirth, Gender gender, MedicalRecordNumber medicalRecordNumber, ContactInformation contactInformation, UserEmail email, MedicalConditions medicalConditions, EmergencyContact emergencyContact, AppointmentHistory appointmentHistory) {
        this.fullName = fullName;
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        Id = medicalRecordNumber;
        this.contactInformation = contactInformation;
        this.email = email;
        this.medicalConditions = medicalConditions;
        this.emergencyContact = emergencyContact;
        this.appointmentHistory = appointmentHistory;
    }

    public void UpdateFullName(string fullName) {
        this.fullName = new FullName(fullName);
    }

    public void UpdateContactInformation(string contact) {
        this.contactInformation = new ContactInformation(new Phone(contact));
    }

    public void UpdateEmergencyContact(string emergencyContact) {
        this.emergencyContact = new EmergencyContact(new Phone(emergencyContact));
    }

    public void UpdateEmail(string email) {
        this.email = new UserEmail(email);
    }

    public void UpdateMedicalConditions(string medicalConditions) {
        this.medicalConditions = new MedicalConditions(medicalConditions.Split(',').ToList());
    }
}