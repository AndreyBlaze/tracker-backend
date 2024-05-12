namespace Application.Common.Identity;

public record UserTokenData(string AccessToken, string RefreshToken, string Role);
