using Domain.Entities;

namespace DTO.Mapping;

public class UserMapper
{
    public static User MapUser(UserDTO model)
    {
        return new()
        {
            Id = model.Id ?? Guid.NewGuid(),
            UserName = model.UserName,
            Password = model.Password,
            Role = model.Role,
            Email = model.Email,
        };
    }
}
