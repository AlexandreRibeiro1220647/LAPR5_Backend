using TodoApi.Models;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure;

namespace TodoApi.Infrastructure.Patient;
public class PatientRepository : BaseRepository<Models.Patient.Patient, MedicalRecordNumber>, IPatientRepository {
    private readonly DbSet<Models.Patient.Patient?> _dbSet;

    public PatientRepository(IPOContext dbContext) : base(dbContext.Patients) {
        _dbSet = dbContext.Set<Models.Patient.Patient>();
    }

    public void DeletePatient(Models.Patient.Patient patient) {
        _dbSet.Remove(patient);
    }

    public async Task<Models.Patient.Patient?> GetByIdAsync(MedicalRecordNumber id) {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Models.Patient.Patient>> SearchAsync(string? contact, Gender? gender, DateOnly? dateOfBirth)
    {
        IQueryable<Models.Patient.Patient> query = _dbSet;

        if (!string.IsNullOrEmpty(contact))
        {
            query = query.Where(p => p.contactInformation.contactInformation.phoneNumber.Contains(contact));
        }

        if (gender.HasValue)
        {
            query = query.Where(p => p.gender == gender);
        }

        if (dateOfBirth.HasValue)
        {
            query = query.Where(p => p.dateOfBirth.dateOfBirth == dateOfBirth);
        }

        return await query.ToListAsync();
    }


    public async Task<List<Models.Patient.Patient>> GetByUserAsync(TodoApi.DTOs.User.UserDTO user) {
        return await _dbSet.Where(p => p.user.Id.Equals(user.Id)).ToListAsync();
    }

   public async Task<bool> ExistsAsync(MedicalRecordNumber patientId)
    {
        return await _dbSet.AnyAsync(p => p.Id == patientId);
    }   
    public async Task<List<Models.Patient.Patient>> GetByNameAsync(string name)
{ 
    return await _dbSet.Where(p => EF.Functions.Like(p.user.Name, $"%{name}%")).ToListAsync();
}
}
