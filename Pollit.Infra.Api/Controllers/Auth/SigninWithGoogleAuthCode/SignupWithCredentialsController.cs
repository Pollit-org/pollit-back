using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogleAuthCode;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;

[ApiController]
public class SigninWithGoogleAuthCodeController : CommandControllerBase<SigninWithGoogleAuthCodeCommand, ISigninWithGoogleAuthCodePresenter, SigninWithGoogleAuthCodePresenter, SigninWithGoogleAuthCodeCommandHandler>
{
    public SigninWithGoogleAuthCodeController(SigninWithGoogleAuthCodeCommandHandler commandHandler) : base(commandHandler)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin/google", Name = "SigninWithGoogle")]
    public async Task<IActionResult?> SigninWithGoogleAuthCodeAsync([FromBody] SigninWithGoogleAuthCodeHttpRequestBody requestBody)
    {
        var command = new SigninWithGoogleAuthCodeCommand(requestBody.Code);

        var presenter = new SigninWithGoogleAuthCodePresenter();
        
        await HandleCommandAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}