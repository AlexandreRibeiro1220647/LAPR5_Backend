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

    public async Task<Models.Patient.Patient?> GetByEmailAsync(string email) {
        return await _dbSet.FirstOrDefaultAsync(p => p.email.Value == email);
    }

    public void DeletePatient(Models.Patient.Patient patient) {
        _dbSet.Remove(patient);
    }

    public async Task<Models.Patient.Patient?> GetByIdAsync(MedicalRecordNumber id) {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Models.Patient.Patient>> GetByNameAsync(string name) {
        return await _dbSet.Where(p => p.fullName.fullName.Equals(name)).ToListAsync();
    }

    public async Task<List<Models.Patient.Patient>> GetByContactInformationAsync(string contact) {
        return await _dbSet.Where(p => p.contactInformation.contactInformation.phoneNumber.Contains(contact)).ToListAsync();
    }

   public async Task<List<Models.Patient.Patient>> GetByGenderAsync(Gender gender) {
        return await _dbSet.Where(p => p.gender == gender).ToListAsync();
   }

   public async Task<List<Models.Patient.Patient>> GetByDateOfBirthAsync(DateOnly dateOfBirth) {
       return await _dbSet.Where(p => p.dateOfBirth.dateOfBirth == dateOfBirth).ToListAsync();
   }
   public async Task<bool> ExistsAsync(MedicalRecordNumber patientId)
    {
        return await _dbSet.AnyAsync(p => p.Id == patientId);
    }   
}
