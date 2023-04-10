using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

[ApiController]
public class SignupWithCredentialsController : OperationControllerBase<SignupWithCredentialsCommand, ISignupWithCredentialsPresenter, SignupWithCredentialsPresenter, SignupWithCredentialsCommandHandler>
{
    public SignupWithCredentialsController(SignupWithCredentialsCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserprovider) : base(commandHandler, authenticatedUserprovider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signup", Name = "Signup")]
    public async Task<IActionResult?> SignupAsync([FromBody] SignupWithCredentialsHttpRequestBody requestBody)
    {
        var command = new SignupWithCredentialsCommand(requestBody.Email, requestBody.UserName, requestBody.Password);

        var presenter = new SignupWithCredentialsPresenter();
        
        await HandleOperationAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}