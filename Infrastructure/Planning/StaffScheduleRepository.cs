using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models;
using TodoApi.Models.Staff;

namespace TodoApi.Infrastructure;
public class StaffScheduleRepository : BaseRepository<StaffSchedule, StaffScheduleID>, IStaffScheduleRepository
{
    
    private readonly DbSet<StaffSchedule> _dbSet;

    public StaffScheduleRepository(IPOContext dbContext) : base(dbContext.StaffSchedules)
    {
        _dbSet = dbContext.Set<StaffSchedule>();
    }
    public async Task<StaffSchedule?> GetByDoctorIdAsync(LicenseNumber DoctorId)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.DoctorId == DoctorId);
    }

}