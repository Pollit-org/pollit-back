namespace Pollit.Application.Users.SetPermanentUserName;

public interface ISetPermanentUserNamePresenter
{
    void Success();

    void UsernameIsAlreadyPermanent();

    void UserNotFound();
    
    void UserNameAlreadyExists();
}