namespace TodoApi.DTOs.User;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    
    public UserDTO()
    {
        
    }
    
    public UserDTO(string id, string name, string email, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
    
}