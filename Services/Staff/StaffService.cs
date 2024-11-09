using TodoApi.Models.Shared;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;
using TodoApi.Models;
using TodoApi.DTOs;
using TodoApi.Mappers;
using TodoApi.Infrastructure.Staff;
using Microsoft.Extensions.Logging;

namespace TodoApi.Services
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IStaffService> _logger;
        private StaffMapper _mapper = new StaffMapper();
        private readonly IUserService _userService;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, IUserRepository userRepository, ILogger<IStaffService> logger, IUserService userService)  
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
                // Obtem o User a partir do UserId
                var user = await _userRepository.GetByIdAsync(new UserID(RegisterStaffDTO.UserId));
                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                // Cria um usuário associado ao staff
                await _userService.CreateUser(new DTOs.User.RegisterUserDTO(dto.FullName, user.Email, dto.Role));                
               
                // Verifica se o email já existe
                var existingEmails = await _staffRepository.SearchByEmail(user.Email);
                if (existingEmails.Any())
                {
                    throw new Exception("O email já está em uso.");
                }

                // Verifica se o telefone já existe
                var existingPhone = await _staffRepository.GetByPhoneAsync(dto.Phone);
                if (existingPhone != null)
                {
                    throw new Exception("O número de telefone já está em uso.");
                }

                Staff staff = _mapper.ToEntity(dto);

                await _staffRepository.AddAsync(staff);
                await _unitOfWork.CommitAsync();

                return _mapper.ToDto(staff);
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
                Staff existingStaff = await _staffRepository.GetByIdAsync(new LicenseNumber(id.ToString()));

                if (existingStaff == null)
                {
                    throw new Exception("Staff member not found");
                }

                // Verifica se o email já existe para outro staff
                var existingEmails = await _staffRepository.SearchByEmail(dto.Email);
                if (existingEmails.Any(e => e.Id != existingStaff.Id))
                {
                    throw new Exception("O email já está em uso.");
                }

                // Verifica se o telefone já existe para outro staff
                var existingPhone = await _staffRepository.GetByPhoneAsync(dto.Phone);
                if (existingPhone != null && existingPhone.Id != existingStaff.Id)
                {
                    throw new Exception("O número de telefone já está em uso.");
                }

                existingStaff.UpdateFullName(dto.FullName);
                existingStaff.UpdatePhone(dto.Phone);
                existingStaff.UpdateEmail(dto.Email);
                existingStaff.UpdateSpecialization(new string(dto.Specialization));
                existingStaff.UpdateStatus(dto.Status);
                var updatedSlots = dto.AvailabilitySlots.Select(slotDto =>
                    new Slot(slotDto.StartTime, slotDto.EndTime)
                ).ToList();
                existingStaff.UpdateAvailabilitySlots(new AvailabilitySlots(updatedSlots));

                await _unitOfWork.CommitAsync();

                StaffDTO updatedStaffDto = new StaffDTO(
                    existingStaff.FullName.fullName,
                    existingStaff.Specialization.Area,
                    existingStaff.Id.AsString(),
                    existingStaff.Email.Value,
                    existingStaff.Phone.phoneNumber,
                    existingStaff.AvailabilitySlots.Slots,
                    existingStaff.Status
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

                if (existingStaff == null)
                {
                    throw new Exception("Staff member not found");
                }

                existingStaff.Inactivate();

                await _unitOfWork.CommitAsync();

                StaffDTO updatedStaffDto = new StaffDTO(
                    existingStaff.FullName.fullName,
                    existingStaff.Specialization.Area,
                    existingStaff.Id.AsString(),
                    existingStaff.Email.Value,
                    existingStaff.Phone.phoneNumber,
                    existingStaff.AvailabilitySlots.Slots,
                    existingStaff.Status
                );

                return updatedStaffDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating the staff member status");
                throw;
            }
        }
        public async Task<List<StaffDTO>> GetStaffBySpecialization(string specialization)
        {
            try
            {
                var staffList = await _staffRepository.SearchBySpecialization(specialization);
                return staffList.Select(staff => _mapper.ToDto(staff)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching staff by specialization");
                throw;
            }
        }
        public async Task<List<StaffDTO>> GetStaffByEmail(string email)
        {
            try
            {
                var staffList = await _staffRepository.SearchByEmail(email);
                return staffList.Select(staff => _mapper.ToDto(staff)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching staff by email");
                throw;
            }
        }

        public async Task<List<StaffDTO>> GetStaffByName(string name)
        {
            try
            {
                var staffList = await _staffRepository.SearchByName(name);
                return staffList.Select(staff => _mapper.ToDto(staff)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching staff by name");
                throw;
            }
        }
       public async Task<List<StaffDTO>> GetStaffByStatus(StaffStatus status)
       {
           try
           {
               var staffStatusList = await _staffRepository.SearchByStatus(status);
               return staffStatusList.Select(staff => _mapper.ToDto(staff)).ToList();
           }
           catch (Exception e)
           {
               _logger.LogError(e, "Error searching staff by status");
               throw;
           }
       }

    }
}

