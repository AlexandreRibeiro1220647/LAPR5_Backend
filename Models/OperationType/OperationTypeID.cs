
using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.OperationType
{
public class OperationTypeID : UniqueID {

    [JsonConstructor]
        public OperationTypeID(string value) : base(value)
        {
        }

        public OperationTypeID(Guid value) : base(value)
        {
        }
    }
}