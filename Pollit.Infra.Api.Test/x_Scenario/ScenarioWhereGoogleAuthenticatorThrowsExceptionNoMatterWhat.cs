using Pollit.Infra.GoogleApi;
using Pollit.Test.Common.Builders.Domain._Ports;

namespace Pollit.Infra.Api.Test.x_Scenario;

public class ScenarioWhereGoogleAuthenticatorThrowsExceptionNoMatterWhat
{
    private readonly GoogleAuthenticatorBuilder _googleAuthenticatorBuilder;

    public ScenarioWhereGoogleAuthenticatorThrowsExceptionNoMatterWhat(GoogleAuthenticatorBuilder googleAuthenticatorBuilder)
    {
        _googleAuthenticatorBuilder = googleAuthenticatorBuilder;
    }

    public void Setup()
    {
        
    }
}