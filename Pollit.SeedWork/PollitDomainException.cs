using System;

namespace Pollit.SeedWork;

public class PollitDomainException : Exception
{
    public PollitDomainException()
    {
    }

    public PollitDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}