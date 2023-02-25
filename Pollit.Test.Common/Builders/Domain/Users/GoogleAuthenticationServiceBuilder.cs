using Pollit.Domain._Ports;
using Pollit.Domain.Users.Services;
using Pollit.Test.Common.Builders.Domain._Ports;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Domain.Users;

public class GoogleAuthenticationServiceBuilder : IFluentBuilder<GoogleAuthenticationService>
{
    private readonly AccessTokenManagerBuilder _accessTokenManagerBuilder = new();
    private readonly IGoogleAuthenticator _googleAuthenticator;
    private readonly InMemoryDatabase _inMemoryDatabase;

    public GoogleAuthenticationServiceBuilder(InMemoryDatabase inMemoryDatabase, IGoogleAuthenticator googleAuthenticator)
    {
        _inMemoryDatabase = inMemoryDatabase;
        _googleAuthenticator = googleAuthenticator;
    }

    public GoogleAuthenticationService Build()
    {
        return new GoogleAuthenticationService(_inMemoryDatabase.GetUserRepository(), _accessTokenManagerBuilder.Build(), _googleAuthenticator);
    }
}