using System;
using System.Collections.Generic;
using TodoApi.DTOs;
using TodoApi.Mappers;
using TodoApi.Models.Staff;
using Xunit;

namespace TodoApi.Tests.UniTest.Mappers.Staff
{
    public class StaffMapperTests
    {
        private readonly StaffMapper _mapper;

        public StaffMapperTests()
        {
            _mapper = new StaffMapper();
        }

        [Fact]
        public void ToEntity_FromCreateStaffDTO_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var createDto = new CreateStaffDTO
            {
                FullName = "John Doe",
                Specialization = "Dermatology",
                Email = "johndoe@example.com",
                Phone = "123456789",
                Status = StaffStatus.ACTIVE,
                AvailabilitySlots = new List<SlotDTO>
                {
                    new SlotDTO { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) }
                }
            };

            // Act
            var staff = _mapper.ToEntity(createDto);

            // Assert
            Assert.Equal(createDto.FullName, staff.FullName.fullName);
            Assert.Equal(createDto.Specialization, staff.Specialization.Area);
            Assert.Equal(createDto.Email, staff.Email.Value);
            Assert.Equal(createDto.Phone, staff.Phone.phoneNumber);
            Assert.Equal(createDto.Status, staff.Status);
            Assert.Single(staff.AvailabilitySlots.Slots);
        }

        [Fact]
        public void ToEntity_FromStaffDTO_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var dto = new StaffDTO
            {
                FullName = "Jane Doe",
                Specialization = "Neurology",
                Email = "janedoe@example.com",
                Phone = "987654321",
                Status = StaffStatus.INACTIVE,
                AvailabilitySlots = new List<Slot>
                {
                    new Slot(DateTime.Now, DateTime.Now.AddHours(1))
                }
            };

            // Act
            var staff = _mapper.ToEntity(dto);

            // Assert
            Assert.Equal(dto.FullName, staff.FullName.fullName);
            Assert.Equal(dto.Specialization, staff.Specialization.Area);
            Assert.Equal(dto.Email, staff.Email.Value);
            Assert.Equal(dto.Phone, staff.Phone.phoneNumber);
            Assert.Equal(dto.Status, staff.Status);
            Assert.Single(staff.AvailabilitySlots.Slots);
        }

        [Fact]
        public void ToDto_FromStaffEntity_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var staff = new Staff(
                new FullName("John Smith"),
                new Specialization("Cardiology"),
                new UserEmail("johnsmith@example.com"),
                new Phone("1122334455"),
                new AvailabilitySlots(new List<Slot>
                {
                    new Slot(DateTime.Now, DateTime.Now.AddHours(1))
                }),
                StaffStatus.ACTIVE
            );

            // Act
            var dto = _mapper.ToDto(staff);

            // Assert
            Assert.Equal(staff.FullName.fullName, dto.FullName);
            Assert.Equal(staff.Specialization.Area, dto.Specialization);
            Assert.Equal(staff.Email.Value, dto.Email);
            Assert.Equal(staff.Phone.phoneNumber, dto.Phone);
            Assert.Equal(staff.Status, dto.Status);
            Assert.Single(dto.AvailabilitySlots);
        }
    }
}
