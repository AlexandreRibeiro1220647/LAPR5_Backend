
using TodoApi.DTOs.Auth;
using TodoApi.DTOs.User;
using TodoApi.Models;
using TodoApi.Models.Auth;

namespace TodoApi.Mappers;

public class UserSessionMapper : IUserSessionMapper
{
    public UserSessionDTO ToDto(UserSession entity)
    {
        return new UserSessionDTO
        (
            entity.Id.AsString(), 
            entity.SessionId,
            entity.AccessToken,
            entity.IsAuthenticated
        );   
    }

    public UserSession ToEntity(UserSessionDTO dto)
    {
        return new UserSession(
            dto.SessionId, 
            dto.AccessToken,
            dto.IsAuthenticated);
    }

    public UserSession toEntity(RegisterUserDTO dto)
    {
        throw new NotImplementedException();
    }
}
