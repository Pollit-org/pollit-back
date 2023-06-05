using Pollit.Application._Ports;
using Pollit.Domain._Ports;
using Pollit.Domain.Users.Events;
using Pollit.SeedWork.Eventing;

namespace Pollit.Application.Users.SendEmailConfirmationEmailToUser;

public class SendEmailConfirmationToUserEventConsumer : IDomainEventConsumer<UserCreatedEvent>
{
    private readonly IEmailVerificationEmailSender _emailVerificationEmailSender;
    private readonly IFrontAppUrlBuilder _frontAppUrlBuilder;

    public SendEmailConfirmationToUserEventConsumer(IEmailVerificationEmailSender emailVerificationEmailSender, IFrontAppUrlBuilder frontAppUrlBuilder)
    {
        _emailVerificationEmailSender = emailVerificationEmailSender;
        _frontAppUrlBuilder = frontAppUrlBuilder;
    }

    public Task ConsumeAsync(UserCreatedEvent @event)
    {
        if (@event.UserEmailIsVerified)
            return Task.CompletedTask;

        var verifyEmailLinkUrl = _frontAppUrlBuilder.BuildVerifyEmailUrl(@event.UserId, @event.UserEmailVerificationToken);

        return _emailVerificationEmailSender.SendEmailVerificationEmail(@event.UserEmail, @event.UserName, verifyEmailLinkUrl);
    }
}