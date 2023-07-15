namespace Pollit.Application.Auth.RequestResetPasswordLink;

public class RequestResetPasswordLinkCommand : IOperation
{
    public RequestResetPasswordLinkCommand(string? email)
    {
        Email = email;
    }

    public string? Email { get; }
}