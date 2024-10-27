using System.Threading.Tasks;
using TodoApi.Infrastructure;
using TodoApi.Services;
using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Mappers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace TodoApi.Tests.UniTest.Services.Staff
{
    public class StaffServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IStaffRepository> _mockStaffRepository;
        private readonly Mock<ILogger<IStaffService>> _mockLogger;
        private readonly StaffService _staffService;

        public StaffServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockStaffRepository = new Mock<IStaffRepository>();
            _mockLogger = new Mock<ILogger<IStaffService>>();
            _staffService = new StaffService(_mockUnitOfWork.Object, _mockStaffRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateStaff_ValidDto_ReturnsStaffDto()
        {
            // Arrange
            var createDto = new CreateStaffDTO
            {
                FullName = "John Doe",
                Specialization = "Cardiology",
                Email = "johndoe@example.com",
                Phone = "123456789"
            };
            var staff = _mapper.ToEntity(createDto);
            _staffRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Staff>())).Returns(Task.CompletedTask);

            // Act
            var result = await _staffService.CreateStaff(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createDto.FullName, result.FullName);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _staffRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Staff>()), Times.Once);
        }

        [Fact]
        public async Task GetStaff_ReturnsListOfStaffDtos()
        {
            // Arrange
            var staffList = new List<Staff>
            {
                new Staff(new FullName("John Doe"), new Specialization("Cardiology"),
                          new UserEmail("johndoe@example.com"), new Phone("123456789"), StaffStatus.ACTIVE)
            };
            _staffRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffList);

            // Act
            var result = await _staffService.GetStaff();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result[0].FullName);
            _staffRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateStaff_ValidIdAndDto_ReturnsUpdatedStaffDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingStaff = new Staff(new FullName("John Doe"), new Specialization("Cardiology"),
                                          new UserEmail("johndoe@example.com"), new Phone("123456789"), StaffStatus.ACTIVE);
            var updateDto = new UpdateStaffDTO
            {
                FullName = "John Smith",
                Phone = "987654321",
                Email = "johnsmith@example.com",
                Specialization = "Dermatology",
                Status = StaffStatus.INACTIVE
            };
            _staffRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<LicenseNumber>())).ReturnsAsync(existingStaff);

            // Act
            var result = await _staffService.UpdateStaff(id, updateDto);

            // Assert
            Assert.Equal(updateDto.FullName, result.FullName);
            Assert.Equal(updateDto.Status, result.Status);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task InactivateStaff_ValidId_InactivatesStaff()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingStaff = new Staff(new FullName("John Doe"), new Specialization("Cardiology"),
                                          new UserEmail("johndoe@example.com"), new Phone("123456789"), StaffStatus.ACTIVE);
            _staffRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<LicenseNumber>())).ReturnsAsync(existingStaff);

            // Act
            var result = await _staffService.InactivateStaff(id, null);

            // Assert
            Assert.Equal(StaffStatus.INACTIVE, result.Status);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GetStaffBySpecialization_ReturnsFilteredStaff()
        {
            // Arrange
            var specialization = "Cardiology";
            var staffList = new List<Staff>
            {
                new Staff(new FullName("John Doe"), new Specialization("Cardiology"),
                          new UserEmail("johndoe@example.com"), new Phone("123456789"), StaffStatus.ACTIVE)
            };
            _staffRepositoryMock.Setup(repo => repo.SearchBySpecialization(specialization)).ReturnsAsync(staffList);

            // Act
            var result = await _staffService.GetStaffBySpecialization(specialization);

            // Assert
            Assert.Single(result);
            Assert.Equal(specialization, result[0].Specialization);
            _staffRepositoryMock.Verify(repo => repo.SearchBySpecialization(specialization), Times.Once);
        }

        [Fact]
        public async Task GetStaffByEmail_ReturnsFilteredStaff()
        {
            // Arrange
            var email = "johndoe@example.com";
            var staffList = new List<Staff>
            {
                new Staff(new FullName("John Doe"), new Specialization("Cardiology"),
                          new UserEmail(email), new Phone("123456789"), StaffStatus.ACTIVE)
            };
            _staffRepositoryMock.Setup(repo => repo.SearchByEmail(email)).ReturnsAsync(staffList);

            // Act
            var result = await _staffService.GetStaffByEmail(email);

            // Assert
            Assert.Single(result);
            Assert.Equal(email, result[0].Email);
            _staffRepositoryMock.Verify(repo => repo.SearchByEmail(email), Times.Once);
        }
    }
}