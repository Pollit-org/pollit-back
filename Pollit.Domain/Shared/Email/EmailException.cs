using System;
using Pollit.SeedWork;

namespace Pollit.Domain.Shared.Email
{
    public class EmailException : PollitDomainException
    {
        private const string PublicMessage = "Email not valid.";

        public EmailException(string message = PublicMessage) : base(message) { }

        public EmailException(Exception innerException, string message = PublicMessage) : base(message, innerException) { }
    }
}