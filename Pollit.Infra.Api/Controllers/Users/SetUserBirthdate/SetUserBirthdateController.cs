﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Users.SetUserBirthdate;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Users.SetUserBirthdate;

[ApiController]
public class SetUserBirthdateController : OperationControllerBase<SetUserBirthdateCommand, ISetUserBirthdatePresenter, SetUserBirthdatePresenter, SetUserBirthdateCommandHandler>
{
    public SetUserBirthdateController(SetUserBirthdateCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserprovider) : base(commandHandler, authenticatedUserprovider)
    {
    }

    [Authorize(Policy = CPolicies.Authenticated)]
    [HttpPatch("users/{userId:guid}/birthdate", Name = "SetUserBirthdate")]
    public async Task<IActionResult?> SetUserBirthdateAsync(Guid userId, [FromBody] SetUserBirthdateHttpRequestBody requestBody)
    {
        var command = new SetUserBirthdateCommand(userId, requestBody.Year, requestBody.Month, requestBody.Day);

        var presenter = new SetUserBirthdatePresenter();

        await HandleOperationAsync(command, presenter);

        return presenter.ActionResult;
    }
}