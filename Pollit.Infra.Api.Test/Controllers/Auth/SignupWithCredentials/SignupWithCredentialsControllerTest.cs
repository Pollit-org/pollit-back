using Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;
using Pollit.Test.Common.Builders.Api.Controllers.SignupWithCredentialsControllerBuilder;
using Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;
using Pollit.Test.Common.Builders.Domain.Users;
using Xunit;

namespace Pollit.Infra.Api.Test.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsControllerTest
{
    [Fact]
    public async Task Given_AUserExistsWithAGivenEmail_When_ITryToSignupWithThatSameEmail_Then_ICannot()
    {
        var existingUser = new UserBuilder()
            .WithEmail("mrflow@pollit.me")
            .Build();
        
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder()
            .WithExistingUser(existingUser)
            .Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
        var response = await controller.SignupAsync(new SignupWithCredentialsHttpRequestBody()
        {
            Email = "mrflow@pollit.me",
            UserName = "McGyver",
            Password = "NotYourBusiness12345++"
        });
    }
}