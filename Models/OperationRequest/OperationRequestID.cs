using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.OperationRequest;
public class OperationRequestID : UniqueID
{

    [JsonConstructor]
    
    public OperationRequestID(string value) : base(value)
    {
    }

    public OperationRequestID(Guid value) : base(value)
    {
    }
}