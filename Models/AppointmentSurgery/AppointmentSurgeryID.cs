using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

public class AppointmentSurgeryID : UniqueID
{

    [JsonConstructor]
    
    public AppointmentSurgeryID(string value) : base(value)
    {
    }

    public AppointmentSurgeryID(Guid value) : base(value)
    {
    }
}