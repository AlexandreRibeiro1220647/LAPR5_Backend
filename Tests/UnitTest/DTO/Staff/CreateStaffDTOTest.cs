using System;
using System.Collections.Generic;
using TodoApi.DTOs;
using TodoApi.Models.Staff;
using Xunit;

namespace TodoApi.Tests.UniTest.DTO.Staff
{
    public class CreateStaffDTOTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var fullName = "John Doe";
            var specialization = "Dermatology";
            var email = "johndoe@example.com";
            var phone = "123456789";
            var availabilitySlots = new List<Slot>
            {
                new Slot(DateTime.Now, DateTime.Now.AddHours(1))
            };
            var status = StaffStatus.ACTIVE;

            // Act
            var dto = new CreateStaffDTO(fullName, specialization, email, phone, availabilitySlots, status);

            // Assert
            Assert.Equal(fullName, dto.FullName);
            Assert.Equal(specialization, dto.Specialization);
            Assert.Equal(email, dto.Email);
            Assert.Equal(phone, dto.Phone);
            Assert.Equal(availabilitySlots, dto.AvailabilitySlots);
            Assert.Equal(status, dto.Status);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaults()
        {
            // Act
            var dto = new CreateStaffDTO();

            // Assert
            Assert.Null(dto.FullName);
            Assert.Null(dto.Specialization);
            Assert.Null(dto.Email);
            Assert.Null(dto.Phone);
            Assert.Null(dto.AvailabilitySlots);
            Assert.Equal(StaffStatus.INACTIVE, dto.Status); // Definir INACTIVE como padrão
        }

        [Fact]
        public void AvailabilitySlots_ShouldAllowAddingSlots()
        {
            // Arrange
            var dto = new CreateStaffDTO();
            var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));

            // Act
            dto.AvailabilitySlots = new List<Slot> { slot };

            // Assert
            Assert.Single(dto.AvailabilitySlots);
            Assert.Equal(slot, dto.AvailabilitySlots[0]);
        }
    }
}
