using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.OperationRequest;
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