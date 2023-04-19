using Pollit.Application.Auth.SigninWithGoogleAccessToken;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAccessToken;

public class SigninWithGoogleAccessTokenPresenter : BasePresenter, ISigninWithGoogleAccessTokenPresenter
{
    public void Success(SigninResult signinResult) => Ok(new SigninWithCredentials.SigninResultDto(signinResult));

    public void GoogleAccessTokenAuthenticationFailed(string error) => Unauthorized(error);
}