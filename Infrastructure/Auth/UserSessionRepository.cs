using TodoApi.Models;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using TodoApi.Models.Auth;

public class UserSessionRepository : BaseRepository<UserSession, UserSessionID>, IUserSessionRepository
{
    private readonly DbSet<UserSession> _dbSet;

    public UserSessionRepository(IPOContext dbContext) : base(dbContext.UserSessions)
    {
        _dbSet = dbContext.Set<UserSession>();
    }

    public async Task<IEnumerable<UserSession>> GetAllUsersAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public async Task<UserSession?> GetBySessionIDAsync(string sessionId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.SessionId == sessionId);
    }
}