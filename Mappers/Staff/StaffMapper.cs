using TodoApi.Models.Staff;
using TodoApi.DTOs;
using System.Collections.Generic;
using TodoApi.Models;
using TodoApi.Models.Shared;

namespace TodoApi.Mappers
{
    public class StaffMapper : IStaffMapper
    {
        public StaffMapper() { }

        public Staff ToEntity(CreateStaffDTO createDto)
        {
            List<Slot> availabilitySlots = new List<Slot>();

            foreach (var slot in createDto.AvailabilitySlots)
            {
                var availabilitySlot = new Slot(slot.StartTime, slot.EndTime);
                availabilitySlots.Add(availabilitySlot);
            }

            var availabilitySlotsObj = new AvailabilitySlots(availabilitySlots);

            return new Staff(
                new FullName(createDto.FullName)
                new Specialization(createDto.Specialization),
                new Phone(createDto.Phone),
                availabilitySlotsObj,
                createDto.Status,
                createDto.UserId,
                createDto.Role 
            );
        }

        public Staff ToEntity(StaffDTO dto)
        {
            List<Slot> availabilitySlots = new List<Slot>();

            foreach (var slot in dto.AvailabilitySlots)
            {
                var availabilitySlot = new Slot(slot.StartTime, slot.EndTime);
                availabilitySlots.Add(availabilitySlot);
            }

            var availabilitySlotsObj = new AvailabilitySlots(availabilitySlots);

            return new Staff(
                new FullName(dto.FullName), 
                new Specialization(dto.Specialization),
                new Phone(dto.Phone),
                availabilitySlotsObj,
                dto.Status,
                dto.UserId,
                dto.Role 
            );
        }

        public StaffDTO ToDto(Staff entity)
        {
            List<Slot> availabilitySlotDTOs = new List<Slot>();

            foreach (var slot in entity.AvailabilitySlots.Slots)
            {
                var availabilitySlotDTO = new Slot
                {
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                };
                availabilitySlotDTOs.Add(availabilitySlotDTO);
            }

            return new StaffDTO
            {
                FullName = entity.FullName.Value,
                Specialization = entity.Specialization.Value,
                LicenseNumber = entity.Id.Value,
                Phone = entity.Phone.Value,
                AvailabilitySlots = availabilitySlotDTOs,
                Status = entity.Status,
                UserId = entity.UserId,
                Role = entity.Role 
            };
        }

        public Staff toEntity(CreateStaffDTO createDto)
        {
            throw new NotImplementedException();
        }
    }
}