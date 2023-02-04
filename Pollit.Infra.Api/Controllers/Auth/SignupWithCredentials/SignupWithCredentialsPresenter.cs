using Microsoft.AspNetCore.Mvc;
using Pollit.Application;
using Pollit.Application.Auth.SignupWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

public class SignupWithCredentialsPresenter : BasePresenter, ISignupWithCredentialsPresenter
{
    public void Success()
    {
        ActionResult = new OkResult();
    }

    public void EMailAlreadyExists(string error) 
        => Conflict(error);

    public void UserNameAlreadyExists(string error) 
        => Conflict(error);
}