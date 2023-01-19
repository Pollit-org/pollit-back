using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

[ApiController]
public class SignupWithCredentialsController : ControllerBase
{
    private readonly SignupWithCredentialsCommandHandler _commandHandler;

    public SignupWithCredentialsController(SignupWithCredentialsCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [HttpPost("auth/signup", Name = "Signup")]
    public async Task<IActionResult?> SignupAsync([FromBody] SignupWithCredentialsHttpRequestBody requestBody)
    {
        var command = new SignupWithCredentialsCommand(new Email(requestBody.Email), new UserName(requestBody.UserName), new ClearPassword(requestBody.Password));

        var presenter = new SignupWithCredentialsPresenter();
        
        await _commandHandler.HandleAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}