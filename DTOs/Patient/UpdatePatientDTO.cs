namespace TodoApi.DTOs;

public class UpdatePatientDTO {
    public string FullName { get; set; }
    public string ContactInformation { get; set; }
    public string Email { get; set; }
    public string MedicalConditions { get; set; }
    public string EmergencyContact { get; set; }
}