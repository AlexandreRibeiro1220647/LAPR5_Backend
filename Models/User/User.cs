using TodoApi.Models.Shared;
using Password = TodoApi.Models.Shared.Password;

namespace TodoApi.Models.User;

public class User : Entity<UserID>
{
    public UserEmail Email { get; private set; }
    public string Name { get; private set; }
    public Shared.Password Password { get; private set; }
    public UserRoles Role { get; private set; }

    public User()
    {
        
    }
    public User(UserEmail email, string name, UserRoles role, string password = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        Password = password != null ? new Password(password) : null;

        Name = name;
        Role = role;
        Email = email;
        Id=new UserID(Guid.NewGuid());
    }
}