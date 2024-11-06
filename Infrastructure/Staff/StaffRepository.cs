using TodoApi.Infrastructure.Shared;
using TodoApi.Models.Staff;
using TodoApi.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Infrastructure.Staff
{
    public class StaffRepository : BaseRepository<Models.Staff.Staff, LicenseNumber>, IStaffRepository
    {
        private readonly IPOContext _context;

        public StaffRepository(IPOContext dbContext) : base(dbContext.Staffs)
        {
            _context = dbContext;
        }

        public Task<Models.Staff.Staff> GetByLicenseNumber(LicenseNumber licenseNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Models.Staff.Staff>> SearchByName(string name)
        {
            return await _context.Staffs
                .Join(_context.Users,
                      staff => staff.UserId,
                      user => user.Id,
                      (staff, user) => new { Staff = staff, User = user })
                .Where(su => su.User.FullName.Contains(name))
                .Select(su => su.Staff)
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchBySpecialization(string specialization)
        {
            return await _context.Staffs
                .Where(s => s.Specialization.Area.Contains(specialization))
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchByStatus(StaffStatus status)
        {
            return await _context.Staffs
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchByEmail(string email)
        {
            return await _context.Staffs
                .Join(_context.Users,
                      staff => staff.UserId,
                      user => user.Id,
                      (staff, user) => new { Staff = staff, User = user })
                .Where(su => su.User.Email == email)
                .Select(su => su.Staff)
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchByRole(string role)
        {
            return await _context.Staffs
                .Join(_context.Users,
                      staff => staff.UserId,
                      user => user.Id,
                      (staff, user) => new { Staff = staff, User = user })
                .Where(su => su.User.Roles.Contains(role))
                .Select(su => su.Staff)
                .ToListAsync();
        }

        public async Task<Models.Staff.Staff> GetByPhoneAsync(string phone)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.Phone.phoneNumber == phone);
        }

        public async Task<bool> ExistsAsync(LicenseNumber doctorId)
        {
            return await _context.Staffs.AnyAsync(p => p.Id == doctorId);
        }
    }
}