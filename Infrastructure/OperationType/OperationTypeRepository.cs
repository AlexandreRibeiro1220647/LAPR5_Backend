
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Shared;

namespace TodoApi.Infrastructure.OperationType;
public class OperationTypeRepository : BaseRepository<Models.OperationType.OperationType, Models.OperationType.OperationTypeID>, IOperationTypeRepository
{
    
    private readonly DbSet<Models.OperationType.OperationType> _dbSet;

    public OperationTypeRepository(IPOContext dbContext) : base(dbContext.OperationTypes)
    {
        _dbSet = dbContext.Set<Models.OperationType.OperationType>();
    }
    public async Task<Models.OperationType.OperationType?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(o => o.Name == name);
    }

    public async Task<List<Models.OperationType.OperationType>> SearchAsync(string? name, string? specialization, string? estimatedDuration, string? status)
    {   

        IQueryable<Models.OperationType.OperationType> query = _dbSet;


        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(op => op.Name.Equals(name));
        }

        if (!string.IsNullOrEmpty(specialization))
        {
            query = query.Where(op => op.RequiredStaffBySpecialization.Contains(specialization));
        }

        if (!string.IsNullOrEmpty(estimatedDuration))
        {
            query = query.Where(op => op.EstimatedDuration.Contains(TimeSpan.Parse(estimatedDuration)));

        }

        if (!string.IsNullOrEmpty(status))
        {
            if (bool.TryParse(status, out bool isActive))
            {
                query = query.Where(op => op.IsActive == isActive);
            }
            else
            {
                // Handle invalid status values (e.g., return a bad request or skip filtering)
                throw new ArgumentException("Invalid value for status. Expected 'true' or 'false'.");
            }        
        }

        return await query.ToListAsync();
    }

}