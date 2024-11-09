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

            return new Staff(
                new FullName(createDto.FullName),
                new Specialization(createDto.Specialization),
                new UserEmail(createDto.Email),
                new Phone(createDto.Phone),
                new AvailabilitySlots(availabilitySlots),
                createDto.Status
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
                new UserEmail(dto.Email),
                new Phone(dto.Phone),
                availabilitySlotsObj,
                dto.Status
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
                entity.FullName.fullName,
                entity.Specialization.Area,
                entity.Id.AsString(),
                entity.Email.Value,
                entity.Phone.phoneNumber,
                availabilitySlotDTOs,
                entity.Status
            );
        }

        public Staff toEntity(CreateStaffDTO createDto)
        {
            throw new NotImplementedException();
        }
    }
}