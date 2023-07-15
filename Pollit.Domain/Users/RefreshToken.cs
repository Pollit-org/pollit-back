using System;
using System.Security.Cryptography;
using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class RefreshToken : StringValueBase
{
    public RefreshToken(string value) : base(value)
    {
    }

    public static RefreshToken Generate()
    {
        return new RefreshToken(StringExtensions.UrlSafeRandomToken(64));
    }
    
    public static implicit operator RefreshToken(string refreshToken) => new (refreshToken);
    public static implicit operator string(RefreshToken refreshToken) => refreshToken.Value;
}