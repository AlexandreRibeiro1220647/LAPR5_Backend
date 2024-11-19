using TodoApi.Models.Shared;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.Patient;

public interface IPatientRepository : IRepository<Models.Patient.Patient, MedicalRecordNumber> {
    void DeletePatient(Models.Patient.Patient patient);
    Task<Models.Patient.Patient?> GetByIdAsync(MedicalRecordNumber id);
    Task<bool> ExistsAsync(MedicalRecordNumber patientId);
    Task<List<Models.Patient.Patient>> SearchAsync(string? contact, Gender? gender, DateOnly? dateOfBirth);
    Task<List<Models.Patient.Patient>> GetByUserAsync(TodoApi.DTOs.User.UserDTO user);
}