using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.SetPermanentUserName;

namespace Pollit.Infra.Api.Controllers.Users;

[ApiController]
public class SetPermanentUserNameController : CommandControllerBase<SetPermanentUserNameCommand, ISetPermanentUserNamePresenter, SetPermanentUserNamePresenter, SetPermanentUserNameCommandHandler>
{

    public SetPermanentUserNameController(SetPermanentUserNameCommandHandler commandHandler) : base(commandHandler)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPatch("users/{userId:guid}/userName", Name = "SetPermanentUserName")]
    public async Task<IActionResult?> SetPermanentUserNameAsync(Guid userId, [FromBody] SetPermanentUserNameHttpRequestBody requestBody)
    {
        var command = new SetPermanentUserNameCommand(userId, requestBody.UserName);

        var presenter = new SetPermanentUserNamePresenter();

        await HandleCommandAsync(command, presenter);

        return presenter.ActionResult;
    }
}