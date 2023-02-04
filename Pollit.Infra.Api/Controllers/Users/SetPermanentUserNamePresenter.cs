using Pollit.Application.Users.SetPermanentUserName;

namespace Pollit.Infra.Api.Controllers.Users;

public class SetPermanentUserNamePresenter : BasePresenter, ISetPermanentUserNamePresenter
{
    public void Success()
        => OkNoContent();

    public void UsernameIsAlreadyPermanent(string error)
        => Conflict(error);

    public void UserNotFound(string error)
        => NotFound(error);

    public void UserNameAlreadyExists(string error)
        => Conflict(error);
}