using TodoApi.DTOs;
using TodoApi.Models.Patient;

namespace TodoApi.Mappers;

public interface IPatientMapper : IMapper<Patient, PatientDTO, RegisterPatientDTO> { }