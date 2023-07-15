using Pollit.Application.Auth.VerifyResetPasswordLinkTokenValidity;

namespace Pollit.Infra.Api.Controllers.Auth.VerifyResetPasswordLinkTokenValidity;

public class VerifyResetPasswordLinkTokenValidityPresenter : BasePresenter, IVerifyResetPasswordLinkTokenValidityPresenter
{
    public void Success(bool isValid)
    {
        Ok(new VerifyResetPasswordLinkTokenValidityHttpResponse
        {
            IsValid = isValid
        });
    }
}