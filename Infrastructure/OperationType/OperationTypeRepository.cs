
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
}