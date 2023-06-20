using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Domain._Ports;

public interface IEmailVerificationEmailSender
{
    Task SendEmailVerificationEmail(Email email, UserName userName, Uri verifyEmailLinkUrl);
    Task SendResetPasswordLinkEmail(Email email, UserName userName, Uri resetPasswordUrl);
}