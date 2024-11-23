
using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models
{
public class StaffScheduleID : UniqueID {

    [JsonConstructor]
        public StaffScheduleID(string value) : base(value)
        {
        }

        public StaffScheduleID(Guid value) : base(value)
        {
        }
    }
}