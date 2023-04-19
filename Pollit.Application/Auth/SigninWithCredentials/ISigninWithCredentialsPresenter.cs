using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithCredentials;

public interface ISigninWithCredentialsPresenter : IPresenter
{
    void Success(SigninResult signinResult);
    
    void LoginFailed(string error = ApplicationError.CredentialsSigninFailed);
}