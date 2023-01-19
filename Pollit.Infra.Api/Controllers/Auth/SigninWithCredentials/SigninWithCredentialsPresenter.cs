using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

public class SigninWithCredentialsPresenter : BasePresenter, ISigninWithCredentialsPresenter
{
    public void Success(SigninResult signinResult)
    {
        ActionResult = new OkObjectResult(new
        {
            AccessToken = signinResult.AccessToken.ToString(),
            RefreshToken = signinResult.RefreshToken.ToString()
        });
    }

    public void LoginFailed() 
        => Unauthorized("Login failed");
}