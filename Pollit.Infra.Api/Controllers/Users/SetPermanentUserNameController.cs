using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.SetPermanentUserName;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Users;

[ApiController]
public class SetPermanentUserNameController : ControllerBase
{
    private readonly SetPermanentUserNameCommandHandler _commandHandler;

    public SetPermanentUserNameController(SetPermanentUserNameCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPatch("users/{userId:guid}/userName", Name = "SetPermanentUserName")]
    public async Task<IActionResult?> SetPermanentUserNameAsync(Guid userId, [FromBody] SetPermanentUserNameHttpRequestBody requestBody)
    {
        var command = new SetPermanentUserNameCommand(new UserId(userId), new UserName(requestBody.UserName));

        var presenter = new SetPermanentUserNamePresenter();

        await _commandHandler.HandleAsync(command, presenter);

        return presenter.ActionResult;
    }
}