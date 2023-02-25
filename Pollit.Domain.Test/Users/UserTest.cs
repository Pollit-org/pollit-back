using FluentAssertions;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.EncryptedPasswords;
using Xunit;

namespace Pollit.Domain.Test.Users;

public class UserTest
{
    [Fact]
    public void Given_AUserWasJustCreatedWithEmailUserNameAndPassword_Then_TheirGenderShouldBeNull()
    {
        var user = User.NewUser("florian@pollit.me", "MrFlow", new EncryptedPassword(new B64Salt("123"), "fefnwfwefbfwef"));

        user.Gender.Should().BeNull();
    }
    
    [Fact]
    public void Given_AUserWasJustCreatedWithJustAnEmail_Then_TheirGenderShouldBeNull()
    {
        var user = User.NewUser("florian@pollit.me");

        user.Gender.Should().BeNull();
    }
}