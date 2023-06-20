using Pollit.Application._Ports;
using Pollit.Domain._Ports;
using Pollit.Domain.Users.Events;
using Pollit.SeedWork.Eventing;

namespace Pollit.Application.Auth.SendResetPasswordLinkEmailToUser;

public class SendResetPasswordLinkEmailToUserEventConsumer : IDomainEventConsumer<ResetPasswordLinkCreatedEvent>
{
    private readonly IEmailVerificationEmailSender _emailVerificationEmailSender;
    private readonly IFrontAppUrlBuilder _frontAppUrlBuilder;

    public SendResetPasswordLinkEmailToUserEventConsumer(IEmailVerificationEmailSender emailVerificationEmailSender, IFrontAppUrlBuilder frontAppUrlBuilder)
    {
        _emailVerificationEmailSender = emailVerificationEmailSender;
        _frontAppUrlBuilder = frontAppUrlBuilder;
    }

    public Task ConsumeAsync(ResetPasswordLinkCreatedEvent @event)
    {
        var resetPasswordUrl = _frontAppUrlBuilder.BuildResetPasswordUrl(@event.User.Id, @event.ResetPasswordLink.Token);

        return _emailVerificationEmailSender.SendResetPasswordLinkEmail(@event.User.Email, @event.User.UserName, resetPasswordUrl);
    }
}