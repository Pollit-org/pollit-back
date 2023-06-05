namespace Pollit.Application.Users.VerifyUserEmail;

public interface IVerifyUserEmailPresenter : IPresenter
{
    void Success();

    void UserNotFound(string error = ApplicationError.UserNotFound);
    
    void VerificationTokenMismatch(string error = ApplicationError.EmailVerificationTokenMismatch);
}