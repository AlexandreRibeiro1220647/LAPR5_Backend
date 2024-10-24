public class PatientRegistrationDto
{
    public string? Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public List<string> MedicalConditions { get; set; } = new List<string>();
    public string EmergencyContact { get; set; }
}
