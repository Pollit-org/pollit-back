using Pollit.SeedWork;

namespace Pollit.Domain.Users.EncryptedPasswords;

public class EncryptedPassword : ValueObject
{
    public EncryptedPassword(B64Salt salt, string hash)
    {
        Salt = salt;
        Hash = hash;
    }

    public static EncryptedPassword Parse(string value)
    {
        var salt = new B64Salt(value.Split('|')[0]);
        var hash = value.Split('|')[1];

        return new EncryptedPassword(salt, hash);
    }

    public B64Salt Salt { get; }
    
    public string Hash { get; }

    public override string ToString() => $"{Salt.Value}|{Hash}";
}
