using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogleAccessToken;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAccessToken;

[ApiController]
public class SigninWithGoogleAccessTokenController : OperationControllerBase<SigninWithGoogleAccessTokenCommand, ISigninWithGoogleAccessTokenPresenter, SigninWithGoogleAccessTokenPresenter, SigninWithGoogleAccessTokenCommandHandler>
{
    public SigninWithGoogleAccessTokenController(SigninWithGoogleAccessTokenCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin/google/accessToken", Name = "SigninWithGoogleAccessToken")]
    public async Task<IActionResult?> SigninWithGoogleAccessTokenAsync([FromBody] SigninWithGoogleAccessTokenHttpRequestBody requestBody)
    {
        var command = new SigninWithGoogleAccessTokenCommand(requestBody.AccessToken);

        var presenter = new SigninWithGoogleAccessTokenPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}