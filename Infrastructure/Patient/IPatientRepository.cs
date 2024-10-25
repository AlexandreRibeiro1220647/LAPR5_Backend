using TodoApi.Models.Shared;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.Patient;

public interface IPatientRepository : IRepository<Models.Patient.Patient, MedicalRecordNumber> {
    Task<Models.Patient.Patient?> GetByEmailAsync(string email);
    void DeletePatient(Models.Patient.Patient patient);
    Task<Models.Patient.Patient?> GetByIdAsync(MedicalRecordNumber id);
    Task<List<Models.Patient.Patient>> GetByNameAsync(string name);
    Task<List<Models.Patient.Patient>> GetByContactInformationAsync(string contact);
    Task<List<Models.Patient.Patient>> GetByGenderAsync(Gender gender);
    Task<List<Models.Patient.Patient>> GetByDateOfBirthAsync(DateOnly dateOfBirth);
}