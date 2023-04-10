using Pollit.Application.Users.GetUserPrivateProfile;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Users.GetUserPrivateProfile;

public class GetUserPrivateProfilePresenter : BasePresenter, IGetUserPrivateProfilePresenter
{
    public void Success(UserPrivateProfileDto userPrivateProfile) => Ok(userPrivateProfile);

    public void UserNotFound(string error) => NotFound(error);
}