using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.Auth;

namespace TodoApi.Infrastructure;

public interface IUserSessionRepository : IRepository<UserSession, UserSessionID>
{
    Task<IEnumerable<UserSession>> GetAllUsersAsync();
    
    Task<UserSession?> GetBySessionIDAsync(string sessionId);
}
