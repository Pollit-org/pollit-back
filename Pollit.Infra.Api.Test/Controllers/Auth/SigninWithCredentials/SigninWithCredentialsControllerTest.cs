using System.Net;
using FluentAssertions.AspNetCore.Mvc;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;
using Pollit.Infra.Api.Test.x_FluentAssertionExtensions;
using Pollit.Infra.Api.Test.x_Scenario;
using Pollit.Test.Common.Builders.Api.Controllers.Auth.SigninWithCredentials;
using Pollit.Test.Common.Builders.Application.Auth.SigninWithCredentials;
using Pollit.Test.Common.Builders.Domain.Users;
using Pollit.Test.InMemoryDb;
using Xunit;

namespace Pollit.Infra.Api.Test.Controllers.Auth.SigninWithCredentials;

public class SigninWithCredentialsControllerTest
{
    [Theory]
    [InlineData("john.doe@gmail.com")]
    [InlineData("john-doe")]
    public async Task When_ITryToSigninWithAnEmailOrUserNameThatDoesNotExist_Then_IShouldReceiveCredentialsSigninFailedError(string emailOrUsernameThatDoesNotExist)
    {
        var commandHandler = new SigninWithCredentialsCommandHandlerBuilder().Build();
        
        var controller = new SigninWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SigninWithCredentialsHttpRequestBodyBuilder().WithEmailOrUserName(emailOrUsernameThatDoesNotExist).Build();
        
        var response = await controller.SigninAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.Unauthorized).WithError("POLLIT:AUTH:CREDENTIALS_SIGNIN_FAILED");
    }
    
    [Fact]
    public async Task Given_IHaveAnAccountButNoPassword_When_ITryToSigninWithCredentials_Then_IShouldReceiveCredentialsSigninFailedError()
    {
        var user = new UserBuilder().WithEmail("mrflow@pollit.me").WithoutPassword().Build();
        var commandHandler = new SigninWithCredentialsCommandHandlerBuilder().WithExistingUser(user).Build();
        
        var controller = new SigninWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SigninWithCredentialsHttpRequestBodyBuilder().WithEmailOrUserName("mrflow@pollit.me").Build();
        
        var response = await controller.SigninAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.Unauthorized).WithError("POLLIT:AUTH:CREDENTIALS_SIGNIN_FAILED");
    }
    
    [Fact]
    public async Task Given_IHaveAnAccountWithAPassword_When_ITryToSigninWithTheWrongPassword_Then_IShouldReceiveCredentialsSigninFailedError()
    {
        var user = new UserBuilder().WithEmail("mrflow@pollit.me").WithoutPassword().Build();
        var commandHandler = new SigninWithCredentialsCommandHandlerBuilder().WithExistingUser(user).Build();
        
        var controller = new SigninWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SigninWithCredentialsHttpRequestBodyBuilder().WithEmailOrUserName("mrflow@pollit.me").Build();
        
        var response = await controller.SigninAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.Unauthorized).WithError("POLLIT:AUTH:CREDENTIALS_SIGNIN_FAILED");
    }
    
    [Fact]
    public async Task Given_IHaveSignedUpWithAPassword_When_ITryToSigninWithTheWrongPassword_Then_IShouldReceiveCredentialsSigninFailedError()
    {
        var database = new InMemoryDatabase();
        await new ScenarioWhereIJustSignedUpWithCredentials(database).WithEmail("mrflow@pollit.me").WithPassword("MrFlowC'estLeB3st").SetupAsync();
        
        var commandHandler = new SigninWithCredentialsCommandHandlerBuilder(database).Build();
        
        var controller = new SigninWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SigninWithCredentialsHttpRequestBodyBuilder().WithEmailOrUserName("mrflow@pollit.me").WithPassword("CeciN'estPasMonP@ssword").Build();
        
        var response = await controller.SigninAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.Unauthorized).WithError("POLLIT:AUTH:CREDENTIALS_SIGNIN_FAILED");
    }
    
    [Fact]
    public async Task Given_IHaveSignedUpWithAPassword_When_ITryToSigninWithTheRightCredentials_Then_IShouldReceiveOkResultWithAccessTokenAndRefreshToken()
    {
        var database = new InMemoryDatabase();
        await new ScenarioWhereIJustSignedUpWithCredentials(database).WithEmail("mrflow@pollit.me").WithPassword("MrFlowC'estLeB3st").SetupAsync();
        
        var commandHandler = new SigninWithCredentialsCommandHandlerBuilder(database).Build();
        
        var controller = new SigninWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SigninWithCredentialsHttpRequestBodyBuilder().WithEmailOrUserName("mrflow@pollit.me").WithPassword("MrFlowC'estLeB3st").Build();
        
        var response = await controller.SigninAsync(requestBody);

        response.Should().BeOkObjectResult().WithStatusCode((int) HttpStatusCode.OK).WithValueMatch<SigninResultDto>(_ => true);
    }
}