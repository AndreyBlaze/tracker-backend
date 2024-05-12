namespace Application.Common.Identity;

public record UserTokenInfo(Guid SessionId, Guid Id, string UserName, string Role);
