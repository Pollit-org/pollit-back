﻿namespace Pollit.Application.Auth.SignupWithCredentials;

public interface ISignupWithCredentialsPresenter
{
    void Success();

    void EMailAlreadyTaken();
}