using System;
using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class EmailVerificationToken : IdValueBase
{
    public EmailVerificationToken(Guid value) : base(value) { }

    public static EmailVerificationToken NewToken() => new(Guid.NewGuid());
    
    public static implicit operator EmailVerificationToken(Guid emailVerificationToken) => new (emailVerificationToken);
    public static implicit operator Guid(EmailVerificationToken emailVerificationToken) => emailVerificationToken.Value;
}