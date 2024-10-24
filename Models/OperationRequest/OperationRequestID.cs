using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

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