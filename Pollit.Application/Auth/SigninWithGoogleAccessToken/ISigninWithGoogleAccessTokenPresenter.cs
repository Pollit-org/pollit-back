using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithGoogleAccessToken;

public interface ISigninWithGoogleAccessTokenPresenter : IPresenter
{
    void Success(SigninResultDto signinResult);
    
    void GoogleAccessTokenAuthenticationFailed(string error = ApplicationError.GoogleSigninFailed);
}