namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class MedicalConditions {
    public List<String> medicalConditions { get; private set; }
    
    public MedicalConditions(List<String> medicalConditions) {
        this.medicalConditions = medicalConditions;
    }
    
    public void AddMedicalCondition(String medicalCondition) {
        this.medicalConditions.Add(medicalCondition);
    }
}