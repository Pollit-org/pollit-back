using Pollit.Application.Auth.SigninWithRefreshToken;
using Pollit.Domain.Users;
using Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithRefreshToken;

public class SigninWithRefreshTokenPresenter : BasePresenter, ISigninWithRefreshTokenPresenter
{
    public void Success(SigninResult signinResult) => Ok(new SigninResultDto(signinResult));

    public void ExpiredAccessTokenIsInvalid(string error) => Unauthorized(error);

    public void RefreshTokenIsInvalid(string error) => Unauthorized(error);

    public void UserDoesNotExist(string error) => Unauthorized(error);
}