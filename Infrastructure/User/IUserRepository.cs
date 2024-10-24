using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.User;

namespace TodoApi.Infrastructure;

public interface IUserRepository : IRepository<Models.User.User, UserID>
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    
    Task<User?> GetByEmailAsync(string email);
}
