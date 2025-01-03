using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

public class RoomTypeId : UniqueID
{

    [JsonConstructor]
    
    public RoomTypeId(string value) : base(value)
    {
    }

    public RoomTypeId(Guid value) : base(value)
    {
    }
}