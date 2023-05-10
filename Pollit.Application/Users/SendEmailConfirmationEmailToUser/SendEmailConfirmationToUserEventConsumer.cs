using Pollit.Domain.Users.Events;
using Pollit.SeedWork.Eventing;

namespace Pollit.Application.Users.SendEmailConfirmationEmailToUser;

public class SendEmailConfirmationToUserEventConsumer : IDomainEventConsumer<UserCreatedEvent>
{
    public Task ConsumeAsync(UserCreatedEvent @event)
    {
        if (@event.UserEmailIsVerified)
            return Task.CompletedTask;

        return Task.Delay(1000);
    }
}