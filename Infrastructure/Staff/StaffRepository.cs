using TodoApi.Infrastructure.Shared;
using TodoApi.Models.Staff;
using TodoApi.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Infrastructure.Staff
{
    public class StaffRepository : BaseRepository<Models.Staff.Staff, LicenseNumber>, IStaffRepository
    {
        private readonly HospitalContext _context;

        public StaffRepository(HospitalContext dbContext) : base(dbContext.Staffs)
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
                .Where(s => s.FullName.fullName.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchBySpecialization(string specialization)
        {
            return await _context.Staffs
                .Where(s => s.Specialization.Area.Contains(specialization))
                .ToListAsync();
        }

        public async Task<List<Models.Staff.Staff>> SearchByEmail(string email)
        {
            return await _context.Staffs
                .Where(s => s.Email.Value.Contains(email))
                .ToListAsync();
        }


    }
}