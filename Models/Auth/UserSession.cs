using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Shared;
namespace TodoApi.Models.Auth
{
public class UserSession : Entity<UserSessionID>
    {
        public string SessionId { get; private set; }
        public string AccessToken { get; private set; }
        public bool IsAuthenticated { get; private set; } = true; 

        public UserSession() { }

        public UserSession(string sessionId, string accessToken, bool isAuthenticated)
        {
            Id = new UserSessionID(Guid.NewGuid().ToString());
            SessionId = sessionId;
            AccessToken = accessToken;
            IsAuthenticated = isAuthenticated; 
        }

        public void authenticate() {
            this.IsAuthenticated = true;
        }

        public void logout() {
            this.IsAuthenticated = false;
        }

        public void updateToken(string id_token) {
            this.AccessToken = id_token;
        }

    }
}