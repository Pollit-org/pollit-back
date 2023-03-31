namespace Pollit.Application.Users.SetUserGender;

public interface ISetUserGenderPresenter
{
    void Success();
    
    void UserNotFound(string error = ApplicationError.UserNotFound);
}