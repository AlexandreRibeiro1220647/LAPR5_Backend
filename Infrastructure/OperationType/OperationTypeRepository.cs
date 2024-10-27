
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
    
            public async Task<List<Models.OperationType.OperationType>> SearchByName(string name)
        {
            return await _dbSet.Where(o => o.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Models.OperationType.OperationType>> SearchBySpecialization(string specialization)
        {
            return await _dbSet.Where(o => o.RequiredStaffBySpecialization.Contains(specialization))
                .ToListAsync();
        }

        public async Task<List<Models.OperationType.OperationType>> SearchByStatus(bool status)
        {
            return await _dbSet.Where(o => o.IsActive == status)
                .ToListAsync();
        }
}