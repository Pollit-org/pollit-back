﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users.ClearPasswords;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

[ApiController]
public class SigninWithCredentialsController : CommandControllerBase<SigninWithCredentialsCommand, ISigninWithCredentialsPresenter, SigninWithCredentialsPresenter,SigninWithCredentialsCommandHandler>
{

    public SigninWithCredentialsController(SigninWithCredentialsCommandHandler commandHandler) : base(commandHandler)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin", Name = "Signin")]
    public async Task<IActionResult?> SigninAsync([FromBody] SigninWithCredentialsHttpRequestBody requestBody)
    {
        var command = new SigninWithCredentialsCommand(requestBody.EmailOrUserName, new ClearPassword(requestBody.Password));

        var presenter = new SigninWithCredentialsPresenter();
        
        await HandleCommandAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}