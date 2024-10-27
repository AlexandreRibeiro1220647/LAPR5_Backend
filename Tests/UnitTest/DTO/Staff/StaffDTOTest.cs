using System;
using System.Collections.Generic;
using TodoApi.DTOs;
using TodoApi.Models.Staff;
using Xunit;

namespace TodoApi.Tests.UniTest.DTO.Staff
{
    public class StaffDTOTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var fullName = "Jane Smith";
            var specialization = "Cardiology";
            var licenseNumber = "ABC123";
            var email = "janesmith@example.com";
            var phone = "987654321";
            var availabilitySlots = new List<Slot>
            {
                new Slot(DateTime.Now, DateTime.Now.AddHours(1))
            };
            var status = StaffStatus.ACTIVE;

            // Act
            var dto = new StaffDTO(fullName, specialization, licenseNumber, email, phone, availabilitySlots, status);

            // Assert
            Assert.Equal(fullName, dto.FullName);
            Assert.Equal(specialization, dto.Specialization);
            Assert.Equal(licenseNumber, dto.LicenseNumber);
            Assert.Equal(email, dto.Email);
            Assert.Equal(phone, dto.Phone);
            Assert.Equal(availabilitySlots, dto.AvailabilitySlots);
            Assert.Equal(status, dto.Status);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaults()
        {
            // Act
            var dto = new StaffDTO();

            // Assert
            Assert.Null(dto.FullName);
            Assert.Null(dto.Specialization);
            Assert.Null(dto.LicenseNumber);
            Assert.Null(dto.Email);
            Assert.Null(dto.Phone);
            Assert.Null(dto.AvailabilitySlots);
            Assert.Equal(StaffStatus.INACTIVE, dto.Status); // Definindo INACTIVE como padrão para o status
        }

        [Fact]
        public void AvailabilitySlots_ShouldAllowAddingSlots()
        {
            // Arrange
            var dto = new StaffDTO();
            var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));

            // Act
            dto.AvailabilitySlots = new List<Slot> { slot };

            // Assert
            Assert.Single(dto.AvailabilitySlots);
            Assert.Equal(slot, dto.AvailabilitySlots[0]);
        }

        [Fact]
        public void SetStatus_ShouldUpdateStatusProperty()
        {
            // Arrange
            var dto = new StaffDTO();

            // Act
            dto.Status = StaffStatus.ACTIVE;

            // Assert
            Assert.Equal(StaffStatus.ACTIVE, dto.Status);
        }
    }
}
