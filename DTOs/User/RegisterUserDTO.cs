using TodoApi.Models;

namespace TodoApi.DTOs.User;

public class RegisterUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }
}