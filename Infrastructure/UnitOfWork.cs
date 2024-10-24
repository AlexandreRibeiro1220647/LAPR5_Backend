using Models.Shared;

namespace TodoApi.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IPOContext _context;

    public UnitOfWork(IPOContext context)
    {
        this._context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await this._context.SaveChangesAsync();
    }
}