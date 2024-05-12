using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Exceptions;

public class UniqueViolationException : DatabaseException
{
    public UniqueViolationException(string message = "Нарушен уникальный индекс.", Exception? innerException = default) :
        base(message, innerException)
    { }
}
