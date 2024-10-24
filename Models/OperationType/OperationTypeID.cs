
using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

public class OperationTypeID : UniqueID {

    [JsonConstructor]
        public OperationTypeID(string value) : base(value)
        {
        }

        public OperationTypeID(Guid value) : base(value)
        {
        }
    }
