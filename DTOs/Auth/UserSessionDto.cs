namespace TodoApi.DTOs.Auth;

public class UserSessionDTO
{
    public string Id {get; set;}
    public string SessionId { get; set; }
    public string AccessToken { get; set; }
    public bool IsAuthenticated { get; set; }
    
    public UserSessionDTO()
    {
        
    }
    
    public UserSessionDTO(string id, string sessionId, string accessToken, bool isAuthenticated)
    {
        Id = id;
        SessionId = sessionId;
        AccessToken = accessToken;
        IsAuthenticated = isAuthenticated;
    }
    
}