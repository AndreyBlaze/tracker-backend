namespace Infrastructure.Persistence.Abstractions;

public abstract class DatabaseException : ApplicationException
{
    protected DatabaseException(string? message = default, Exception? innerException = default) : base(message, innerException) { }
}
