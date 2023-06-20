using Pollit.SeedWork.Eventing;

namespace Pollit.Domain.Users.Events;

public class PasswordResetEvent : DomainEventBase
{
    public PasswordResetEvent(User user)
    {
        User = user;
    }
    
    public User User { get; }
}