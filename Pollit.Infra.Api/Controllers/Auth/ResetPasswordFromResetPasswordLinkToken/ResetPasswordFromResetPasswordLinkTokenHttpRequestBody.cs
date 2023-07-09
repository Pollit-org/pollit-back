namespace Pollit.Infra.Api.Controllers.Auth.ResetPasswordFromResetPasswordLinkToken;

public class ResetPasswordFromResetPasswordLinkTokenHttpRequestBody
{
    public Guid UserId { get; set; }
    public string ResetPasswordToken { get; set; }
    public string NewPassword { get; set; }
}