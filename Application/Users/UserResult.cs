using Shared;

namespace Application.Users;

public static class UserResult
{
    public static Error NotFound(Guid id) => new Error(Code: "Users.NotFound", Description: $"User with ID = '{id}' is not found");
    public static Error NotFound(string userName) => new Error(Code: "Users.NotFound", Description: $"User with username = '{userName}' is not found");
}
