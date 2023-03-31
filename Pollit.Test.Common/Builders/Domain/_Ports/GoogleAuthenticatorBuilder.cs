﻿using Moq;
using Pollit.Domain._Ports;
using Pollit.Domain.Users;

namespace Pollit.Test.Common.Builders.Domain._Ports;

public class GoogleAuthenticatorBuilder : IFluentBuilder<IGoogleAuthenticator>
{
    private Mock<IGoogleAuthenticator> _googleAuthenticatorMock = new();

    public GoogleAuthenticatorBuilder()
    {
        _googleAuthenticatorMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>())).Throws<Exception>();
    }

    public GoogleAuthenticatorBuilder WithAuthCodeReturningProfile(string authCode, GoogleProfile profile)
    {
        _googleAuthenticatorMock.Setup(x => x.AuthenticateAsync(It.Is<string>(c => c == authCode))).ReturnsAsync(profile);
        return this;
    }
    
    public IGoogleAuthenticator Build()
    {
        return _googleAuthenticatorMock.Object;
    }
}