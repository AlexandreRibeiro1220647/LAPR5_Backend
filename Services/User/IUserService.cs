using TodoApi.DTOs.User;

namespace TodoApi.Services.User;

public interface IUserService
{
    Task<UserDTO> CreateUser(RegisterUserDTO dto);
    Task<IEnumerable<Models.User.User>> GetAllUsersAsync();
    Task<Models.User.User> GetUserByEmail(string email);

    Task createUserAuth0(RegisterUserDTO model);

}