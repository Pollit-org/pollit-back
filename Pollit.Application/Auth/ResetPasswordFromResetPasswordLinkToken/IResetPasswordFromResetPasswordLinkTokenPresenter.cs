namespace Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;

public interface IResetPasswordFromResetPasswordLinkTokenPresenter : IPresenter
{
    void Success();
    
    void ResetPasswordLinkNotFoundOrExpired(string error = ApplicationError.ResetPasswordLinkNotFoundOrExpired);
}