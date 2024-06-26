﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithGoogleAuthCode;
using Pollit.Infra.Api.AuthenticatedUserProviders;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithGoogleAuthCode;

[ApiController]
public class SigninWithGoogleAuthCodeController : OperationControllerBase<SigninWithGoogleAuthCodeCommand, ISigninWithGoogleAuthCodePresenter, SigninWithGoogleAuthCodePresenter, SigninWithGoogleAuthCodeCommandHandler>
{
    public SigninWithGoogleAuthCodeController(SigninWithGoogleAuthCodeCommandHandler commandHandler, IAuthenticatedUserProvider authenticatedUserProvider) : base(commandHandler, authenticatedUserProvider)
    {
    }

    [AllowAnonymous]
    [HttpPost("auth/signin/google/authCode", Name = "SigninWithGoogleAuthCode")]
    public async Task<IActionResult?> SigninWithGoogleAuthCodeAsync([FromBody] SigninWithGoogleAuthCodeHttpRequestBody requestBody)
    {
        var command = new SigninWithGoogleAuthCodeCommand(requestBody.Code);

        var presenter = new SigninWithGoogleAuthCodePresenter();
        
        await HandleOperationAsync(command, presenter);
        
        return presenter.ActionResult;
    }
}