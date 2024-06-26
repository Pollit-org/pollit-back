﻿using Microsoft.AspNetCore.Mvc;
using Pollit.Application.Auth.SigninWithCredentials;
using Pollit.Domain.Users;

namespace Pollit.Infra.Api.Controllers.Auth.SigninWithCredentials;

public class SigninWithCredentialsPresenter : BasePresenter, ISigninWithCredentialsPresenter
{
    public void Success(SigninResult signinResult) => Ok(new SigninResultDto(signinResult));

    public void LoginFailed(string error) => Unauthorized(error);
}