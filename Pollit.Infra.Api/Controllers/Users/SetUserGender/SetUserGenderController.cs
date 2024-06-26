﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.SetUserGender;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Users.SetUserGender;

[ApiController]
public class SetUserGenderController : OperationControllerBase<SetUserGenderCommand, ISetUserGenderPresenter, SetUserGenderPresenter, SetUserGenderCommandHandler>
{

    public SetUserGenderController(SetUserGenderCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserprovider) : base(commandHandler, authenticatedUserprovider)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPatch("users/{userId:guid}/gender", Name = "SetUserGender")]
    public async Task<IActionResult?> SetUserGenderAsync(Guid userId, [FromBody] SetUserGenderHttpRequestBody requestBody)
    {
        var command = new SetUserGenderCommand(userId, requestBody.Gender);

        var presenter = new SetUserGenderPresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}