namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class MedicalRecord {
    public List<Allergy> Allergies { get; private set; }
    public List<MedicalCondition> MedicalConditions { get; private set; }

    public MedicalRecord() {}
    
    public MedicalRecord(List<Allergy> allergies, List<MedicalCondition> medicalConditions) {
        this.Allergies = allergies;
        this.MedicalConditions = medicalConditions;
    }
    
    public void AddMedicalCondition(MedicalCondition medicalCondition) {
        this.MedicalConditions.Add(medicalCondition);
    }

    public void AddAlergy(Allergy allergie) {
        this.Allergies.Add(allergie);
    }
}