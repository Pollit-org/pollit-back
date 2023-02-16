using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Domain.Users;
using Pollit.Test.Common.Builders.Application._Ports;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommandHandlerBuilder : IFluentBuilder<SignupWithCredentialsCommandHandler>
{
    private readonly InMemoryDatabase _inMemoryDatabase = new();
    private readonly AccessTokenManagerBuilder _accessTokenManagerBuilder = new();
    private readonly PasswordEncryptorBuilder _passwordEncryptorBuilder = new();

    public SignupWithCredentialsCommandHandlerBuilder WithExistingUser(User existingUser)
    {
        _inMemoryDatabase.GetRepository<IUserRepository>().AddAsync(existingUser);
        return this;
    }
    
    public SignupWithCredentialsCommandHandler Build()
    {
        return new SignupWithCredentialsCommandHandler(_inMemoryDatabase.GetRepository<IUserRepository>(), _accessTokenManagerBuilder.Build(), _passwordEncryptorBuilder.Build(), _inMemoryDatabase.GetUnitOfWork());
    }
}