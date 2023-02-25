using System;
using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class UserId : IdValueBase
{
    public UserId(Guid value) : base(value) { }

    public static UserId NewUserId() => new(Guid.NewGuid());
    
    public static implicit operator UserId(Guid userid) => new (userid);
    public static implicit operator Guid(UserId userid) => userid.Value;
}