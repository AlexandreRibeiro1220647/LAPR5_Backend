using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient {
    public class AllergyId : EntityId 
    {
        // Property to store the value for EF Core mapping
        [JsonPropertyName("value")]
        public Guid Value { get; private set; }
        
        [JsonConstructor]
        public AllergyId(Guid value) : base(value)
        {
            this.Value = value;
        }

        public AllergyId(string value) : base(new Guid(value))
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