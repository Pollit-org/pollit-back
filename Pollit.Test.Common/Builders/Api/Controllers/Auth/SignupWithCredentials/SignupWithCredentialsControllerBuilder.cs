using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

namespace Pollit.Test.Common.Builders.Api.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsControllerBuilder : ControllerBuilderBase<SignupWithCredentialsController>
{
    private SignupWithCredentialsCommandHandler _commandHandler;

    public SignupWithCredentialsControllerBuilder WithCommandHandler(SignupWithCredentialsCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public override SignupWithCredentialsController Build()
    {
        return new SignupWithCredentialsController(_commandHandler, _authenticatedUserProvider);
    }
}