using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Pollit.Domain._Ports;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Infra.Mailing.Mailjet;

public class EmailSender : IEmailVerificationEmailSender
{
    private readonly MailjetConfig _mailjetConfig;

    public EmailSender(MailjetConfig mailjetConfig)
    {
        _mailjetConfig = mailjetConfig;
    }

    public Task SendEmailVerificationEmail(Email email, UserName userName, Uri verifyEmailLinkUrl)
    {
        return SendEmail(email, userName, 4518999, new Dictionary<string, object>()
        {
            {"confirmation_link", verifyEmailLinkUrl.ToString()},
            {"user_name", userName.ToString()}
        });
    }

    public async Task SendEmail(Email email, UserName userName, int templateId, IDictionary<string, object> variables)
    {
        var transactionalEmail = new TransactionalEmailBuilder()
            .WithTo(new SendContact(email.ToString()))
            .WithTemplateId(templateId)
            .WithTemplateLanguage(true)
            .WithVariables(variables)
            .Build();

        var client = new MailjetClient(_mailjetConfig.ApiKey, _mailjetConfig.SecretKey);
        var response = await client.SendTransactionalEmailAsync(transactionalEmail);
    }
}