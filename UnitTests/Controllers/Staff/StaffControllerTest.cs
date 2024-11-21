/*using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.DTOs;
using TodoApi.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models.Staff;

namespace TodoApi.Tests.Controllers
{
    public class StaffControllerTests
    {
        private readonly Mock<IStaffService> _mockStaffService;
        private readonly StaffController _controller;

        public StaffControllerTests()
        {
            _mockStaffService = new Mock<IStaffService>();
            _controller = new StaffController(_mockStaffService.Object);
        }

        [Fact]
        public async Task RegisterStaff_ShouldReturnOk_WhenValidDataIsProvided()
        {
            // Arrange
            var createDto = new CreateStaffDTO { Email = "test@example.com", Phone = "967270354", FullName = "Test User", Specialization = "Orthopedics" };
            var expectedStaffDto = new StaffDTO { Email = "test@example.com", Phone = "968011048", FullName = "Test User", Specialization = "Orthopedics" };

            _mockStaffService.Setup(s => s.CreateStaff(createDto)).ReturnsAsync(expectedStaffDto);

            // Act
            var result = await _controller.RegisterStaff(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualStaffDto = Assert.IsType<StaffDTO>(okResult.Value);
            Assert.Equal(expectedStaffDto.Email, actualStaffDto.Email);
        }

        [Fact]
        public async Task RegisterStaff_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var createDto = new CreateStaffDTO();
            _mockStaffService.Setup(s => s.CreateStaff(createDto)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.RegisterStaff(createDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task GetStaff_ShouldReturnOk_WhenStaffExists()
        {
            // Arrange
            var staffList = new List<StaffDTO>
            {
                new StaffDTO { Email = "test@example.com", Phone = "933285398", FullName = "Test User", Specialization = "Neurology" }
            };
            _mockStaffService.Setup(s => s.GetStaff()).ReturnsAsync(staffList);

            // Act
            var result = await _controller.GetStaff();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualStaffList = Assert.IsAssignableFrom<List<StaffDTO>>(okResult.Value);
            Assert.Single(actualStaffList);
        }

        [Fact]
        public async Task GetStaff_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            _mockStaffService.Setup(s => s.GetStaff()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.GetStaff();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateStaff_ShouldReturnOk_WhenValidDataIsProvided()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateStaffDTO { FullName = "Updated Name", Phone = "987654321", Email = "updated@example.com" };
            var updatedStaffDto = new StaffDTO { Email = "updated@example.com", Phone = "987654321", FullName = "Updated Name" };

            _mockStaffService.Setup(s => s.UpdateStaff(id, updateDto)).ReturnsAsync(updatedStaffDto);

            // Act
            var result = await _controller.UpdateStaff(id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUpdatedStaff = Assert.IsType<StaffDTO>(okResult.Value);
            Assert.Equal(updatedStaffDto.Email, actualUpdatedStaff.Email);
        }

        [Fact]
        public async Task UpdateStaff_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateStaffDTO();
            _mockStaffService.Setup(s => s.UpdateStaff(id, updateDto)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.UpdateStaff(id, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task InactivateStaff_ShouldReturnOk_WhenValidIdIsProvided()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateStaffDTO { Status = StaffStatus.INACTIVE };
            var updatedStaffDto = new StaffDTO { Status = StaffStatus.INACTIVE };

            _mockStaffService.Setup(s => s.InactivateStaff(id, updateDto)).ReturnsAsync(updatedStaffDto);

            // Act
            var result = await _controller.InactivateStaff(id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUpdatedStaff = Assert.IsType<StaffDTO>(okResult.Value);
            Assert.Equal(StaffStatus.INACTIVE, actualUpdatedStaff.Status);
        }

        [Fact]
        public async Task InactivateStaff_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateStaffDTO();
            _mockStaffService.Setup(s => s.InactivateStaff(id, updateDto)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.InactivateStaff(id, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
    }
}
*/