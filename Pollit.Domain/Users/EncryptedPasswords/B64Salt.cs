using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.EncryptedPasswords;

public class B64Salt : ValueObject
{
    public B64Salt(string value)
    {
        Value = value;
    }
    
    public B64Salt(byte[] value) : this(Convert.ToBase64String(value))
    {
    }

    public string Value { get; }

    [Pure]
    public byte[] AsByteArray() => Convert.FromBase64String(Value);

    public static B64Salt NewSalt()
    {
        var salt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        
        var b64Salt = Convert.ToBase64String(salt);

        return new B64Salt(b64Salt);
    }
}