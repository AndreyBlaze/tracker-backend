using Application.Common.Identity;
using Application.Services.Interfaces;
using Configuration.Identity;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Impl;

public class JwtService : IJwtService
{
    private readonly AuthOptions _authOptions;

    public JwtService(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public string CreateToken(UserTokenInfo model)
    {
        var now = DateTimeOffset.Now;

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.SessionId, model.SessionId.ToString()),
                new Claim("user_id", model.Id.ToString()),
                new Claim(JwtClaimTypes.Subject, model.UserName),
                new Claim(JwtClaimTypes.Role, model.Role)
            }),
            Issuer = _authOptions.Issuer,
            Audience = _authOptions.Audience,
            IssuedAt = now.DateTime,
            Expires = now.AddMinutes(_authOptions.AccessTokenTtl).DateTime,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret)),
                SecurityAlgorithms.HmacSha256Signature)
        };


        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
