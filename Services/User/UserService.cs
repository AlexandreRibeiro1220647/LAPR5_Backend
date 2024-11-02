using TodoApi.DTOs.User;
using TodoApi.Infrastructure;
using TodoApi.Models.Shared;
using TodoApi.Mappers;
using TodoApi.Services.Login;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using TodoApi.Models;

namespace TodoApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IUserService> _logger;
        private readonly UserMapper mapper = new();

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ILogger<IUserService> logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserDTO> CreateUser(RegisterUserDTO model)
        {
            try
            {
                Models.User.User mapped = mapper.toEntity(model);

                await _userRepository.AddAsync(mapped);

                UserDTO mappedDto = mapper.ToDto(mapped);

                await _unitOfWork.CommitAsync();

                return mappedDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Models.User.User>> GetAllUsersAsync()
        {
            try
            { 
                return await _userRepository.GetAllUsersAsync();
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }

        public async Task<Models.User.User> GetUserByEmail(string email)
        {
            try
            { 
                return await _userRepository.GetByEmailAsync(email);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }

    }
}
