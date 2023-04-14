using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users.ClearPasswords;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

[ApiController]
public class SigninWithCredentialsController : OperationControllerBase<SigninWithCredentialsCommand, ISigninWithCredentialsPresenter, SigninWithCredentialsPresenter,SigninWithCredentialsCommandHandler>
{

    public SigninWithCredentialsController(SigninWithCredentialsCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserprovider) : base(commandHandler, authenticatedUserprovider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin", Name = "Signin")]
    public async Task<IActionResult?> SigninAsync([FromBody] SigninWithCredentialsHttpRequestBody requestBody)
    {
        var command = new SigninWithCredentialsCommand(requestBody.EmailOrUserName, new ClearPassword(requestBody.Password));

        var presenter = new SigninWithCredentialsPresenter();
        
        await HandleOperationAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}