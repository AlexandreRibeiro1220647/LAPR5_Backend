using TodoApi.DTOs.User;

namespace TodoApi.Services.Login;

public interface ILoginService
{
    Task<string>  AuthenticateUser();
    Task<string> GetManagementApiTokenAsync();
    Task<string> GetAuthenticationToken();

    Task createUserAuth0(RegisterUserDTO model);
    Task changePassword(string email);
    Task<UserInfo> GetUserInfoBySubjectAsync(string subject, string accessToken);
    
}