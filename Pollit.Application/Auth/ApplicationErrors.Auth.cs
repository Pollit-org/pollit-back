namespace Pollit.Application;

public partial class ApplicationError
{
    private const string AuthErrorPrefix = $"{GlobalErrorPrefix}:AUTH";

    public const string PasswordDesNotMeetRequirements = $"{AuthErrorPrefix}:PASSWORD_DOES_NOT_MEET_REQUIREMENTS";
    public const string UserNameAlreadyExists = $"{AuthErrorPrefix}:USER_NAME_ALREADY_EXISTS";
    public const string EmailAlreadyExists = $"{AuthErrorPrefix}:EMAIL_ALREADY_EXISTS";
    public const string InvalidEmail = $"{AuthErrorPrefix}:INVALID_EMAIL";
    public const string InvalidUserName = $"{AuthErrorPrefix}:INVALID_USER_NAME";
    public const string CredentialsSigninFailed = $"{AuthErrorPrefix}:CREDENTIALS_SIGNIN_FAILED";
    public const string GoogleSigninFailed = $"{AuthErrorPrefix}:GOOGLE_SIGNIN_FAILED";
    public const string ForbiddenAccess = $"{AuthErrorPrefix}:FORBIDDEN_ACCESS";
}