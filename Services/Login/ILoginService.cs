namespace TodoApi.Services.Login;

public interface ILoginService
{
    Task<string?> AuthenticateUser();
    Task<string> GetManagementApiTokenAsync();
    
}