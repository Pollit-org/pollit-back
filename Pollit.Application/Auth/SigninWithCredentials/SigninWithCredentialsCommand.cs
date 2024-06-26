﻿namespace Pollit.Application.Auth.SigninWithCredentials;

[OperationAuthorizedForAnyone]
public class SigninWithCredentialsCommand : IOperation
{
    public SigninWithCredentialsCommand(string userNameOrEmail, string password)
    {
        UserNameOrEmail = userNameOrEmail;
        Password = password;
    }

    public string UserNameOrEmail { get; }
    public string Password { get; }
}