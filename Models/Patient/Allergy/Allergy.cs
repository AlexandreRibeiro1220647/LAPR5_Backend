namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class Allergy : Entity<AllergyId> {
    public string Code { get; private set; }
    public string Designation { get; private set; }
    public string? Description { get; private set; }

    public Allergy() {}
    
    public Allergy(string code, string designation) {
        Id = new AllergyId(Guid.NewGuid());
        this.Code = code;
        this.Designation = designation;
    }
    public Allergy(AllergyId id, string code, string designation) {
        Id = id;
        this.Code = code;
        this.Designation = designation;
    }
    public Allergy(string code, string designation, string description) {
        Id = new AllergyId(Guid.NewGuid());
        this.Code = code;
        this.Designation = designation;
        this.Description = description;
    }
    public Allergy(AllergyId id, string code, string designation, string description) {
        Id = id;
        this.Code = code;
        this.Designation = designation;
        this.Description = description;
    }
    
}