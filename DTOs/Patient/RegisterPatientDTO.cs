using TodoApi.Models;
using TodoApi.Models.Patient;

namespace TodoApi.DTOs;

public class RegisterPatientDTO {
    public string FullName { get;  set; }
    public string DateOfBirth { get;  set; }
    public string Gender { get;  set; }
    public string ContactInformation { get;  set; }
    public string Email { get;  set; }
    public MedicalRecord MedicalRecord { get;  set; }
    public string EmergencyContact { get;  set; }
    public List<string> AppointmentHistory { get;  set; }
}