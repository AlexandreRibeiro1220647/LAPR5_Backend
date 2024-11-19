using TodoApi.DTOs;
using TodoApi.Models.Patient;

namespace TodoApi.Services;

public interface IPatientService {
    Task<PatientDTO> RegisterPatient(RegisterPatientDTO dto);
    Task<PatientDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto);
    Task<List<PatientDTO>> GetAllPatients();
    Task<bool> DeletePatientByIDAsync(Guid id);
    Task<PatientDTO> GetPatientByIdAsync(Guid id);
    Task<List<PatientDTO>> SearchPatients(string? contact, Gender? gender, DateOnly? dateOfBirth);
}