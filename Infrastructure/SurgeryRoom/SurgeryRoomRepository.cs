using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;


public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, RoomNumber>, ISurgeryRoomRepository
{
    
    private readonly DbSet<SurgeryRoom> _dbSet;


    public SurgeryRoomRepository(IPOContext context) : base(context.SurgeryRooms)
    {
        _dbSet = context.Set<SurgeryRoom>();
     
    }

}

