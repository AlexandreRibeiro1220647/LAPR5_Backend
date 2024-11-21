using TodoApi.Models.Shared;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;
using TodoApi.Models;
using TodoApi.DTOs;
using TodoApi.Mappers;
using TodoApi.Infrastructure.Staff;
using Microsoft.Extensions.Logging;
using TodoApi.Services.User;
using TodoApi.Models.User;
using TodoApi.Infrastructure;
using TodoApi.Services.User;

namespace TodoApi.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        private readonly ILogger<IStaffService> _logger;
        private StaffMapper _mapper = new StaffMapper();
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, ILogger<IStaffService> logger, IUserService userService, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _staffRepository = staffRepository;
            _logger = logger;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<StaffDTO> CreateStaff(CreateStaffDTO dto)
        {
            try
            {
                TodoApi.DTOs.User.UserDTO user = await _userService.CreateUser(new DTOs.User.RegisterUserDTO(dto.FullName, dto.Email, dto.Role)
                );

                Staff mapped = _mapper.toEntity(dto, user);
                await _staffRepository.AddAsync(mapped);
                StaffDTO mappedDto = _mapper.ToDto(mapped);
                await _unitOfWork.CommitAsync();
                return mappedDto;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error registering the staff member");
                throw;
            }
        }


        public async Task<List<StaffDTO>> GetStaff()
        {
            List<Staff> staffList = await _staffRepository.GetAllAsync();

            return staffList.Select(staff => _mapper.ToDto(staff)).ToList();
        }



        public async Task<StaffDTO> UpdateStaff(Guid id, UpdateStaffDTO dto)
        {
            try
            {
                // Obtém o staff existente
                Staff existingStaff = await _staffRepository.GetByIdAsync(new LicenseNumber(id.ToString()));
                TodoApi.Models.User.User user = await _userRepository.GetByIdAsync(new UserID(existingStaff.user.Id));

                if (existingStaff == null)
                {
                    throw new Exception("Staff member not found");
                }

                // Obtém o usuário associado

                // Verifica se o email já está em uso por outro staff
                var existingStaffWithEmail = await _staffRepository.SearchAsync(email: dto.Email);
                if (existingStaffWithEmail.Any(e => e.Id != existingStaff.Id))
                {
                    throw new Exception("O email já está em uso.");
                }

                // Verifica se o telefone já está em uso por outro staff
                var existingStaffWithPhone = await _staffRepository.SearchAsync(phone: dto.Phone);
                if (existingStaffWithPhone.Any(e => e.Id != existingStaff.Id))
                {
                    throw new Exception("O número de telefone já está em uso.");
                }

                // Atualiza os dados do staff
                existingStaff.UpdatePhone(dto.Phone);
                existingStaff.UpdateSpecialization(new string(dto.Specialization));
                existingStaff.UpdateStatus(dto.Status);

                // Atualiza o usuário associado
                user.UpdateFullName(dto.FullName);
                user.UpdateEmail(dto.Email);

                // Atualiza os horários de disponibilidade
                var updatedSlots = dto.AvailabilitySlots.Select(slotDto =>
                    new Slot(slotDto.StartTime, slotDto.EndTime)
                ).ToList();
                existingStaff.UpdateAvailabilitySlots(new AvailabilitySlots(updatedSlots));

                // Confirma as alterações
                await _unitOfWork.CommitAsync();

                // Mapeia para DTO
                StaffDTO updatedStaffDto = new StaffDTO(
                    existingStaff.Specialization.Area,
                    existingStaff.Id.AsString(),
                    existingStaff.Phone.phoneNumber,
                    existingStaff.AvailabilitySlots.Slots,
                    existingStaff.Status,
                    new TodoApi.DTOs.User.UserDTO(user.Id.ToString(), user.Name, user.Email, user.Role.ToString())
                );

                return updatedStaffDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating the staff member");
                throw;
            }
        }
        public async Task<StaffDTO> InactivateStaff(Guid id, UpdateStaffDTO dto)        {
            try
            {
                Staff existingStaff = await _staffRepository.GetByIdAsync(new LicenseNumber(id.ToString()));
                TodoApi.Models.User.User user = await _userRepository.GetByIdAsync(new UserID(existingStaff.user.Id));


                if (existingStaff == null)
                {
                    throw new Exception("Staff member not found");
                }

                existingStaff.Inactivate();

                await _unitOfWork.CommitAsync();

                StaffDTO updatedStaffDto = new StaffDTO(
                    existingStaff.Specialization.Area,
                    existingStaff.Id.AsString(),
                    existingStaff.Phone.phoneNumber,
                    existingStaff.AvailabilitySlots.Slots,
                    existingStaff.Status,
                    new TodoApi.DTOs.User.UserDTO(user.Id.ToString(), user.Name, user.Email, user.Role.ToString())
                );

                return updatedStaffDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating the staff member status");
                throw;
            }
        }
        public async Task<List<StaffDTO>> SearchStaff(string? fullName, string? specialization, string? email, string? status, string? phone)
        {
            try
            {
                var staffList = await _staffRepository.SearchAsync(fullName, specialization, email, status, phone);
                return staffList.Select(staff => _mapper.ToDto(staff)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching staff");
                throw;
            }
        }
    }
}

