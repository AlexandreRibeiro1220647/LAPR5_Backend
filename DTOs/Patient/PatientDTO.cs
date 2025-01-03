using TodoApi.Models;
using TodoApi.Models.Patient;

namespace TodoApi.DTOs;

public class PatientDTO {
    public string DateOfBirth { get;  set; }
    public string Gender { get;  set; }
    public string MedicalRecordNumber { get;  set; }
    public string ContactInformation { get;  set; }
    public MedicalRecord MedicalRecord { get; set; }
    public string EmergencyContact { get;  set; }
    public List<string> AppointmentHistory { get;  set; }
    public TodoApi.DTOs.User.UserDTO User {get; set; }

    public PatientDTO() { }

    public PatientDTO(string dateOfBirth, string gender, string medicalRecordNumber, string contactInformation, MedicalRecord medicalRecord, string emergencyContact, List<string> appointmentHistory, TodoApi.DTOs.User.UserDTO user) {
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MedicalRecordNumber = medicalRecordNumber;
        ContactInformation = contactInformation;
        MedicalRecord = medicalRecord;
        EmergencyContact = emergencyContact;
        AppointmentHistory = appointmentHistory;
        User = user;
    }
}