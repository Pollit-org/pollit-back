using System.Net;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;
using Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;
using Pollit.Infra.Api.Test.x_FluentAssertionExtensions;
using Pollit.Infra.Api.Test.x_Scenario;
using Pollit.Test.Common.Builders.Api.Controllers.SigninWithGoogleAuthCodeControllerBuilder;
using Pollit.Test.Common.Builders.Application.Auth.SignupWithGoogleAuthCode;
using Pollit.Test.Common.Builders.Domain._Ports;
using Pollit.Test.InMemoryDb;
using Xunit;

namespace Pollit.Infra.Api.Test.Controllers.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodeControllerTest
{
    [Fact]
    public async Task When_ISigninInWithGoogleAuthCodeButGoogleAuthenticatorThrowsAnyError_Then_IShouldReceiveGoogleSigninFailedError()
    {
        var googleAuthenticatorBuilder = new GoogleAuthenticatorBuilder();
        new ScenarioWhereGoogleAuthenticatorThrowsExceptionNoMatterWhat(googleAuthenticatorBuilder).Setup();

        var commandHandler = new SigninWithGoogleAuthCodeCommandHandlerBuilder(googleAuthenticatorBuilder.Build()).Build();

        var controller = new SigninWithGoogleAuthCodeControllerBuilder().WithCommandHandler(commandHandler).Build();

        var response = await controller.SigninWithGoogleAuthCodeAsync(new SigninWithGoogleAuthCodeHttpRequestBody {Code = "whatever"});
        
        response.Should().BeObjectResult().WithStatusCode(401).WithError("POLLIT:AUTH:GOOGLE_SIGNIN_FAILED");
    }
    
    [Fact]
    public async Task Given_IDontHaveAnAccountYet_When_ISigninInWithGoogleAuthCode_Then_IShouldReceiveOkResultWithAccessAndRefreshToken_And_ExistAsAUser_And_HaveATemporaryUserName_HaveMyEmailVerified()
    {
        var googleAuthenticatorBuilder = new GoogleAuthenticatorBuilder();
        var database = new InMemoryDatabase();
        
        const string authCode = "ewbufwbveffdfjkwewenfwfufhhujwfbwwebfwefwefubwefwe";
        const string email = "kevin-du-06@pollit.me";
        
        new ScenarioWhereGoogleAuthenticatorReturnsAValidProfileFromMyAuthCode(googleAuthenticatorBuilder)
            .WithCode(authCode)
            .ReturningProfileWithEmail(email)
            .Setup();

        var commandHandler = new SigninWithGoogleAuthCodeCommandHandlerBuilder(googleAuthenticatorBuilder.Build(), database).Build();

        var controller = new SigninWithGoogleAuthCodeControllerBuilder().WithCommandHandler(commandHandler).Build();

        var response = await controller.SigninWithGoogleAuthCodeAsync(new SigninWithGoogleAuthCodeHttpRequestBody {Code = authCode});
        
        response.Should().BeOkObjectResult().WithStatusCode((int) HttpStatusCode.OK).WithValueMatch<SigninResultDto>(_ => true);
        var myUser = database.GetUserRepository().FirstOrDefault(u => u.Email == email);
        myUser.Should().NotBeNull();
        myUser!.HasTemporaryUserName.Should().BeTrue();
        myUser.IsEmailVerified.Should().BeTrue();
    }
}