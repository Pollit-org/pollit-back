namespace Pollit.Application.Auth.VerifyResetPasswordLinkTokenValidity;

public interface IVerifyResetPasswordLinkTokenValidityPresenter : IPresenter
{
    void Success(bool isValid);
}