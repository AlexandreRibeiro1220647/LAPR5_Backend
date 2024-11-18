using TodoApi.DTOs.User;
using TodoApi.DTOs.Auth;
using TodoApi.Models.Auth;

namespace TodoApi.Services.Login;

public interface ILoginService
{
    Task<UserSessionDTO>  AuthenticateUser(string sessionId);
    Task<UserSessionDTO> SignUpPatient(string sessionId);
    Task<string> GetManagementApiTokenAsync();
    Task<string> ExchangeAuthorizationCodeForTokensAsync(string code);
    Task createUserAuth0(RegisterUserDTO model);
    Task changePassword(string email);
    Task defineIAMRoleAsPatient(string email);
    string GetEmailFromIdToken(string idToken);
    string GetSubjectFromIdToken(string idToken);
    Task<UserSessionDTO> CreateSessionAsync(UserSessionDTO model);
    Task<UserSessionDTO> MarkSessionAsAuthenticated(string sessionId, string id_token);
    Task<UserSession> GetSessionByIdAsync(string id);
}