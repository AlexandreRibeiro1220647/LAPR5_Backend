

using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Infrastructure;
public interface IStaffScheduleRepository : IRepository<StaffSchedule, StaffScheduleID>

{
    Task<StaffSchedule?> GetByDoctorIdAsync(LicenseNumber DoctorId);
}