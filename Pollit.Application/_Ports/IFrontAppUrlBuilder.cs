using Pollit.Domain.Users;
using Pollit.Domain.Users.ResetPasswordLinks;

namespace Pollit.Application._Ports;

public interface IFrontAppUrlBuilder
{
    Uri BuildVerifyEmailUrl(UserId userId, EmailVerificationToken emailVerificationToken);
    Uri BuildResetPasswordUrl(UserId userId, PasswordResetToken passwordResetToken);
}