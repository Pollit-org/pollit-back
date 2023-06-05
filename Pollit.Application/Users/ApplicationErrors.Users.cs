namespace Pollit.Application;

public partial class ApplicationError
{
    private const string UsersErrorPrefix = $"{GlobalErrorPrefix}:USERS";

    public const string UserNotFound = $"{UsersErrorPrefix}:USER_NOT_FOUND";
    public const string UserNameIsAlreadyPermanent = $"{UsersErrorPrefix}:USER_NAME_IS_ALREADY_PERMANENT";
    public const string BirthdateIsInTheFuture = $"{UsersErrorPrefix}:BIRTHDATE_IS_IN_THE_FUTURE";
    public const string BirthdateMalformed = $"{UsersErrorPrefix}:BIRTHDATE_IS_MALFORMED";
    public const string EmailVerificationTokenMismatch = $"{UsersErrorPrefix}:EMAIL_VERIFICATION_TOKEN_MISMATCH";
}