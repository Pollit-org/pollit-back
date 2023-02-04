using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithGoogle;

public interface ISigninWithGooglePresenter
{
    void Success(SigninResult signinResult);

    void GoogleAuthenticationFailed(string error = ApplicationError.GoogleSigninFailed);
}