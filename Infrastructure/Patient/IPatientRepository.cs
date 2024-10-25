using TodoApi.Models.Shared;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.Patient;

public interface IPatientRepository : IRepository<Models.Patient.Patient, MedicalRecordNumber> {
    Task<Models.Patient.Patient?> GetByEmailAsync(string email);

    void DeletePatient(Models.Patient.Patient patient);
}