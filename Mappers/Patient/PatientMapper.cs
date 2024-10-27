using TodoApi.Models.Patient;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Models.Shared;

namespace TodoApi.Mappers;

public class PatientMapper : IPatientMapper {

    public PatientDTO ToDto(Patient entity) {
        return new PatientDTO(entity.fullName.ToString(), entity.dateOfBirth.ToString(), entity.gender.ToString(), entity.Id.ToString(), entity.contactInformation.ToString(), 
        entity.email.ToString(), entity.medicalConditions.medicalConditions, entity.emergencyContact.ToString(), entity.appointmentHistory.appointments);
    }

    public Patient ToEntity(PatientDTO dto) {
        Gender gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender, true);
        DateOnly dateOfBirth = DateOnly.Parse(dto.DateOfBirth);
        return new Patient(new FullName(dto.FullName), new DateOfBirth(dateOfBirth), gender, new MedicalRecordNumber(dto.MedicalRecordNumber), 
        new ContactInformation(new Phone(dto.ContactInformation)), new UserEmail(dto.Email), new MedicalConditions(dto.MedicalConditions), new EmergencyContact(new Phone(dto.EmergencyContact)), new AppointmentHistory(dto.AppointmentHistory));
    }

    public Patient toEntity(RegisterPatientDTO dto) {
        Gender gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender, true);
        DateOnly dateOfBirth = DateOnly.Parse(dto.DateOfBirth);
        return new Patient(new FullName(dto.FullName), new DateOfBirth(dateOfBirth), gender, new ContactInformation(new Phone(dto.ContactInformation)), new UserEmail(dto.Email), 
        new MedicalConditions(dto.MedicalConditions), new EmergencyContact(new Phone(dto.EmergencyContact)), new AppointmentHistory(dto.AppointmentHistory));
    }
}