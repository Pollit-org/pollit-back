using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogle;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogle;

public class SigninWithGooglePresenter : BasePresenter, ISigninWithGooglePresenter
{
    public void Success(SigninResult signinResult)
    {
        ActionResult = new OkObjectResult(new
        {
            AccessToken = signinResult.AccessToken.ToString(),
            RefreshToken = signinResult.RefreshToken.ToString()
        });
    }

    public void GoogleAuthenticationFailed()
        => Unauthorized("Google authentication failed");
}