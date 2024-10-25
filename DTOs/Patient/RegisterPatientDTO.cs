using TodoApi.Models;

namespace TodoApi.DTOs;

public class RegisterPatientDTO {
    public string? Username { get; set; }
    public string FullName { get;  set; }
    public string DateOfBirth { get;  set; }
    public string Gender { get;  set; }
    public string ContactInformation { get;  set; }
    public string Email { get;  set; }
    public List<string> MedicalConditions { get;  set; }
    public string EmergencyContact { get;  set; }
    public List<string> AppointmentHistory { get;  set; }
}