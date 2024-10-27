using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApi.DTOs;
using System.Collections.Generic;
using System.Net;




namespace TodoApi.Tests.IntegrationTest.Controller.Staff
{
    public class StaffControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public StaffControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterStaff_ShouldReturnOk_WhenValidDtoIsProvided()
        {
            // Arrange
            var createDto = new CreateStaffDTO
            {
                FullName = "John Doe",
                Specialization = "Dermatology",
                Email = "john.doe@example.com",
                Phone = "123456789",
                AvailabilitySlots = new List<Slot>(),
                Status = StaffStatus.ACTIVE
            };

            var content = new StringContent(JsonSerializer.Serialize(createDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/staff/create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var staffDto = JsonSerializer.Deserialize<StaffDTO>(responseString);

            Assert.NotNull(staffDto);
            Assert.Equal("John Doe", staffDto.FullName);
        }

        [Fact]
        public async Task GetStaff_ShouldReturnOk_WhenCalled()
        {
            // Act
            var response = await _client.GetAsync("/api/staff");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var staffList = JsonSerializer.Deserialize<List<StaffDTO>>(responseString);

            Assert.NotNull(staffList);
        }

        [Fact]
        public async Task SearchBySpecialization_ShouldReturnOk_WhenValidSpecializationIsProvided()
        {
            // Arrange
            string specialization = "Dermatology";

            // Act
            var response = await _client.GetAsync($"/api/staff/search/specialization?specialization={specialization}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var staffList = JsonSerializer.Deserialize<List<StaffDTO>>(responseString);

            Assert.NotNull(staffList);
        }

        [Fact]
        public async Task UpdateStaff_ShouldReturnOk_WhenValidIdAndDtoAreProvided()
        {
            // Arrange
            var staffId = Guid.NewGuid(); // Use a valid ID from your database context
            var updateDto = new UpdateStaffDTO
            {
                FullName = "John Doe Updated",
                Email = "john.doe.updated@example.com",
                Phone = "1234567890",
                Specialization = "Dermatology",
                AvailabilitySlots = new List<SlotDTO>(),
                Status = StaffStatus.ACTIVE
            };

            var content = new StringContent(JsonSerializer.Serialize(updateDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/staff/update/{staffId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedStaffDto = JsonSerializer.Deserialize<StaffDTO>(responseString);

            Assert.NotNull(updatedStaffDto);
            Assert.Equal("John Doe Updated", updatedStaffDto.FullName);
        }

        [Fact]
        public async Task InactivateStaff_ShouldReturnOk_WhenValidIdIsProvided()
        {
            // Arrange
            var staffId = Guid.NewGuid(); // Use a valid ID from your database context
            var updateDto = new UpdateStaffDTO
            {
                FullName = "John Doe Updated",
                Email = "john.doe.updated@example.com",
                Phone = "1234567890",
                Specialization = "Dermatology",
                AvailabilitySlots = new List<SlotDTO>(),
                Status = StaffStatus.INACTIVE // Set status to inactive for inactivation
            };

            var content = new StringContent(JsonSerializer.Serialize(updateDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.DeleteAsync($"/api/staff/{staffId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var inactivatedStaffDto = JsonSerializer.Deserialize<StaffDTO>(responseString);

            Assert.NotNull(inactivatedStaffDto);
            Assert.Equal(StaffStatus.INACTIVE, inactivatedStaffDto.Status);
        }
    }
}
