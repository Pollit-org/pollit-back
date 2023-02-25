using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class AccessToken : StringValueBase
{
    public AccessToken(string value) : base(value) { }
    
    public static implicit operator AccessToken(string refreshToken) => new (refreshToken);
    public static implicit operator string(AccessToken refreshToken) => refreshToken.Value;
}