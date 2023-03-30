namespace Pollit.Application.Users.SetUserBirthdate;

public interface ISetUserBirthdatePresenter
{
    void Success();
    
    void UserNotFound(string error = ApplicationError.UserNotFound);
    void BirthdateIsInTheFuture(string error = ApplicationError.BirthdateIsInTheFuture);
}