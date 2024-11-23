using TodoApi.Models;

namespace TodoApi.DTOs.User;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public UserEmail Email { get; set; }
    public string Role { get; set; }

    
    public UserDTO()
    {
        
    }
    
    public UserDTO(string id, string name, UserEmail email, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
    
}