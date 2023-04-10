using Pollit.Application.Auth.SigninWithGoogleAuthCode;
using Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;

namespace Pollit.Test.Common.Builders.Api.Controllers.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeControllerBuilder : ControllerBuilderBase<SigninWithGoogleAuthCodeController>
{
    private SigninWithGoogleAuthCodeCommandHandler _commandHandler;

    public SigninWithGoogleAuthCodeControllerBuilder WithCommandHandler(SigninWithGoogleAuthCodeCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public override SigninWithGoogleAuthCodeController Build()
    {
        return new SigninWithGoogleAuthCodeController(_commandHandler, _authenticatedUserProvider);
    }
}