using TodoApi.Models.Shared;

namespace TodoApi.Models.Specialization{
public class SpecializationDesignation
{
    public string Designation { get; }

    private SpecializationDesignation(){}

    public SpecializationDesignation(string designation)
    {
        if (string.IsNullOrWhiteSpace(designation))
            throw new BusinessRuleValidationException("Specialization designation cannot be empty or null.");
        Designation = designation;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (SpecializationDesignation)obj;
        return Designation.Equals(other.Designation);
    }

    public override int GetHashCode()
    {
        return Designation.GetHashCode();
    }

    public override string ToString()
    {
        return Designation;
    }
    }
}