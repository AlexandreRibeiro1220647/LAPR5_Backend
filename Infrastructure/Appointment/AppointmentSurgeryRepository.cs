using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;


public class AppointmentSurgeryRepository : BaseRepository<AppointmentSurgery, AppointmentSurgeryID>, IAppointmentSurgeryRepository
{
    
    private readonly DbSet<AppointmentSurgery> _dbSet;

    public AppointmentSurgeryRepository(IPOContext context) : base(context.AppointmentSurgeries)
    {
        _dbSet = context.Set<AppointmentSurgery>();
     
    }

    public async Task<bool> ExistsAsync(OperationRequestID operationRequestID)
{
    return await _dbSet.AnyAsync(req => req.OperationRequestID == operationRequestID);
}   
}