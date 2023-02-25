using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.UserNames;
using Pollit.Test.Common.Builders.Domain.Users;
using Pollit.Test.InMemoryDb;

namespace Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;

public class SignupWithCredentialsCommandHandlerBuilder : IFluentBuilder<SignupWithCredentialsCommandHandler>
{
    private readonly InMemoryDatabase _inMemoryDatabase = new();
    private readonly UserAuthenticationServiceBuilder _userAuthenticationServiceBuilder;

    public SignupWithCredentialsCommandHandlerBuilder(InMemoryDatabase? inMemoryDatabase = null)
    {
        _inMemoryDatabase = inMemoryDatabase ?? _inMemoryDatabase;
        _userAuthenticationServiceBuilder = new UserAuthenticationServiceBuilder(_inMemoryDatabase);
    }

    public SignupWithCredentialsCommandHandlerBuilder WithExistingUserHavingUserName(UserName userName)
    {
        var existingUser = new UserBuilder()
            .WithUserName(userName)
            .Build();
        
        return WithExistingUser(existingUser);
    }

    public SignupWithCredentialsCommandHandlerBuilder WithExistingUserHavingEmail(Email email)
    {
        var existingUser = new UserBuilder()
            .WithEmail(email)
            .Build();
        
        return WithExistingUser(existingUser);
    }
    
    public SignupWithCredentialsCommandHandlerBuilder WithExistingUser(User existingUser)
    {
        var userRepository = _inMemoryDatabase.GetUserRepository();
        userRepository.Add(existingUser);
        _inMemoryDatabase.GetUnitOfWork().SaveChangesAsync();
        return this;
    }
    
    public SignupWithCredentialsCommandHandler Build()
    {
        return new SignupWithCredentialsCommandHandler(_userAuthenticationServiceBuilder.Build(), _inMemoryDatabase.GetUnitOfWork());
    }
}