using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.ClearPasswords.Exceptions;
using Pollit.Domain.Users.UserNames.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Application;

public class PollitApplicationException : Exception
{
    public string ErrorCode => Message;
    
    public PollitApplicationException(string errorCode, Exception? innerException = null) : base(errorCode, innerException)
    {
        
    }

    public static PollitApplicationException FromDomainException(PollitDomainException domainException)
    {
        var errorCode = domainException switch
        {
            EmailException => ApplicationError.InvalidEmail,
            PasswordException => ApplicationError.PasswordDesNotMeetRequirements,
            UserNameException => ApplicationError.InvalidUserName,
            _ => ApplicationError.UnknownError
        };

        return new PollitApplicationException(errorCode, domainException);
    }
}