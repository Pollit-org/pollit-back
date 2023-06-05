using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.UserNames;
using Pollit.SeedWork.Eventing;

namespace Pollit.Domain.Users.Events;

public class UserCreatedEvent : DomainEventBase
{
    public UserCreatedEvent(User user) : this(user.Id, user.UserName, user.Email, user.IsEmailVerified, user.EmailVerificationToken) { }
    
    public UserCreatedEvent(UserId userId, UserName userName, Email userEmail, bool userEmailIsVerified, EmailVerificationToken emailVerificationToken)
    {
        UserId = userId;
        UserName = userName;
        UserEmail = userEmail;
        UserEmailIsVerified = userEmailIsVerified;
        UserEmailVerificationToken = emailVerificationToken;
    }

    public EmailVerificationToken UserEmailVerificationToken { get; set; }

    public UserId UserId { get; }
    
    public UserName UserName { get; }
    
    public Email UserEmail { get; }
    
    public bool UserEmailIsVerified { get; }
}