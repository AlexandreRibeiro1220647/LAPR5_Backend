using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.OperationRequest;
public class OperationRequestLogID : UniqueID
{

    [JsonConstructor]
    
    public OperationRequestLogID(string value) : base(value)
    {
    }

    public OperationRequestLogID(Guid value) : base(value)
    {
    }
}