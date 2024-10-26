
using TodoApi.DTOs.User;
using TodoApi.Models;
using TodoApi.Models.User;

namespace TodoApi.Mappers;

public class UserMapper : IUserMapper
{
    public UserDTO ToDto(User entity)
    {
        return new UserDTO
        (
            entity.Id.AsString(), 
            entity.Name,
            entity.Email.ToString(),
            entity.Role.ToString()
        );   
    }

    public User ToEntity(UserDTO dto)
    {
        throw new NotImplementedException();
    }

    public User toEntity(RegisterUserDTO dto)
    {
        return new User(
            new UserEmail(dto.Email), 
            dto.Name,
            dto.Role, "Passwo1_");
    }
}
