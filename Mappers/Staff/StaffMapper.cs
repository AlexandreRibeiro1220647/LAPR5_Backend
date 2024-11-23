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

        public Staff toEntity(CreateStaffDTO createDto, TodoApi.DTOs.User.UserDTO user)
        {
            List<Slot> availabilitySlots = new List<Slot>();

            foreach (var slot in createDto.AvailabilitySlots)
            {
                var availabilitySlot = new Slot(slot.StartTime, slot.EndTime);
                availabilitySlots.Add(availabilitySlot);
            }

            return new Staff(
                new Specialization(createDto.Specialization),
                new Phone(createDto.Phone),
                new AvailabilitySlots(availabilitySlots),
                createDto.Status,
                user
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
                new Specialization(dto.Specialization),
                new Phone(dto.Phone),
                availabilitySlotsObj,
                dto.Status,
                dto.User
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

            return new StaffDTO(
                entity.Specialization.Area,
                entity.Id.AsString(),
                entity.Phone.phoneNumber,
                availabilitySlotDTOs,
                entity.Status,
                entity.user
            );
        }
    }
}
