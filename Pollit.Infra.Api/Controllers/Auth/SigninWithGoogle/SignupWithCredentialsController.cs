using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogle;
using Pollit.Application.Auth.SignupWithCredentials;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.ClearPasswords;
using Pollit.Infra.Api.Controllers.Auth.SignupWithCredentials;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogle;

[ApiController]
public class SigninWithGoogleController : ControllerBase
{
    private readonly SigninWithGoogleCommandHandler _commandHandler;
    
    public SigninWithGoogleController(SigninWithGoogleCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [HttpPost("auth/signin/google", Name = "SigninWithGoogle")]
    public async Task<IActionResult?> SigninWithGoogleAsync([FromBody] SigninWithGoogleHttpRequestBody requestBody)
    {
        var command = new SigninWithGoogleCommand(requestBody.Code);

        var presenter = new SigninWithGooglePresenter();
        
        await _commandHandler.HandleAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}