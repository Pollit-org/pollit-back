using Pollit.Domain.Users;

namespace Pollit.Application._Ports;

public interface IFrontAppUrlBuilder
{
    Uri BuildVerifyEmailUrl(UserId userId, EmailVerificationToken emailVerificationToken);
}