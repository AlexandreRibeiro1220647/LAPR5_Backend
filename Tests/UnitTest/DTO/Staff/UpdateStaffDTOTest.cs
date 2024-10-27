using System;
using System.Collections.Generic;
using TodoApi.DTOs;
using TodoApi.Models.Staff;
using Xunit;

namespace TodoApi.Tests.UniTest.DTO.Staff
{
    public class UpdateStaffDTOTests
    {
        [Fact]
        public void Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var fullName = "John Doe";
            var email = "johndoe@example.com";
            var phone = "123456789";
            var specialization = "Neurology";
            var availabilitySlots = new List<SlotDTO>
            {
                new SlotDTO { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) }
            };
            var status = StaffStatus.ACTIVE;

            // Act
            var dto = new UpdateStaffDTO
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                Specialization = specialization,
                AvailabilitySlots = availabilitySlots,
                Status = status
            };

            // Assert
            Assert.Equal(fullName, dto.FullName);
            Assert.Equal(email, dto.Email);
            Assert.Equal(phone, dto.Phone);
            Assert.Equal(specialization, dto.Specialization);
            Assert.Equal(availabilitySlots, dto.AvailabilitySlots);
            Assert.Equal(status, dto.Status);
        }

        [Fact]
        public void AvailabilitySlots_ShouldAllowAddingSlotDTOs()
        {
            // Arrange
            var dto = new UpdateStaffDTO();
            var slotDto = new SlotDTO { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };

            // Act
            dto.AvailabilitySlots = new List<SlotDTO> { slotDto };

            // Assert
            Assert.Single(dto.AvailabilitySlots);
            Assert.Equal(slotDto, dto.AvailabilitySlots[0]);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaults()
        {
            // Act
            var dto = new UpdateStaffDTO();

            // Assert
            Assert.Null(dto.FullName);
            Assert.Null(dto.Email);
            Assert.Null(dto.Phone);
            Assert.Null(dto.Specialization);
            Assert.Null(dto.AvailabilitySlots);
            Assert.Equal(StaffStatus.INACTIVE, dto.Status); // Valor padrão sugerido
        }

        [Fact]
        public void SetStatus_ShouldUpdateStatusProperty()
        {
            // Arrange
            var dto = new UpdateStaffDTO();

            // Act
            dto.Status = StaffStatus.ACTIVE;

            // Assert
            Assert.Equal(StaffStatus.ACTIVE, dto.Status);
        }
    }

    public class SlotDTOTests
    {
        [Fact]
        public void SlotDTO_ShouldSetStartAndEndTimeCorrectly()
        {
            // Arrange
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(1);

            // Act
            var slotDto = new SlotDTO { StartTime = startTime, EndTime = endTime };

            // Assert
            Assert.Equal(startTime, slotDto.StartTime);
            Assert.Equal(endTime, slotDto.EndTime);
        }
    }
}
