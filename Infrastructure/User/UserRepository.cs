using TodoApi.Models;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

public class UserRepository : BaseRepository<User, UserID>, IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(IPOContext dbContext) : base(dbContext.Users)
    {
        _dbSet = dbContext.Set<TodoApi.Models.User.User>();
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