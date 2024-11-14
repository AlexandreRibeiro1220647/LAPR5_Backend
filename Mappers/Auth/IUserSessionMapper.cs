using TodoApi.DTOs.Auth;
using TodoApi.DTOs.User;
using TodoApi.Models.Auth;

namespace TodoApi.Mappers;

public interface IUserSessionMapper : IMapper<UserSession, UserSessionDTO, RegisterUserDTO>
{
}