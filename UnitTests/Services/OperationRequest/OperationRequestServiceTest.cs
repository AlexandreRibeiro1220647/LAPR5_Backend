using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TodoApi.Services;
using TodoApi.Infrastructure;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;
using System;
using System.Threading.Tasks;
using TodoApi.Models.Shared;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Staff;
using TodoApi.Models.OperationType;
using TodoApi.Models;

public class OperationRequestServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IOperationRequestRepository> _operationRequestRepoMock = new();
    private readonly Mock<IPatientRepository> _patientRepoMock = new();
    private readonly Mock<IStaffRepository> _staffRepoMock = new();
    private readonly Mock<IOperationRequestLogRepository> _operationRequestLogRepoMock = new();
    private readonly Mock<ILogger<IOperationRequestService>> _loggerMock = new();
    private readonly Mock<IConfiguration> _configMock = new();

    private readonly OperationRequestService _service;

    public OperationRequestServiceTests()
    {
        _service = new OperationRequestService(
            _unitOfWorkMock.Object,
            _operationRequestRepoMock.Object,
            _loggerMock.Object,
            _configMock.Object,
            _patientRepoMock.Object,
            _operationRequestLogRepoMock.Object,
            _staffRepoMock.Object
        );
    }

    [Fact]
    public async Task CreateOperationRequestDoctorNotFoundThrowsException()
    {
        var createDto = new CreateOperationRequestDTO { doctorid = "nonExistingDoctor", pacientid = "12324", operationTypeId = "1", deadline = "2024-12-12", priority = TodoApi.Models.Priority.ELECTIVE };
        _staffRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<LicenseNumber>())).ReturnsAsync(false);

        var ex = await Assert.ThrowsAsync<Exception>(() => _service.CreateOperationRequest(createDto));
        Assert.Contains("The specified doctor does not exist.", ex.Message);
    }

    [Fact]
    public async Task CreateOperationRequestPatientNotFoundThrowsException()
    {
        var createDto = new CreateOperationRequestDTO { doctorid = "3213131", pacientid = "nonExistingPatient", operationTypeId = "1", deadline = "2024-12-12", priority = TodoApi.Models.Priority.ELECTIVE };
        _patientRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<MedicalRecordNumber>())).ReturnsAsync(false);

        var ex = await Assert.ThrowsAsync<Exception>(() => _service.CreateOperationRequest(createDto));
        Assert.Contains("The specified doctor does not exist.", ex.Message);
    }

    [Fact]
    public async Task CreateOperationRequestAsyncNullDtoThrowsArgumentNullException()
    {

    await Assert.ThrowsAsync<Exception>(() => _service.CreateOperationRequest(null));
    }


/*
   [Fact]
    public async Task CreateOperationRequestValidDataReturnsOperationRequestDTO()
    {
        // Configuração do cenário
        var createDto = new CreateOperationRequestDTO 
        { 
            doctorid = "43212bdf-71a4-4f87-95e4-56d25c7f443e", 
            pacientid = "412321af-71a4-4f87-95e4-56d25c7f443e", 
            operationTypeId = "d614fae8-d852-4c3d-9b71-3f176b6cb1fb", 
            deadline = "2024-12-14", 
            priority = TodoApi.Models.Priority.ELECTIVE 
        };        

        // Configuração dos mocks para simular o comportamento esperado
        _staffRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<LicenseNumber>())).ReturnsAsync(true);
        _patientRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<MedicalRecordNumber>())).ReturnsAsync(true);
        _operationRequestRepoMock.Setup(repo => repo.ExistsAsync(It.IsAny<MedicalRecordNumber>(), It.IsAny<OperationTypeID>())).ReturnsAsync(false);

        var mockOperationRequest = new OperationRequest(
            new MedicalRecordNumber(createDto.pacientid),
            new LicenseNumber(createDto.doctorid),
            new OperationTypeID(createDto.operationTypeId),
            new Deadline(DateOnly.Parse(createDto.deadline)),
            createDto.priority
        );

        // Configura o mock para simular a adição de uma nova solicitação de operação
        _operationRequestRepoMock.Setup(repo => repo.AddAsync(It.IsAny<OperationRequest>())).ReturnsAsync(mockOperationRequest);
        
        // Configura o mock do UnitOfWork para simular a confirmação da transação
        _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.FromResult(1));

        // Execução do teste
        var result = await _service.CreateOperationRequest(createDto);

        // Verificações
        Assert.NotNull(result);
        Assert.IsType<OperationRequestDTO>(result);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }
}*/

    [Fact]
    public async Task DeleteOperationRequestOperationNotFoundThrowsException()
    {
        
        Guid invalidId = Guid.NewGuid();
        _operationRequestRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>())).ReturnsAsync((OperationRequest)null);

        var ex = await Assert.ThrowsAsync<Exception>(() => _service.DeleteOperationRequest(invalidId));
        Assert.Equal("Operation request not found.", ex.Message);
    }

    
    [Fact]
    public async Task DeleteOperationRequestValidIdReturnsTrue()
    {
       
        var validId = Guid.NewGuid();
        var operationRequest = new OperationRequest(
        new MedicalRecordNumber("412321af-71a4-4f87-95e4-56d25c7f443e"), 
        new LicenseNumber("43212bdf-71a4-4f87-95e4-56d25c7f443e"),       
        new OperationTypeID("d614fae8-d852-4c3d-9b71-3f176b6cb1fb"),    
        new Deadline(DateOnly.Parse("2024-12-14")),                      
        TodoApi.Models.Priority.ELECTIVE                                                
    );
        _operationRequestRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>())).ReturnsAsync(operationRequest);

       
        var result = await _service.DeleteOperationRequest(validId);

        
        Assert.True(result);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetOperationsReturnsListOfDtos()
    {
    
    var operations = new List<OperationRequest>
    {
        new OperationRequest(
            new MedicalRecordNumber("412321af-71a4-4f87-95e4-56d25c7f443e"),
            new LicenseNumber("43212bdf-71a4-4f87-95e4-56d25c7f443e"),
            new OperationTypeID("d614fae8-d852-4c3d-9b71-3f176b6cb1fb"),
            new Deadline(DateOnly.Parse("2024-12-14")),
            TodoApi.Models.Priority.ELECTIVE
        ),
        new OperationRequest(
            new MedicalRecordNumber("512321af-71a4-4f87-95e4-56d25c7f443e"),
            new LicenseNumber("53212bdf-71a4-4f87-95e4-56d25c7f443e"),
            new OperationTypeID("e314fae8-d852-4c3d-9b71-3f176b6cb1fb"),
            new Deadline(DateOnly.Parse("2024-12-18")),
            TodoApi.Models.Priority.URGENT
        )
    };

    _operationRequestRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(operations);

    
    var result = await _service.GetOperations();

    
    Assert.NotNull(result);
    Assert.Equal(2, result.Count);
    _operationRequestRepoMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateOperationRequestAsyncValidUpdateReturnsUpdatedDto()
    {
    var requestId = Guid.NewGuid();
    var existingRequest = new OperationRequest(
        new MedicalRecordNumber("412321af-71a4-4f87-95e4-56d25c7f443e"),
        new LicenseNumber("43212bdf-71a4-4f87-95e4-56d25c7f443e"),
        new OperationTypeID("d614fae8-d852-4c3d-9b71-3f176b6cb1fb"),
        new Deadline(DateOnly.Parse("2024-12-14")),
        TodoApi.Models.Priority.ELECTIVE
    );

    _operationRequestRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<OperationRequestID>())).ReturnsAsync(existingRequest);
    _operationRequestLogRepoMock.Setup(repo => repo.AddAsync(It.IsAny<RequestsLog>())).ReturnsAsync(new RequestsLog(new OperationRequestID(requestId.ToString()),
    DateTime.UtcNow, 
    "Alteração na data limite e prioridade da requisição de operação."  // Descrição da alteração
    ));
    _unitOfWorkMock.Setup(uow => uow.CommitAsync()).Returns(Task.FromResult(1));

    var updateDto = new UpdateOperationRequestDTO
    {
        Deadline = DateOnly.Parse("2024-12-20"),
        Priority = TodoApi.Models.Priority.URGENT
    };

    var updatedDto = await _service.UpdateOperationRequestAsync(requestId, updateDto);

    Assert.NotNull(updatedDto);
    Assert.Equal("URGENT", updatedDto.deadline.ToString());
    
    Assert.Equal("2024-12-20", updatedDto.priority.ToString());
    _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
}

}