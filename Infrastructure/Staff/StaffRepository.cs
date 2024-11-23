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

        public async Task<List<Models.Staff.Staff>> GetByUserAsync(TodoApi.DTOs.User.UserDTO user) {
                return await _context.Staffs.Where(p => p.user.Id.Equals(user.Id)).ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchAsync(
            string? fullName = null,
            string? specialization = null,
            string? email = null,
            string? status = null,
            string? phone = null)
        {
            IQueryable<Models.Staff.Staff> query = _context.Staffs;

            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(s => s.user.Name.Contains(fullName));
            }

            if (!string.IsNullOrEmpty(specialization))
            {
                query = query.Where(s => s.Specialization.Area.Contains(specialization));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(s => s.user.Email.Value.Contains(email));
            }

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<StaffStatus>(status, true, out var statusEnum))
            {
                query = query.Where(s => s.Status == statusEnum);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(s => s.Phone.phoneNumber.Contains(phone));
            }

            return await query.ToListAsync();
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