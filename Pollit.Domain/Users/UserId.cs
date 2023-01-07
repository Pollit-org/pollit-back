using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class UserId : IdValueBase
{
    public UserId(Guid value) : base(value) { }

    public static UserId NewUserId() => new(Guid.NewGuid());
}