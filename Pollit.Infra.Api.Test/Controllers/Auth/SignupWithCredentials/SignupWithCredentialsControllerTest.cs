using System.Net;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Pollit.Infra.Api.Test.x_FluentAssertionExtensions;
using Pollit.Test.Common.Builders.Api.Controllers.SignupWithCredentialsControllerBuilder;
using Pollit.Test.Common.Builders.Application.Auth.SignupWithCredentials;
using Pollit.Test.InMemoryDb;
using Xunit;

namespace Pollit.Infra.Api.Test.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsControllerTest
{
    [Theory]
    [InlineData("john doe@gmail.com")]
    [InlineData("john.doe.gmail.com")]
    [InlineData("john@doe@gmail.com")]
    public async Task When_ITryToSignupWithAnInvalidEmail_Then_IShouldReceiveInvalidEmailError(string invalidEmail)
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder().Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithEmail(invalidEmail).Build();
        
        var response = await controller.SignupAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:AUTH:INVALID_EMAIL");
    }
    
    [Theory]
    [InlineData("JJdsaj*3f")] // less than 10 characters
    [InlineData("JFjejfKdnmwndWJdnwd")] // no special charcater
    [InlineData("JJDWDJJJDH+@BVHJDVWJ")] // no lowercase character
    [InlineData("fbewifwe@%#&bwhdbqd")] // no uppercase character
    public async Task When_ITryToSignupWithAnInvalidPassword_Then_IShouldReceivePasswordDoesNotMeetRequirementsError(string invalidPassword)
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder().Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithPassword(invalidPassword).Build();
        
        var response = await controller.SignupAsync(requestBody);

        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:AUTH:PASSWORD_DOES_NOT_MEET_REQUIREMENTS");
    }
    
    [Theory]
    [InlineData("Mf")] // less than 3 characters
    [InlineData("MrFlow-DelPueblo-DeLaNocce-Di-Gonzales-Del-Mar")] // more than 32 characters
    [InlineData("Mr--Flow")] // 2 consecutive dashes
    [InlineData("Mr-Flow-")] // ends with dash
    [InlineData("-Mr-Flow")] // starts with dash
    [InlineData("MrFlow!")] // special character
    public async Task When_ITryToSignupWithAnInvalidUserName_Then_IShouldReceiveInvalidUserNameError(string invalidUserName)
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder().Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();
    
        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithUserName(invalidUserName).Build();
        
        var response = await controller.SignupAsync(requestBody);
    
        response.Should().BeObjectResult().WithStatusCode((int)HttpStatusCode.BadRequest).WithError("POLLIT:AUTH:INVALID_USER_NAME");
    }
    
    [Fact]
    public async Task Given_AUserExistsWithAGivenEmail_When_ITryToSignupWithThatSameEmail_Then_IShouldReceiveEmailAlreadyExistsError()
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder()
            .WithExistingUserHavingEmail("mrflow@pollit.me")
            .Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();

        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithEmail("mrflow@pollit.me").Build();
        
        var response = await controller.SignupAsync(requestBody);

        response.Should().BeObjectResult().WithStatusCode(409).WithError("POLLIT:AUTH:EMAIL_ALREADY_EXISTS");
    }
    
    [Fact]
    public async Task Given_AUserExistsWithAGivenUserName_When_ITryToSignupWithThatSameUserNameEvenThoTheCaseIsDifferent_Then_IShouldReceiveUserNameAlreadyExistsError()
    {
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder()
            .WithExistingUserHavingUserName("MrFlow")
            .Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();

        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithUserName("mrflow").Build();
        
        var response = await controller.SignupAsync(requestBody);

        response.Should().BeObjectResult().WithStatusCode(409).WithError("POLLIT:AUTH:USER_NAME_ALREADY_EXISTS");
    }
    
    [Fact]
    public async Task Given_NoUserHasMyEmailNorUserName_When_ITryToSignupWithValidCredentials_Then_IShouldReceiveOkResult_And_IShouldExistAsAUser()
    {
        var database = new InMemoryDatabase();
        
        var commandHandler = new SignupWithCredentialsCommandHandlerBuilder(database).Build();
        
        var controller = new SignupWithCredentialsControllerBuilder().WithCommandHandler(commandHandler).Build();

        var requestBody = new SignupWithCredentialsHttpRequestBodyBuilder().WithValidCredentials().Build();
        
        var response = await controller.SignupAsync(requestBody);

        response.Should().BeOkResult();
        database.GetUserRepository().Exists(u => u.Email == requestBody.Email).Should().BeTrue();
    }
}