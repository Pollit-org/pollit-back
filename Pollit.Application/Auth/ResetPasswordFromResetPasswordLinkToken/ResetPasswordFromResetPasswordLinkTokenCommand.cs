namespace Pollit.Application.Auth.ResetPasswordFromResetPasswordLinkToken;

[OperationAuthorizedForAnyone]
public class ResetPasswordFromResetPasswordLinkTokenCommand : IOperation
{
    public ResetPasswordFromResetPasswordLinkTokenCommand(Guid userId, string resetPasswordToken, string newPassword)
    {
        UserId = userId;
        ResetPasswordToken = resetPasswordToken;
        NewPassword = newPassword;
    }

    public Guid UserId { get; }
    public string ResetPasswordToken { get; }
    public string NewPassword { get; }
}