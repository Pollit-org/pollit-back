using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users;
using Pollit.Test.Common.Builders.Domain.Users;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Application.Auth.SigninWithCredentials;

public class SigninWithCredentialsCommandHandlerBuilder : IFluentBuilder<SigninWithCredentialsCommandHandler>
{
    private readonly InMemoryDatabase _inMemoryDatabase = new();
    private readonly UserAuthenticationServiceBuilder _userAuthenticationServiceBuilder;

    public SigninWithCredentialsCommandHandlerBuilder(InMemoryDatabase? inMemoryDatabase = null)
    {
        _inMemoryDatabase = inMemoryDatabase ?? _inMemoryDatabase;
        _userAuthenticationServiceBuilder = new UserAuthenticationServiceBuilder(_inMemoryDatabase);
    }

    public SigninWithCredentialsCommandHandlerBuilder WithExistingUser(User existingUser)
    {
        var userRepository = _inMemoryDatabase.GetUserRepository();
        userRepository.Add(existingUser);
        _inMemoryDatabase.GetUnitOfWork().SaveChangesAsync();
        return this;
    }
    
    public SigninWithCredentialsCommandHandler Build()
    {
        return new SigninWithCredentialsCommandHandler(_inMemoryDatabase.GetUnitOfWork(), _userAuthenticationServiceBuilder.Build());
    }
}