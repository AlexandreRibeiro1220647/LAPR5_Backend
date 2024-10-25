using TodoApi.Models;

namespace TodoApi.DTOs;

public class PatientDTO {
    public string FullName { get;  set; }
    public string BirthDate { get;  set; }
    public string Gender { get;  set; }
    public string MedicalRecordNumber { get;  set; }
    public string ContactInformation { get;  set; }
    public string Email { get;  set; }
    public List<string> MedicalConditions { get;  set; }
    public string EmergencyContact { get;  set; }
    public List<string> AppointmentHistory { get;  set; }

    public PatientDTO() { }

    public PatientDTO(string fullName, string birthDate, string gender, string medicalRecordNumber, string contactInformation, string email, List<string> medicalConditions, string emergencyContact, List<string> appointmentHistory) {
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        MedicalRecordNumber = medicalRecordNumber;
        ContactInformation = contactInformation;
        Email = email;
        MedicalConditions = medicalConditions;
        EmergencyContact = emergencyContact;
        AppointmentHistory = appointmentHistory;
    }
}