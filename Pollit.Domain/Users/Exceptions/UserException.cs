using System;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.Exceptions;

public class UserException : DomainException
{
    public UserException()
    {
    }

    public UserException(string? message) : base(message)
    {
    }

    public UserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}