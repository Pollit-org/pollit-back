using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;
using Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;

namespace Pollit.Test.Common.Builders.Api.Controllers.SignupWithCredentialsControllerBuilder;

public class SignupWithCredentialsControllerBuilder : IFluentBuilder<SignupWithCredentialsController>
{
    private SignupWithCredentialsCommandHandler _commandHandler;

    public SignupWithCredentialsControllerBuilder WithCommandHandler(SignupWithCredentialsCommandHandler commandHandler)
    {
        this._commandHandler = commandHandler;
        return this;
    }
    
    public SignupWithCredentialsController Build()
    {
        return new SignupWithCredentialsController(_commandHandler);
    }
}