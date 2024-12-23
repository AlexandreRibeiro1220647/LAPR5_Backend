namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class MedicalCondition : Entity<MedicalConditionId> {
    public string Code { get; private set; }
    public string Designation { get; private set; }
    public string Description { get; private set; }
    public string CommonSymptoms { get; private set; }

    public MedicalCondition() {}

    public MedicalCondition(string code, string designation, string description, string commonSymptoms) {
        Id = new MedicalConditionId(Guid.NewGuid());
        this.Code = code;
        this.Designation = designation;
        this.Description = description;
        this.CommonSymptoms = commonSymptoms;
    }

    public MedicalCondition(MedicalConditionId id, string code, string designation, string description, string commonSymptoms) {
        Id = id;
        this.Code = code;
        this.Designation = designation;
        this.Description = description;
        this.CommonSymptoms = commonSymptoms;
    }
}