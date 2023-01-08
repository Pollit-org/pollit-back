using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SignupWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsPresenter : BasePresenter, ISignupWithCredentialsPresenter
{
    public void Success()
    {
        ActionResult = new OkResult();
    }

    public void EMailAlreadyTaken() 
        => Conflict("Email already exists");
}