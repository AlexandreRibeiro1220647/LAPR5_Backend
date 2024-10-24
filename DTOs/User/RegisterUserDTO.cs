using TodoApi.Models.User;

namespace TodoApi.DTOs.User;

public class RegisterUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }
    public string Password { get; set; }
}