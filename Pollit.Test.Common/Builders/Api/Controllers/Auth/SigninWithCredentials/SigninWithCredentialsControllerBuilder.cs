using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

namespace Pollit.Test.Common.Builders.Api.Controllers.Auth.SigninWithCredentials;

public class SigninWithCredentialsControllerBuilder : ControllerBuilderBase<SigninWithCredentialsController>
{
    private SigninWithCredentialsCommandHandler _commandHandler;

    public SigninWithCredentialsControllerBuilder WithCommandHandler(SigninWithCredentialsCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public override SigninWithCredentialsController Build()
    {
        return new SigninWithCredentialsController(_commandHandler, _authenticatedUserProvider);
    }
}