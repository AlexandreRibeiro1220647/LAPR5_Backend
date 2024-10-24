using TodoApi.DTOs.User;
using TodoApi.Models.User;

namespace TodoApi.Mappers;

public interface IUserMapper : IMapper<User, UserDTO, RegisterUserDTO>
{
}