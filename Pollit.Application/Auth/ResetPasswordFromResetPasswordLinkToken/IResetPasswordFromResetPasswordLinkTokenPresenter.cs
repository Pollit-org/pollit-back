namespace Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;

public interface IResetPasswordFromResetPasswordLinkTokenPresenter : IPresenter
{
    void Success();

    void UserDoesNotExist(string error = ApplicationError.UserNotFound);
    
    void ResetPasswordLinkNotFoundOrExpired(string error = ApplicationError.ResetPasswordLinkNotFoundOrExpired);
}