using Pollit.Domain.Users;
using Pollit.Infra.GoogleApi;
using Pollit.Test.Common.Builders.Domain._Ports;

namespace Pollit.Infra.Api.Test.x_Scenario;

public class ScenarioWhereGoogleAuthenticatorReturnsAValidProfileFromMyAuthCode
{
    private readonly GoogleAuthenticatorBuilder _googleAuthenticatorBuilder;
    private string _authCode = "wenfqbffqoenfquifgq";
    private GoogleProfile _googleProfile = new() {Email = "fwnjfnewj@nj.cece"};

    public ScenarioWhereGoogleAuthenticatorReturnsAValidProfileFromMyAuthCode(GoogleAuthenticatorBuilder googleAuthenticatorBuilder)
    {
        _googleAuthenticatorBuilder = googleAuthenticatorBuilder;
    }

    public ScenarioWhereGoogleAuthenticatorReturnsAValidProfileFromMyAuthCode WithCode(string authCode)
    {
        _authCode = authCode;
        return this;
    }
    
    public ScenarioWhereGoogleAuthenticatorReturnsAValidProfileFromMyAuthCode ReturningProfileWithEmail(string email)
    {
        _googleProfile.Email = email;
        return this;
    }

    public void Setup()
    {
        _googleAuthenticatorBuilder.WithAuthCodeReturningProfile(_authCode, _googleProfile);
    }
}