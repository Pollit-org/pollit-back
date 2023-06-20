using Pollit.SeedWork;

namespace Pollit.Domain.Users.ResetPasswordLinks;

public class ResetPasswordLinkId : IdValueBase
{
    public ResetPasswordLinkId(Guid value) : base(value) { }

    public static ResetPasswordLinkId NewResetPasswordLinkId() => new(Guid.NewGuid());
    
    public static implicit operator ResetPasswordLinkId(Guid resetPasswordLinkId) => new (resetPasswordLinkId);
    public static implicit operator Guid(ResetPasswordLinkId resetPasswordLinkId) => resetPasswordLinkId.Value;
}