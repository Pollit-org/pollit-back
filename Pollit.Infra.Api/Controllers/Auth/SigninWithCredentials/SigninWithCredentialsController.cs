using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

[ApiController]
public class SigninWithCredentialsController : ControllerBase
{
    private readonly SigninWithCredentialsCommandHandler _commandHandler;

    public SigninWithCredentialsController(SigninWithCredentialsCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [AllowAnonymous]
    [HttpPost("auth/signin", Name = "Signin")]
    public async Task<IActionResult?> SigninAsync([FromBody] SigninWithCredentialsHttpRequestBody requestBody)
    {
        var command = new SigninWithCredentialsCommand(requestBody.EmailOrUserName, new ClearPassword(requestBody.Password));

        var presenter = new SigninWithCredentialsPresenter();
        
        await _commandHandler.HandleAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}