using Pollit.Application._Ports;
using Pollit.Domain.Users;
using Pollit.Infra.FrontApp.UrlBuilder.Config;
using Flurl;

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
}