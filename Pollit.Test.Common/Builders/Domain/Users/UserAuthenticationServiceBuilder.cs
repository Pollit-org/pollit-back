using Pollit.Domain.Users.Services;
using Pollit.Test.Common.Builders.Domain._Ports;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Domain.Users;

public class UserAuthenticationServiceBuilder : IFluentBuilder<CredentialsAuthenticationService>
{
    private readonly AccessTokenManagerBuilder _accessTokenManagerBuilder = new();
    private readonly PasswordEncryptorBuilder _passwordEncryptorBuilder = new();
    private readonly InMemoryDatabase _inMemoryDatabase;

    public UserAuthenticationServiceBuilder(InMemoryDatabase inMemoryDatabase)
    {
        _inMemoryDatabase = inMemoryDatabase;
    }

    public CredentialsAuthenticationService Build()
    {
        return new CredentialsAuthenticationService(_inMemoryDatabase.GetUserRepository(), _passwordEncryptorBuilder.Build(), _accessTokenManagerBuilder.Build());
    }
}