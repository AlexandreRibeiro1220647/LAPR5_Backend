using Microsoft.AspNetCore.Routing.Tree;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class Patient : Entity<MedicalRecordNumber> {
    public DateOfBirth dateOfBirth { get; private set; }
    public Gender gender { get; private set; }
    public ContactInformation contactInformation { get; private set; }
    public MedicalRecord medicalRecord { get; private set; }
    public EmergencyContact emergencyContact { get; private set; }
    public AppointmentHistory appointmentHistory { get; private set; }
    public TodoApi.DTOs.User.UserDTO user {get; private set; }


    public Patient() { }

    public Patient(DateOfBirth dateOfBirth, Gender gender, ContactInformation contactInformation, MedicalRecord medicalRecord, EmergencyContact emergencyContact, AppointmentHistory appointmentHistory, TodoApi.DTOs.User.UserDTO user) {
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        Id = new MedicalRecordNumber(Guid.NewGuid());
        this.contactInformation = contactInformation;
        this.medicalRecord = medicalRecord;
        this.emergencyContact = emergencyContact;
        this.appointmentHistory = appointmentHistory;
        this.user = user;
    }

    public Patient(DateOfBirth dateOfBirth, Gender gender, MedicalRecordNumber medicalRecordNumber, ContactInformation contactInformation, MedicalRecord medicalRecord, EmergencyContact emergencyContact, AppointmentHistory appointmentHistory, TodoApi.DTOs.User.UserDTO user) {
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        Id = medicalRecordNumber;
        this.contactInformation = contactInformation;
        this.medicalRecord = medicalRecord;
        this.emergencyContact = emergencyContact;
        this.appointmentHistory = appointmentHistory;
        this.user = user;
    }

    public void UpdateFullName(string fullName) {
        this.user.Name = fullName;
    }

    public void UpdateContactInformation(string contact) {
        this.contactInformation = new ContactInformation(new Phone(contact));
    }

    public void UpdateEmergencyContact(string emergencyContact) {
        this.emergencyContact = new EmergencyContact(new Phone(emergencyContact));
    }

    public void UpdateEmail(string email) {
        this.user.Email = new UserEmail(email);
    }

    public void UpdateMedicalConditions(string medicalConditions) {
        this.medicalConditions = new MedicalConditions(medicalConditions.Split(',').ToList());
    }
}