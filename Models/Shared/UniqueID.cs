using System;
using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Shared
{
    public class UniqueID : EntityId
    {
        [JsonPropertyName("value")]
        public Guid Value { get; private set; }

        [JsonConstructor]
        public UniqueID(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("ID cannot be null or empty", nameof(value));
            }
            this.Value = new Guid(value);
        }

        public UniqueID(Guid value) : base(value)
        {
            this.Value = value;
        }

        protected override object createFromString(string text)
        {
            return text;
        }

        public override string AsString()
        {
            return Value.ToString();
        }

       
    }
}