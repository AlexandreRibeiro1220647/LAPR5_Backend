using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models;

public class UserID : UniqueID
{
    [JsonConstructor]
    
    public UserID(string value) : base(value)
    {
    }
    public UserID(Guid value) : base(value)
    {
    }
}