using TodoApi.Models;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.User;
using Microsoft.EntityFrameworkCore;

public class UserRepository : BaseRepository<User, UserID>, IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(IPOContext dbContext) : base(dbContext.Users)
    {
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.Value == email);
    }
}