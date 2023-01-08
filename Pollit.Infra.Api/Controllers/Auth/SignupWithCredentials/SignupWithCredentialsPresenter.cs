using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SignupWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsPresenter : BasePresenter, ISignupWithCredentialsPresenter
{
    public void Success()
    {
        ActionResult = new OkResult();
    }

    public void EMailAlreadyExists() 
        => Conflict("Email already exists");

    public void UserNameAlreadyExists() 
        => Conflict("User mame already exists");
}