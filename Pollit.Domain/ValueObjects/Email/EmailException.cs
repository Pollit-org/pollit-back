namespace Pollit.Domain.ValueObjects.Email
{
    public class EmailException : Exception
    {
        private const string PublicMessage = "Email not valid.";

        public EmailException(string message = PublicMessage) : base(message) { }

        public EmailException(Exception innerException, string message = PublicMessage) : base(message, innerException) { }
    }
}