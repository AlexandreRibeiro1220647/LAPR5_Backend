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
}