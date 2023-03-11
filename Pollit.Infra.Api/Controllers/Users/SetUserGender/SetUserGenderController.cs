using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.SetUserGender;

namespace Pollit.Infra.Api.Controllers.Users.SetUserGender;

[ApiController]
public class SetUserGenderController : CommandControllerBase<SetUserGenderCommand, ISetUserGenderPresenter, SetUserGenderPresenter, SetUserGenderCommandHandler>
{

    public SetUserGenderController(SetUserGenderCommandHandler commandHandler) : base(commandHandler)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPatch("users/{userId:guid}/gender", Name = "SetUserGender")]
    public async Task<IActionResult?> SetUserGenderAsync(Guid userId, [FromBody] SetUserGenderHttpRequestBody requestBody)
    {
        var command = new SetUserGenderCommand(userId, requestBody.Gender);

        var presenter = new SetUserGenderPresenter();

        await HandleCommandAsync(command, presenter);

        return presenter.ActionResult;
    }
}