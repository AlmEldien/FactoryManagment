using FactoryManagment.Domain.Enums;

namespace FactoryManagment.Domain.Exceptions;

public class InvalidProductStateException : Exception
{
    public Status CurrentStatus { get; }

    public InvalidProductStateException(Status status, string message) : base(message)
    {
        CurrentStatus = status;
    }
}
