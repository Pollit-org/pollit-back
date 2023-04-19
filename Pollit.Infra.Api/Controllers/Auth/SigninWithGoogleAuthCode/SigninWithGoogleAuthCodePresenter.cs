using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogleAuthCode;
using Pollit.Domain.Users;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;

public class SigninWithGoogleAuthCodePresenter : BasePresenter, ISigninWithGoogleAuthCodePresenter
{
    public void Success(SigninResult signinResult) => Ok(new SigninWithCredentials.SigninResultDto(signinResult));

    public void GoogleAuthCodeAuthenticationFailed(string error) => Unauthorized(error);
}