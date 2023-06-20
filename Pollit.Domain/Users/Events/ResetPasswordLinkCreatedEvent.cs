using Pollit.Domain.Users.ResetPasswordLinks;
using Pollit.SeedWork.Eventing;

namespace Pollit.Domain.Users.Events;

public class ResetPasswordLinkCreatedEvent : DomainEventBase
{
    public ResetPasswordLinkCreatedEvent(ResetPasswordLink resetPasswordLink, User user)
    {
        ResetPasswordLink = resetPasswordLink;
        User = user;
    }
    
    public ResetPasswordLink ResetPasswordLink { get; }
    public User User { get; }
}