using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class LicenseNumber : EntityId
    {
        [JsonPropertyName("value")]
        public string Value { get; private set; }

        [JsonConstructor]
        public LicenseNumber(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("License number cannot be empty.");
            this.Value = value;
        }

        public LicenseNumber() : base(Guid.NewGuid().ToString())
        {
            this.Value = Guid.NewGuid().ToString();
        }

        protected override object createFromString(string text)
        {
            return text;
        }

        public override string AsString()
        {
            return Value;
        }
    }
}