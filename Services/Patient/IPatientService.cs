using TodoApi.DTOs;
using TodoApi.Models.Patient;

namespace TodoApi.Services;

public interface IPatientService {
    Task<PatientDTO> RegisterPatient(RegisterPatientDTO dto);
    Task<PatientDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto);
    Task<List<PatientDTO>> GetAllPatients();
    Task<bool> DeletePatientByEmailAsync(string email);
    Task<PatientDTO> GetPatientByEmailAsync(string email);
    Task<PatientDTO> GetPatientByIdAsync(Guid id);
    Task<List<PatientDTO>> GetPatientsByNameAsync(string name);
    Task<List<PatientDTO>> GetPatientsByContactInformationAsync(string contact);
    Task<List<PatientDTO>> GetPatientsByGenderAsync(Gender gender);
    Task<List<PatientDTO>> GetPatientsByDateOfBirthAsync(DateOnly dateOfBirth);
}