using TodoApi.Models;

namespace TodoApi.DTOs.User;

public class RegisterUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoles Role { get; set; }


    public RegisterUserDTO(string name, string email, UserRoles role)
    {
        Name = name;
        Email = email;
        Role = role;
    }

}