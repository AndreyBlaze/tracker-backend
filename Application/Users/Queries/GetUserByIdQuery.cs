using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IQuery<User>;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, User>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserByIdQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(query.Id, cancellationToken);

        if (user == null)
            return Result.Failure<User>(UserResult.NotFound(query.Id));

        return Result.Success(user);
    }
}
