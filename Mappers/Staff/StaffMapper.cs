using TodoApi.Models.Staff;
using TodoApi.DTOs;
using System.Collections.Generic;
using TodoApi.Models;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff.FullName;

namespace TodoApi.Mappers
{
    public class StaffMapper : IStaffMapper
    {
        public StaffMapper() { }

        public Staff ToEntity(CreateStaffDTO createDto)
        {
            List<Slot> availabilitySlots = new List<Slot>();

            foreach (var slot in createDto.availabilitySlots)
            {
                var availabilitySlot = new Slot(slot.StartTime, slot.EndTime);
                availabilitySlots.Add(availabilitySlot);
            }

            return new Staff(
                new FullName(createDto.fullName),
                new Specialization(createDto.specialization),
                new UserEmail(createDto.email),
                new Phone(createDto.phone),
                new AvailabilitySlots(availabilitySlots),
                (StaffStatus)Enum.Parse(typeof(StaffStatus), createDto.status)
            );
        }
        public Staff ToEntity(StaffDTO dto)
        {
            List<Slot> availabilitySlots = new List<Slot>();

            foreach (var slot in dto.availabilitySlots)
            {
                var availabilitySlot = new Slot(slot.StartTime, slot.EndTime);
                availabilitySlots.Add(availabilitySlot);
            }

            var availabilitySlotsObj = new AvailabilitySlots(availabilitySlots);

            return new Staff(
                new FullName(dto.fullName),
                new Specialization(dto.specialization),
                new UserEmail(dto.email),
                new Phone(dto.phone),
                availabilitySlotsObj,
                (StaffStatus)Enum.Parse(typeof(StaffStatus), dto.status)
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
                entity.Status.ToString()
            );
        }

        public Staff toEntity(CreateStaffDTO createDto)
        {
            throw new NotImplementedException();
        }
    }
}