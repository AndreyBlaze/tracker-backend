using Application.Abstractions.Messaging;
using Shared;

namespace Application.Users.Commands;

public sealed record CreateUserCommand() : ICommand;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    public Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
