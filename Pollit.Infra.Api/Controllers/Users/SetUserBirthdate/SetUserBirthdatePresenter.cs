using Pollit.Application.Users.SetUserBirthdate;

namespace Pollit.Infra.Api.Controllers.Users.SetUserBirthdate;

public class SetUserBirthdatePresenter : BasePresenter, ISetUserBirthdatePresenter
{
    public void Success()
        => OkNoContent();

    public void UserNotFound(string error)
        => NotFound(error);
}