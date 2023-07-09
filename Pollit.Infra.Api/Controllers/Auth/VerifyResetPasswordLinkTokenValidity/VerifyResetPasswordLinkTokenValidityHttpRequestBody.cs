namespace Pollit.Infra.Api.Controllers.Auth.VerifyResetPasswordLinkTokenValidity;

public class VerifyResetPasswordLinkTokenValidityHttpRequestBody
{
    public Guid UserId { get; set; }
    public string ResetPasswordToken { get; set; }
}