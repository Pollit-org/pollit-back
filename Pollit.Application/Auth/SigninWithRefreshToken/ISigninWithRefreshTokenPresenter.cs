using Pollit.Domain.Users;

namespace Pollit.Application.Auth.SigninWithRefreshToken;

public interface ISigninWithRefreshTokenPresenter : IPresenter
{
    void Success(SigninResult signinResult);
    
    void ExpiredAccessTokenIsInvalid(string error = ApplicationError.ExpiredAccessTokenIsInvalid);
    void RefreshTokenIsInvalid(string error = ApplicationError.RefreshTokenIsInvalid);
    void UserDoesNotExist(string error = ApplicationError.UserNotFound);
}