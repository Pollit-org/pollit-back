using Pollit.Application.Users.SetPermanentUserName;

namespace Pollit.Infra.Api.Controllers.Users;

public class SetPermanentUserNamePresenter : BasePresenter, ISetPermanentUserNamePresenter
{
    public void Success()
        => OkNoContent();

    public void UsernameIsAlreadyPermanent()
        => Conflict("UserName is already permanent.");

    public void UserNotFound()
        => NotFound("User not found.");

    public void UserNameAlreadyExists()
        => Conflict("User mame already exists");
}