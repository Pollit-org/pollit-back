using Pollit.Domain.Users;

namespace Pollit.Application.Users.GetUserPublicProfile;

public interface IGetUserPublicProfilePresenter : IPresenter
{
    void Success(UserPublicProfileDto userPrivateProfile);
    
    void UserNotFound(string error = ApplicationError.UserNotFound);
}