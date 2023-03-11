using Pollit.Application.Users.SetUserGender;

namespace Pollit.Infra.Api.Controllers.Users.SetUserGender;

public class SetUserGenderPresenter : BasePresenter, ISetUserGenderPresenter
{
    public void Success()
        => OkNoContent();

    public void UserNotFound(string error)
        => NotFound(error);
}