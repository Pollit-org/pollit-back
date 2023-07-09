namespace Pollit.Application.Auth.VerifyResetPasswordLinkTokenValidity;

[OperationAuthorizedForAnyone]
public class VerifyResetPasswordLinkTokenValidityQuery : IOperation
{
    public VerifyResetPasswordLinkTokenValidityQuery(Guid userId, string resetPasswordToken)
    {
        UserId = userId;
        ResetPasswordToken = resetPasswordToken;
    }

    public Guid UserId { get; }
    public string ResetPasswordToken { get; }
}