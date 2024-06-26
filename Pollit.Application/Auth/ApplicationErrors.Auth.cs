﻿namespace Pollit.Application;

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
    public const string ExpiredAccessTokenIsInvalid = $"{AuthErrorPrefix}:EXPIRED_ACCESS_TOKEN_IS_INVALID";
    public const string RefreshTokenIsInvalid = $"{AuthErrorPrefix}:REFRESH_TOKEN_IS_INVALID";
    public const string ResetPasswordLinkNotFoundOrExpired = $"{UsersErrorPrefix}:RESET_PASSWORD_LINK_NOT_FOUND_OR_EXPIRED";
}