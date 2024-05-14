using Application.Abstractions.Messaging;
using Application.Common.Identity;
using Application.Services.Interfaces;
using Configuration.Identity;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Shared;

namespace Application.Users.Commands;

public record AuthCommand(string Login, string Password) : ICommand<UserTokenData>;

public class AuthCommandHandler : ICommandHandler<AuthCommand, UserTokenData>
{
    private readonly IJwtService _jwtService;
    private readonly ISessionRepository _sessionRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly AuthOptions _authOptions;

    public AuthCommandHandler(IJwtService jwtService, ISessionRepository sessionRepository, IUsersRepository usersRepository, IOptions<AuthOptions> authOptions)
    {
        _jwtService = jwtService;
        _sessionRepository = sessionRepository;
        _usersRepository = usersRepository;
        _authOptions = authOptions.Value;
    }

    public async Task<Result<UserTokenData>> Handle(AuthCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUserNameAsync(command.Login, cancellationToken);

        if (user is null)
            return Result.Failure<UserTokenData>(UserResult.NotFound(command.Login));

        var refreshTokenKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var session = await _sessionRepository.AddAsync(new Session()
        {
            Account = user,
            RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshTokenKey, 6),
            ExpirationDateTime = DateTimeOffset.UtcNow.AddMinutes(_authOptions.RefreshTokenTtl)
        });

        user.DateUpdate = DateTimeOffset.UtcNow;
        await _usersRepository.UpdateAsync(user, CancellationToken.None);

        var accessToken = _jwtService.CreateToken(new UserTokenInfo(session.Id, user.Id, user.UserName, user.Role.ToString()));

        var userToken = new UserTokenData(accessToken,
            new SessionToken(session.Id, refreshTokenKey).ToString(),
            session.Account.Role.ToString());

        return Result.Success(userToken);
    }
}
