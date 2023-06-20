namespace Pollit.Application.Auth.RequestResetPasswordLink;

public interface IRequestResetPasswordLinkPresenter : IPresenter
{
    void Success();

    void LinkNotFoundOrExpired(string error = ApplicationError.ResetPasswordLinkNotFoundOrExpired);
    
    void UserDoesNotExistError(string error = ApplicationError.UserNotFound);
}