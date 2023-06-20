using Pollit.Application._Ports;
using Pollit.Domain.Users;
using Pollit.Infra.FrontApp.UrlBuilder.Config;
using Flurl;
using Pollit.Domain.Users.ResetPasswordLinks;

namespace Pollit.Infra.FrontApp.UrlBuilder;

public class FrontAppUrlBuilder : IFrontAppUrlBuilder
{
    private readonly FrontAppConfig _frontAppConfig;

    public FrontAppUrlBuilder(FrontAppConfig frontAppConfig)
    {
        _frontAppConfig = frontAppConfig;
    }

    public Uri BuildVerifyEmailUrl(UserId userId, EmailVerificationToken emailVerificationToken)
    {
        return _frontAppConfig
            .BaseUrl
            .TrimEnd('/')
            .AppendPathSegment(_frontAppConfig.Routes.VerifyEmail.Path)
            .SetQueryParam(_frontAppConfig.Routes.VerifyEmail.UserIdParamName, userId)
            .SetQueryParam(_frontAppConfig.Routes.VerifyEmail.VerificationTokenParamName, emailVerificationToken)
            .ToUri();
    }

    public Uri BuildResetPasswordUrl(UserId userId, PasswordResetToken passwordResetToken)
    {
        return _frontAppConfig
            .BaseUrl
            .TrimEnd('/')
            .AppendPathSegment(_frontAppConfig.Routes.ResetPassword.Path)
            .SetQueryParam(_frontAppConfig.Routes.ResetPassword.UserIdParamName, userId)
            .SetQueryParam(_frontAppConfig.Routes.ResetPassword.ResetPasswordTokenParamName, passwordResetToken)
            .ToUri();
    }
}