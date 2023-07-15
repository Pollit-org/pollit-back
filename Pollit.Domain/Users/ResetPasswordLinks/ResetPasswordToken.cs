using Pollit.SeedWork;

namespace Pollit.Domain.Users.ResetPasswordLinks;

public class ResetPasswordToken : StringValueBase
{
    public ResetPasswordToken(string value) : base(value, caseSensitive: true)
    {
    }

    public static ResetPasswordToken Generate()
    {
        return new ResetPasswordToken(StringExtensions.UrlSafeRandomToken(64));
    }
    
    public static implicit operator ResetPasswordToken(string tokenValue) => new (tokenValue);
    public static implicit operator string(ResetPasswordToken token) => token.Value;
    
}