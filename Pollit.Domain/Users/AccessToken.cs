using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class AccessToken : StringValueBase
{
    public AccessToken(string value) : base(value) { }
}