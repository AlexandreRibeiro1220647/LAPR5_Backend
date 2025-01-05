using TodoApi.Models.Shared;

namespace TodoApi.Models.Specialization{
public class SpecializationDescription
{
    public string Description { get; }

    private SpecializationDescription(){}

    public SpecializationDescription(string description)
    {
        Description = description;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (SpecializationDescription)obj;
        return Description.Equals(other.Description);
    }

    public override int GetHashCode()
    {
        return Description.GetHashCode();
    }

    public override string ToString()
    {
        return Description;
    }
    }
}