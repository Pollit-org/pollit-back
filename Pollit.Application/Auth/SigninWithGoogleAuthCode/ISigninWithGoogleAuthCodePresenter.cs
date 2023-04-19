using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithGoogleAuthCode;

public interface ISigninWithGoogleAuthCodePresenter : IPresenter
{
    void Success(SigninResult signinResult);

    void GoogleAuthCodeAuthenticationFailed(string error = ApplicationError.GoogleSigninFailed);
}