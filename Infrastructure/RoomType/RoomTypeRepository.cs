
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;

public class RoomTypeRepository : BaseRepository<RoomType, RoomTypeId>, IRoomTypeRepository
{
    
    private readonly DbSet<RoomType> _dbSet;



    public RoomTypeRepository(IPOContext context) : base(context.RoomTypes)
    {
        _dbSet = context.Set<RoomType>();

    }

    public async Task<bool> ExistsAsync(RoomDesignation roomDesignation)
{
    return await _dbSet.AnyAsync(req => req.RoomDesignation.Equals(roomDesignation));
}   
}