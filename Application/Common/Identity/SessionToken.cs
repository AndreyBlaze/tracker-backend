namespace Application.Common.Identity;

public record SessionToken(Guid SessionId, string Key)
{
    private const string Separator = "::";

    public override string ToString()
    {
        return $"{SessionId}{Separator}{Key}";
    }

    public static SessionToken? Parse(string str)
    {
        var parts = str.Split(Separator);
        if (parts.Length == 2 && Guid.TryParse(parts[0], out var sessionId))
        {
            return new SessionToken(sessionId, parts[1]);
        }
        return null;
    }
}
