using Pollit.Domain.Users;

namespace Pollit.Application.Users.GetUserPrivateProfile;

public interface IGetUserPrivateProfilePresenter : IPresenter
{
    void Success(UserPrivateProfileDto userPrivateProfile);
    
    void UserNotFound(string error = ApplicationError.UserNotFound);
}