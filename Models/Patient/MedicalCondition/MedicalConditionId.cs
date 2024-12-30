using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient {
    public class MedicalConditionId : EntityId 
    {
        // Property to store the value for EF Core mapping
        [JsonPropertyName("value")]
        public Guid Value { get; private set; }
        
        [JsonConstructor]
        public MedicalConditionId(Guid value) : base(value)
        {
            this.Value = value;
        }

        public MedicalConditionId(string value) : base(new Guid(value))
        {
            this.Value = new Guid(value);
        }


        protected override object createFromString(string text)
        {
            return new Guid(text);
        }

        public override string AsString()
        {
            return Value.ToString();
        }

        public Guid AsGuid()
        {
            return Value;
        }

    }
}