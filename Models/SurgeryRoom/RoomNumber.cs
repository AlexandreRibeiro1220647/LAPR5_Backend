using System.Text.Json.Serialization;
using TodoApi.Models.Shared;


public class RoomNumber : UniqueID
{

    [JsonConstructor]
    
    public RoomNumber(string value) : base(value)
    {
    }

    public RoomNumber(Guid value) : base(value)
    {
    }
}