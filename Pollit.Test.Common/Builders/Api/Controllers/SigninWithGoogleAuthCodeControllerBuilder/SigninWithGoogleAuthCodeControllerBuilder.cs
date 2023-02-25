using Pollit.Application.Auth.SigninWithGoogleAuthCode;
using Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;

namespace Pollit.Test.Common.Builders.Api.Controllers.SigninWithGoogleAuthCodeControllerBuilder;

public class SigninWithGoogleAuthCodeControllerBuilder : IFluentBuilder<SigninWithGoogleAuthCodeController>
{
    private SigninWithGoogleAuthCodeCommandHandler _commandHandler;

    public SigninWithGoogleAuthCodeControllerBuilder WithCommandHandler(SigninWithGoogleAuthCodeCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public SigninWithGoogleAuthCodeController Build()
    {
        return new SigninWithGoogleAuthCodeController(_commandHandler);
    }
}