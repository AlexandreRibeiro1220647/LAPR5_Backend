using Moq;
using Xunit;
using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Services;
using TodoApi.Infrastructure.Staff;
using Microsoft.Extensions.Logging;
using TodoApi.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Tests.IntegrationTest.Services.Staff
{

public class StaffServiceIntegrationTests
{
    private readonly StaffService _staffService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IStaffRepository> _mockStaffRepository;
    private readonly Mock<ILogger<IStaffService>> _mockLogger;

    public StaffServiceIntegrationTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockStaffRepository = new Mock<IStaffRepository>();
        _mockLogger = new Mock<ILogger<IStaffService>>();
        _staffService = new StaffService(_mockUnitOfWork.Object, _mockStaffRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CreateStaff_ShouldAddStaff_WhenValidDtoIsProvided()
    {
        // Arrange
        var createDto = new CreateStaffDTO(
            "John Doe",
            "Dermatology",
            "john.doe@example.com",
            "123456789",
            new List<Slot>(),
            StaffStatus.ACTIVE
        );

        var staff = new Staff(
            new FullName(createDto.FullName),
            new Specialization(createDto.Specialization),
            new UserEmail(createDto.Email),
            new Phone(createDto.Phone),
            new AvailabilitySlots(new List<Slot>()),
            createDto.Status
        );

        _mockStaffRepository.Setup(repo => repo.AddAsync(staff)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _staffService.CreateStaff(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.FullName);
        Assert.Equal("Dermatology", result.Specialization);
        _mockStaffRepository.Verify(repo => repo.AddAsync(It.IsAny<Staff>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task GetStaff_ShouldReturnAllStaff()
    {
        // Arrange
        var staffList = new List<Staff>
        {
            new Staff(new FullName("John Doe"), new Specialization("Dermatology"), new UserEmail("john.doe@example.com"), new Phone("123456789"), new AvailabilitySlots(new List<Slot>()), StaffStatus.ACTIVE),
            new Staff(new FullName("Jane Smith"), new Specialization("Pediatrics"), new UserEmail("jane.smith@example.com"), new Phone("987654321"), new AvailabilitySlots(new List<Slot>()), StaffStatus.ACTIVE)
        };

        _mockStaffRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(staffList);

        // Act
        var result = await _staffService.GetStaff();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("John Doe", result[0].FullName);
        Assert.Equal("Jane Smith", result[1].FullName);
    }

    [Fact]
    public async Task UpdateStaff_ShouldUpdateStaff_WhenValidDtoIsProvided()
    {
        // Arrange
        var staffId = Guid.NewGuid();
        var existingStaff = new Staff(
            new FullName("John Doe"),
            new Specialization("Dermatology"),
            new UserEmail("john.doe@example.com"),
            new Phone("123456789"),
            new AvailabilitySlots(new List<Slot>()),
            StaffStatus.ACTIVE
        );

        var updateDto = new UpdateStaffDTO
        {
            FullName = "John Doe Updated",
            Email = "john.doe.updated@example.com",
            Phone = "1234567890",
            Specialization = "Dermatology",
            AvailabilitySlots = new List<SlotDTO>(),
            Status = StaffStatus.ACTIVE
        };

        _mockStaffRepository.Setup(repo => repo.GetByIdAsync(new LicenseNumber(staffId.ToString()))).ReturnsAsync(existingStaff);
        _mockUnitOfWork.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _staffService.UpdateStaff(staffId, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe Updated", result.FullName);
        _mockStaffRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<LicenseNumber>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
    }
}
}