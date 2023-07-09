using Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;

namespace Pollit.Infra.Api.Controllers.Auth.ResetPasswordFromResetPasswordLinkToken;

public class ResetPasswordFromResetPasswordLinkTokenPresenter : BasePresenter, IResetPasswordFromResetPasswordLinkTokenPresenter
{
    public void Success() => OkNoContent();

    public void UserDoesNotExist(string error) => NotFound(error);

    public void ResetPasswordLinkNotFoundOrExpired(string error) => Conflict(error);
}