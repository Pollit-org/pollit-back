using Pollit.Application.Users.GetUserPublicProfile;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Users.GetUserPublicProfile;

public class GetUserPublicProfilePresenter : BasePresenter, IGetUserPublicProfilePresenter
{
    public void Success(UserPublicProfileDto userPrivateProfile) => Ok(userPrivateProfile);

    public void UserNotFound(string error) => NotFound(error);
}