using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithGoogleAccessToken;

public interface ISigninWithGoogleAccessTokenPresenter : IPresenter
{
    void Success(SigninResult signinResult);
    
    void GoogleAccessTokenAuthenticationFailed(string error = ApplicationError.GoogleSigninFailed);
}