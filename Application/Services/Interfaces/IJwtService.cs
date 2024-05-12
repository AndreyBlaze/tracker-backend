using Application.Common.Identity;

namespace Application.Services.Interfaces;

public interface IJwtService
{
    string CreateToken(UserTokenInfo info);
}
