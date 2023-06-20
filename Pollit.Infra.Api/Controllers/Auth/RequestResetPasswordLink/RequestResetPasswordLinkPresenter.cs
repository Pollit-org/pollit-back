using Pollit.Application.Auth.RequestResetPasswordLink;

namespace Pollit.Infra.Api.Controllers.Auth.RequestResetPasswordLink;

public class RequestResetPasswordLinkPresenter : BasePresenter, IRequestResetPasswordLinkPresenter
{
    public void Success() => OkNoContent();

    public void LinkNotFoundOrExpired(string error) => Conflict(error);

    public void UserDoesNotExistError(string error) => NotFound(error);
}