namespace Pollit.Application.Auth.SignupWithCredentials;

public interface ISignupWithCredentialsPresenter
{
    void Success();

    void EMailAlreadyExists(string error = ApplicationError.EmailAlreadyExists);
    void UserNameAlreadyExists(string error = ApplicationError.UserNameAlreadyExists);
}