using TodoApi.DTOs.User;

namespace TodoApi.Services.Login;

public interface ILoginService
{
    Task<string>  AuthenticateUser();
    Task<string> RegisterPatient();
    Task<string> GetManagementApiTokenAsync();
    Task<string> ExchangeAuthorizationCodeForTokensAsync(string code);
    Task createUserAuth0(RegisterUserDTO model);
    Task changePassword(string email);
    Task defineIAMRoleAsPatient(string email);
    string GetEmailFromIdToken(string idToken);
    string GetSubjectFromIdToken(string idToken);
}