using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithCredentials;

public interface ISigninWithCredentialsPresenter : IPresenter
{
    void Success(SigninResultDto signinResult);
    
    void LoginFailed(string error = ApplicationError.CredentialsSigninFailed);
}