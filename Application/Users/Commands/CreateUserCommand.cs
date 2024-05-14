using Application.Abstractions.Messaging;
using Domain.Entities;
using DTO;
using DTO.Mapping;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.Users.Commands;

public sealed record CreateUserCommand(UserDTO Model) : ICommand<User>;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, User>
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var model = UserMapper.MapUser(command.Model);

        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
        model.DateAdd = DateTimeOffset.UtcNow;
        model.DateUpdate = DateTimeOffset.UtcNow;

        try
        {
            var res = await _usersRepository.AddAsync(model, cancellationToken);
            return Result.Success(res);
        }
        catch (Exception ex)
        {
            return Result.Failure<User>(new(Code: "Users.ServerError", Description: ex.ToString()));
        }
    }
}
