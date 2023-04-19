using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithRefreshToken;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithRefreshToken;

[ApiController]
public class SigninWithRefreshTokenController : OperationControllerBase<SigninWithRefreshTokenCommand, ISigninWithRefreshTokenPresenter, SigninWithRefreshTokenPresenter, SigninWithRefreshTokenCommandHandler>
{
    public SigninWithRefreshTokenController(SigninWithRefreshTokenCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin/refreshToken", Name = "SigninWithRefreshToken")]
    public async Task<IActionResult?> SigninWithRefreshTokenAsync([FromBody] SigninWithRefreshTokenHttpRequestBody requestBody)
    {
        var command = new SigninWithRefreshTokenCommand(requestBody.ExpiredAccessToken, requestBody.RefreshToken);

        var presenter = new SigninWithRefreshTokenPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}