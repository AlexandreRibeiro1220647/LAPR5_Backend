using TodoApi.Models.Shared;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.Patient;

public interface IPatientRepository : IRepository<Models.Patient.Patient, MedicalRecordNumber> {
    void DeletePatient(Models.Patient.Patient patient);
    Task<Models.Patient.Patient?> GetByIdAsync(MedicalRecordNumber id);
    Task<List<Models.Patient.Patient>> GetByContactInformationAsync(string contact);
    Task<bool> ExistsAsync(MedicalRecordNumber patientId);
    Task<List<Models.Patient.Patient>> GetByGenderAsync(Gender gender);
    Task<List<Models.Patient.Patient>> GetByDateOfBirthAsync(DateOnly dateOfBirth);
    Task<List<Models.Patient.Patient>> GetByUserAsync(TodoApi.DTOs.User.UserDTO user);
}