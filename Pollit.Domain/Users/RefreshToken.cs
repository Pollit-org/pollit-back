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
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        return new RefreshToken(Convert.ToBase64String(randomNumber).Replace(' ', '.'));
    }
}