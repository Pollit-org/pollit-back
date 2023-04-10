namespace Pollit.Application.Users.SetUserGender;

public interface ISetUserGenderPresenter : IPresenter
{
    void Success();
    
    void UserNotFound(string error = ApplicationError.UserNotFound);
}