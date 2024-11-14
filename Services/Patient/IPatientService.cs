using TodoApi.DTOs;
using TodoApi.Models.Patient;

namespace TodoApi.Services;

public interface IPatientService {
    Task<PatientDTO> RegisterPatient(RegisterPatientDTO dto);
    Task<PatientDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto);
    Task<List<PatientDTO>> GetAllPatients();
    Task<bool> DeletePatientByIDAsync(Guid id);
    Task<PatientDTO> GetPatientByIdAsync(Guid id);
    Task<List<PatientDTO>> GetPatientsByContactInformationAsync(string contact);
    Task<List<PatientDTO>> GetPatientsByGenderAsync(Gender gender);
    Task<List<PatientDTO>> GetPatientsByDateOfBirthAsync(DateOnly dateOfBirth);
}