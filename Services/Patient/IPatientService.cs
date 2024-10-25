using TodoApi.DTOs;

namespace TodoApi.Services;

public interface IPatientService {
    Task<PatientDTO> RegisterPatient(RegisterPatientDTO dto);
    Task<PatientDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto);
    Task<List<PatientDTO>> GetAllPatients();
    Task<bool> DeletePatientByEmailAsync(string email);
}