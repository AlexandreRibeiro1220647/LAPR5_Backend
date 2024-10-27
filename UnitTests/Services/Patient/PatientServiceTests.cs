using Moq;
using Xunit;
using TodoApi.DTOs;
using TodoApi.Infrastructure;
using TodoApi.Mappers;
using TodoApi.Services;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models;
using TodoApi.Infrastructure.Patient;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<IPatientService>> _loggerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly PatientService _patientService;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPatientMapper> _mapperMock;


    public PatientServiceTests()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<IPatientService>>();
        _configMock = new Mock<IConfiguration>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _patientService = new PatientService(_unitOfWorkMock.Object, _patientRepositoryMock.Object, _loggerMock.Object, _configMock.Object, _userRepositoryMock.Object);

    }
    [Fact]
    public async Task CreateUser_ShouldReturnMappedPatientDTO_WhenPatientIsValid()
    {
        // Arrange
        var dto = new RegisterPatientDTO { FullName = "Test User", DateOfBirth = "2000-1-1", Gender = Gender.Male.ToString(), 
        ContactInformation = "911111111", Email = "test@example.com", MedicalConditions = new List<string>{"medicalcondition"}, EmergencyContact = "912222222", AppointmentHistory = new List<string>{"appointment"}};
        
        var patient = new Patient(new FullName(dto.FullName), new DateOfBirth(new DateOnly(2000, 1, 1)), Gender.Male, new ContactInformation(new Phone(dto.ContactInformation)), 
        new UserEmail(dto.Email), new MedicalConditions(dto.MedicalConditions), new EmergencyContact(new Phone(dto.EmergencyContact)), new AppointmentHistory(dto.AppointmentHistory));
        
        var expectedDto = new PatientDTO { FullName = patient.fullName.fullName, DateOfBirth = patient.dateOfBirth.dateOfBirth.ToString(), Gender = patient.gender.ToString(),
        ContactInformation = patient.contactInformation.contactInformation.phoneNumber, Email = patient.email.Value, MedicalConditions = patient.medicalConditions.medicalConditions,
        EmergencyContact = patient.emergencyContact.emergencyContact.phoneNumber, AppointmentHistory = patient.appointmentHistory.appointments };

        _patientRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Patient>()))
            .ReturnsAsync(patient);
    
        _unitOfWorkMock.Setup(uow => uow.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _patientService.RegisterPatient(dto);

        // Assert
        Assert.Equal(expectedDto.FullName, patient.fullName.fullName);
        Assert.Equal(expectedDto.DateOfBirth, patient.dateOfBirth.dateOfBirth.ToString());
        Assert.Equal(expectedDto.Gender, patient.gender.ToString());
        Assert.Equal(expectedDto.ContactInformation, patient.contactInformation.contactInformation.phoneNumber);
        Assert.Equal(expectedDto.Email, patient.email.Value);
        Assert.Equal(expectedDto.MedicalConditions, patient.medicalConditions.medicalConditions);
        Assert.Equal(expectedDto.EmergencyContact, patient.emergencyContact.emergencyContact.phoneNumber);
        Assert.Equal(expectedDto.AppointmentHistory, patient.appointmentHistory.appointments);

        _patientRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Patient>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new Patient(new FullName("Test User"), new DateOfBirth(new DateOnly(2000,1,1)), Gender.Male, new ContactInformation(new Phone("911111111")), 
            new UserEmail("test@example.com"), new MedicalConditions(new List<string>{"medicalcondition"}), new EmergencyContact(new Phone("912222222")), 
            new AppointmentHistory(new List<string>{"appointment"})),
            
            new Patient(new FullName("Test User2"), new DateOfBirth(new DateOnly(2000,1,2)), Gender.Male, new ContactInformation(new Phone("911111112")), 
            new UserEmail("test2@example.com"), new MedicalConditions(new List<string>{"medicalcondition2"}), new EmergencyContact(new Phone("912222223")), 
            new AppointmentHistory(new List<string>{"appointment2"})),
        };
        _patientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(patients);

        // Act
        var result = await _patientService.GetAllPatients();

        // Assert
        Assert.Equal(patients.Count, result.Count);
    }

    [Fact]
    public async Task GetUserByEmail_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var email = "test@example.com";

        var patient = new Patient(new FullName("Test User"), new DateOfBirth(new DateOnly(2000,1,1)), Gender.Male, new ContactInformation(new Phone("911111111")), 
            new UserEmail(email), new MedicalConditions(new List<string>{"medicalcondition"}), new EmergencyContact(new Phone("912222222")), 
            new AppointmentHistory(new List<string>{"appointment"}));

        _patientRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(patient);

        // Act
        var result = await _patientService.GetPatientByEmailAsync(email);

        // Assert
        Assert.Equal(patient.email.Value, result.Email);
        _patientRepositoryMock.Verify(repo => repo.GetByEmailAsync(email), Times.Once);
    }
}
