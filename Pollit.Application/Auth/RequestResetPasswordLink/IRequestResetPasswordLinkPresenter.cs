namespace Pollit.Application.Auth.RequestResetPasswordLink;

public interface IRequestResetPasswordLinkPresenter : IPresenter
{
    void Success();

    void UserDoesNotExistError(string error = ApplicationError.UserNotFound);
}