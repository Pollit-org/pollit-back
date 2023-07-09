namespace Pollit.Infra.Api.Controllers.Auth.VerifyResetPasswordLinkTokenValidity;

public class VerifyResetPasswordLinkTokenValidityHttpQuery
{
    public Guid UserId { get; set; }
    public string ResetPasswordToken { get; set; } = null!;
}