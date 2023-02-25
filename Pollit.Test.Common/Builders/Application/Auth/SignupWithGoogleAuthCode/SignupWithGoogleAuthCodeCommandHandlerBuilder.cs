using Pollit.Application.Auth.SigninWithGoogleAuthCode;
using Pollit.Domain._Ports;
using Pollit.Test.Common.Builders.Domain.Users;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Application.Auth.SignupWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeCommandHandlerBuilder : IFluentBuilder<SigninWithGoogleAuthCodeCommandHandler>
{
    private readonly InMemoryDatabase _inMemoryDatabase = new();
    private readonly GoogleAuthenticationServiceBuilder _googleAuthenticationServiceBuilder;

    public SigninWithGoogleAuthCodeCommandHandlerBuilder(IGoogleAuthenticator googleAuthenticator, InMemoryDatabase? inMemoryDatabase = null)
    {
        _inMemoryDatabase = inMemoryDatabase ?? _inMemoryDatabase;
        _googleAuthenticationServiceBuilder = new GoogleAuthenticationServiceBuilder(_inMemoryDatabase, googleAuthenticator);
    }

    public SigninWithGoogleAuthCodeCommandHandler Build()
    {
        return new SigninWithGoogleAuthCodeCommandHandler(_inMemoryDatabase.GetUnitOfWork(), _googleAuthenticationServiceBuilder.Build());
    }
}