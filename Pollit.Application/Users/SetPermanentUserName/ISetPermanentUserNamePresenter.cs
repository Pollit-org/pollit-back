namespace Pollit.Application.Users.SetPermanentUserName;

public interface ISetPermanentUserNamePresenter
{
    void Success();

    void UsernameIsAlreadyPermanent(string error = ApplicationError.UserNameIsAlreadyPermanent);

    void UserNotFound(string error = ApplicationError.UserNotFound);
    
    void UserNameAlreadyExists(string error = ApplicationError.UserNameAlreadyExists);
}