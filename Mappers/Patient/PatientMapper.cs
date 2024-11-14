using TodoApi.Models.Patient;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Models.Shared;

namespace TodoApi.Mappers;

public class PatientMapper : IPatientMapper {

    public PatientDTO ToDto(Patient entity) {
        return new PatientDTO(entity.dateOfBirth.dateOfBirth.ToString(), entity.gender.ToString(), entity.Id.AsString(), entity.contactInformation.contactInformation.phoneNumber, 
        entity.medicalConditions.medicalConditions, entity.emergencyContact.emergencyContact.phoneNumber, entity.appointmentHistory.appointments, entity.user);
    }

    public Patient ToEntity(PatientDTO dto) {
        Gender gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender, true);
        DateOnly dateOfBirth = DateOnly.Parse(dto.DateOfBirth);
        return new Patient(new DateOfBirth(dateOfBirth), gender, new MedicalRecordNumber(dto.MedicalRecordNumber), 
        new ContactInformation(new Phone(dto.ContactInformation)), new MedicalConditions(dto.MedicalConditions), new EmergencyContact(new Phone(dto.EmergencyContact)), new AppointmentHistory(dto.AppointmentHistory), dto.User);
    }

    public Patient toEntity(RegisterPatientDTO dto) {
        Gender gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender, true);
        DateOnly dateOfBirth = DateOnly.Parse(dto.DateOfBirth);
        return null;
    }

        public Patient toEntity(RegisterPatientDTO dto, TodoApi.DTOs.User.UserDTO user) {
        Gender gender = (Gender)Enum.Parse(typeof(Gender), dto.Gender, true);
        DateOnly dateOfBirth = DateOnly.Parse(dto.DateOfBirth);
        return new Patient(new DateOfBirth(dateOfBirth), gender, new ContactInformation(new Phone(dto.ContactInformation)),
        new MedicalConditions(dto.MedicalConditions), new EmergencyContact(new Phone(dto.EmergencyContact)), new AppointmentHistory(dto.AppointmentHistory), user);
    }
}