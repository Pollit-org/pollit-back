using Pollit.Application.Users.VerifyUserEmail;

namespace Pollit.Infra.Api.Controllers.Users.VerifyUserEmail;

public class VerifyUserEmailPresenter : BasePresenter, IVerifyUserEmailPresenter
{
    public void Success()
        => OkNoContent();

    public void UserNotFound(string error)
        => NotFound(error);

    public void VerificationTokenMismatch(string error)
        => Conflict(error);
}