using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Services;
using TodoApi.Infrastructure.Staff;
using TodoApi.Models.Shared;
using TodoApi.Models;


public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _staffRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<IStaffService>> _loggerMock;
    private readonly StaffService _staffService;

    public StaffServiceTests()
    {
        _staffRepositoryMock = new Mock<IStaffRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<IStaffService>>();
        _staffService = new StaffService(_unitOfWorkMock.Object, _staffRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateStaff_WithDuplicateEmail_ShouldThrowException()
    {
        var createDto = new CreateStaffDTO { Email = "duplicate@example.com", Phone = "967270345" };
        _staffRepositoryMock.Setup(repo => repo.SearchByEmail(It.IsAny<string>())).ReturnsAsync(new List<Staff> { new Staff() });

        await Assert.ThrowsAsync<Exception>(() => _staffService.CreateStaff(createDto));
    }

   [Fact]
   public async Task CreateStaff_WithDuplicatePhone_ShouldThrowException()
   {
       var createDto = new CreateStaffDTO { Email = "niu@example.com", Phone = "912321453" };
       _staffRepositoryMock.Setup(repo => repo.GetByPhoneAsync(It.IsAny<string>())).ReturnsAsync(new Staff());

       // Ajusta o tipo da exceção esperada para ArgumentNullException se for mais específico
       await Assert.ThrowsAsync<ArgumentNullException>(() => _staffService.CreateStaff(createDto));
   }
/*
    [Fact]
    public async Task CreateStaff_ShouldAddStaff_WhenValidDataProvided()
    {
        var createDto = new CreateStaffDTO { Email = "unique@example.com", Phone = "933285395" };
        _staffRepositoryMock.Setup(repo => repo.SearchByEmail(It.IsAny<string>())).ReturnsAsync(new List<Staff>());
        _staffRepositoryMock.Setup(repo => repo.GetByPhoneAsync(It.IsAny<string>())).ReturnsAsync((Staff)null);

        var result = await _staffService.CreateStaff(createDto);

        Assert.Equal(createDto.Email, result.Email);
    }

    /*[Fact]
    public async Task UpdateStaff_ShouldUpdateExistingStaff()
    {
        var updateDto = new UpdateStaffDTO
        {
            FullName = "Updated Name",
            Phone = "917654321",
            Email = "updated@example.com",
            Specialization = "Cardiology",
            Status = StaffStatus.ACTIVE
        };

        var staffId = Guid.NewGuid();
        var licenseNumber = new LicenseNumber(staffId.ToString());

        var existingStaff = new Staff(
            new FullName("John Doe"),
            new Specialization("Dermatology"),
            new UserEmail("john.doe@example.com"),
            new Phone("933456789"),
            StaffStatus.ACTIVE
        );

        // Configura o Mock para garantir que `existingStaff` não seja `null`
        _staffRepositoryMock
            .Setup(repo => repo.GetByIdAsync(licenseNumber))
            .ReturnsAsync(existingStaff);

        var result = await _staffService.UpdateStaff(staffId, updateDto);

        Assert.Equal(updateDto.FullName, result.FullName);
        Assert.Equal(updateDto.Phone, result.Phone);
        Assert.Equal(updateDto.Email, result.Email);
        Assert.Equal(updateDto.Specialization, result.Specialization);
        Assert.Equal(updateDto.Status, result.Status);
    }*/


    [Fact]
    public async Task InactivateStaff_ShouldUpdateStatusToInactive()
    {
        var staffId = Guid.NewGuid();
        var existingStaff = new Staff(new FullName("John Doe"), new Specialization("Dermatology"), new UserEmail("john.doe@example.com"), new Phone("963456789"), StaffStatus.ACTIVE);
        _staffRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<LicenseNumber>())).ReturnsAsync(existingStaff);

        var result = await _staffService.InactivateStaff(staffId, new UpdateStaffDTO());

        Assert.Equal(StaffStatus.INACTIVE, result.Status);
    }
}
