/*using Moq;
using TodoApi.DTOs.OperationType;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Mappers.OperationType;
using TodoApi.Models.OperationType;
using TodoApi.Models.Shared;
using TodoApi.Services;

namespace TodoApi.Tests.Services
{
    public class OperationTypeServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOperationTypeRepository> _operationTypeRepositoryMock;
        private readonly Mock<ILogger<IPatientService>> _loggerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly OperationTypeService _operationTypeService;
        private readonly OperationTypeMapper _mapper;

        public OperationTypeServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _operationTypeRepositoryMock = new Mock<IOperationTypeRepository>();
            _loggerMock = new Mock<ILogger<IPatientService>>();
            _configMock = new Mock<IConfiguration>();
            _mapper = new OperationTypeMapper();

            _operationTypeService = new OperationTypeService(
                _unitOfWorkMock.Object,
                _operationTypeRepositoryMock.Object,
                _loggerMock.Object,
                _configMock.Object);
        }

        [Fact]
        public async Task CreateOperationType_ShouldReturnMappedOperationTypeDTO_WhenOperationTypeIsValid()
        {
            // Arrange
            var createOperationTypeDto = new CreateOperationTypeDTO { Name = "New Operation" };
            var operationType = new OperationType(createOperationTypeDto.Name, new List<string>(), new TimeSpan(100));
            var expectedOperationTypeDto = new OperationTypeDTO(operationType.Name, new List<string>(), new TimeSpan(100), operationType.Id.AsString());

            _operationTypeRepositoryMock
                .Setup(repo => repo.GetByNameAsync(createOperationTypeDto.Name))
                .ReturnsAsync((OperationType)null); // No existing operation type

            _operationTypeRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Models.OperationType.OperationType>()))
                .ReturnsAsync(operationType);

            _unitOfWorkMock.Setup(uow => uow.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _operationTypeService.CreateOperationType(createOperationTypeDto);

            // Assert
            Assert.Equal(expectedOperationTypeDto.Name, result.Name);

            _operationTypeRepositoryMock.Verify(repo => repo.GetByNameAsync(createOperationTypeDto.Name), Times.Once);
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Models.OperationType.OperationType>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOperationType_ShouldThrowException_WhenOperationTypeWithSameNameExists()
        {
            // Arrange
            var createOperationTypeDto = new CreateOperationTypeDTO { Name = "Existing Operation" };
            var existingOperationType = new Models.OperationType.OperationType(createOperationTypeDto.Name, new List<string>(), new TimeSpan(100));

            _operationTypeRepositoryMock
                .Setup(repo => repo.GetByNameAsync(createOperationTypeDto.Name))
                .ReturnsAsync(existingOperationType); // Existing operation type found

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _operationTypeService.CreateOperationType(createOperationTypeDto));
            Assert.Equal("An operation type with the same name already exists.", exception.Message);

            _operationTypeRepositoryMock.Verify(repo => repo.GetByNameAsync(createOperationTypeDto.Name), Times.Once);
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Models.OperationType.OperationType>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateOperationType_ShouldThrowException_WhenExceptionIsThrown()
        {
            // Arrange
            var createOperationTypeDto = new CreateOperationTypeDTO { Name = "New Operation" };
            var exceptionMessage = "Database Error";

            _operationTypeRepositoryMock
                .Setup(repo => repo.GetByNameAsync(createOperationTypeDto.Name))
                .ReturnsAsync((Models.OperationType.OperationType)null); // No existing operation type

            _operationTypeRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Models.OperationType.OperationType>()))
                .ThrowsAsync(new Exception(exceptionMessage)); // Simulating exception during adding

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _operationTypeService.CreateOperationType(createOperationTypeDto));
            Assert.Equal(exceptionMessage, exception.Message);

            _operationTypeRepositoryMock.Verify(repo => repo.GetByNameAsync(createOperationTypeDto.Name), Times.Once);
            _operationTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Models.OperationType.OperationType>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }
    }
}
*/