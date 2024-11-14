using System.Text.Json.Serialization;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Auth;
public class UserSessionID : UniqueID
{

    [JsonConstructor]
    
    public UserSessionID(string value) : base(value)
    {
    }

    public UserSessionID(Guid value) : base(value)
    {
    }
}