﻿using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Models.Shared;
using TodoApi.Models.Patient;
using TodoApi.Mappers;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Patient;
using TodoApi.Services.User;


namespace TodoApi.Services;

public class PatientService : IPatientService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;
    private readonly ILogger<IPatientService> _logger;
    private readonly IUserService _userService;
    private PatientMapper _mapper = new PatientMapper();

    public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository, ILogger<IPatientService> logger, IUserService userService) {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
        _logger = logger;
        _userService = userService;
    }

    public async Task<PatientDTO> RegisterPatient(RegisterPatientDTO dto) {
        try {
            await _userService.CreateUser(new DTOs.User.RegisterUserDTO(dto.FullName, dto.Email, UserRoles.Patient));
            
            Patient mapped = _mapper.toEntity(dto);
            await _patientRepository.AddAsync(mapped);
            PatientDTO mappedDto = _mapper.ToDto(mapped);
            await _unitOfWork.CommitAsync();
            return mappedDto;

        } catch (Exception e) {
            _logger.LogError(e, "Error registering patient");
            throw;
        }
    }

    public async Task<PatientDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto) {
        try {
            Patient patient = await _patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null) {
                throw new Exception("Patient does not exist");
            }
            patient.UpdateFullName(dto.FullName);
            patient.UpdateContactInformation(dto.ContactInformation);
            patient.UpdateEmail(dto.Email);
            patient.UpdateMedicalConditions(dto.MedicalConditions);
            patient.UpdateEmergencyContact(dto.EmergencyContact);
            await _unitOfWork.CommitAsync();

            PatientDTO newPatientDto = new PatientDTO(patient.fullName.ToString(), patient.dateOfBirth.ToString(), patient.gender.ToString(), patient.Id.AsString(), 
            patient.contactInformation.ToString(), patient.email.Value, patient.medicalConditions.medicalConditions, patient.emergencyContact.ToString(), patient.appointmentHistory.appointments);
            return newPatientDto;
        }
        catch (Exception e) {
            _logger.LogError(e, "Error updating patient information");
            throw;
        }
    }

    public async Task<List<PatientDTO>> GetAllPatients() {
        List<Patient> patientList = await _patientRepository.GetAllAsync();
        List<PatientDTO> patientDTOList = new List<PatientDTO>();
        foreach (Patient p in patientList) {
            PatientDTO patientDTO = _mapper.ToDto(p);
            patientDTOList.Add(patientDTO);
        }
        return patientDTOList;
    }
    
    public async Task<bool> DeletePatientByEmailAsync(string email) {
        try {
            var patient = await _patientRepository.GetByEmailAsync(email);
            if (patient == null) {
                throw new Exception($"Patient with email {email} does not exist");
            }
            _patientRepository.Remove(patient);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation($"Patient with email {email} was deleted");
            return true;
        }
        catch (Exception e) {
            _logger.LogError(e, $"Error deleting patient with email {email}");
            throw;
        }
    }
    public async Task<PatientDTO> GetPatientByEmailAsync(string email) {
        Patient patient = await _patientRepository.GetByEmailAsync(email);
        if (patient == null) {
            throw new Exception($"Patient with email {email} does not exist");
        }
        return _mapper.ToDto(patient);
    }

    public async Task<PatientDTO> GetPatientByIdAsync(Guid id) {
        Patient patient = await _patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
        if (patient == null) {
            throw new Exception($"Patient with id {id} does not exist");
        }
        return _mapper.ToDto(patient);
    }

    public async Task<List<PatientDTO>> GetPatientsByNameAsync(string name) {
        List<Patient> patients = await _patientRepository.GetByNameAsync(name);
        return patients.Select(p => _mapper.ToDto(p)).ToList();
    }

    public async Task<List<PatientDTO>> GetPatientsByContactInformationAsync(string contact) {
        List<Patient> patients = await _patientRepository.GetByContactInformationAsync(contact);
        return patients.Select(p => _mapper.ToDto(p)).ToList();
    }

    public async Task<List<PatientDTO>> GetPatientsByGenderAsync(Gender gender) {
        List<Patient> patients = await _patientRepository.GetByGenderAsync(gender);
        return patients.Select(p => _mapper.ToDto(p)).ToList();
    }

    public async Task<List<PatientDTO>> GetPatientsByDateOfBirthAsync(DateOnly dateOfBirth) {
        List<Patient> patients = await _patientRepository.GetByDateOfBirthAsync(dateOfBirth);
        return patients.Select(p => _mapper.ToDto(p)).ToList();
    }
}