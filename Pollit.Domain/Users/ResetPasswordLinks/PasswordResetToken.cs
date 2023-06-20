using Pollit.SeedWork;

namespace Pollit.Domain.Users.ResetPasswordLinks;

public class PasswordResetToken : StringValueBase
{
    public PasswordResetToken(string value) : base(value, caseSensitive: true)
    {
    }

    public static PasswordResetToken Generate()
    {
        return new PasswordResetToken(StringExtensions.UrlSafeRandomToken(64));
    }
    
    public static implicit operator PasswordResetToken(string tokenValue) => new (tokenValue);
    public static implicit operator string(PasswordResetToken token) => token.Value;
    
}