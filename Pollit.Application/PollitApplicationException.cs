using Pollit.Domain.Polls.PollOptionTitles.Exceptions;
using Pollit.Domain.Polls.PollTitles.Exceptions;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.Birthdates.Exceptions;
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
            BirthdateMalformedException => ApplicationError.BirthdateMalformed,
            PollTitleTooShortException => ApplicationError.PollTitleTooShort,
            PollTitleTooLongException => ApplicationError.PollTitleTooLong,
            PollOptionTitleTooLongException => ApplicationError.PollOptionTitleTooLong,
            _ => ApplicationError.UnknownError
        };

        return new PollitApplicationException(errorCode, domainException);
    }
}