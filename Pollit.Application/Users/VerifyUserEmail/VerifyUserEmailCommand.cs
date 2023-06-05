namespace Pollit.Application.Users.VerifyUserEmail;

public class VerifyUserEmailCommand : IOperation
{
    public VerifyUserEmailCommand(Guid emailVerificationToken, Guid userId)
    {
        EmailVerificationToken = emailVerificationToken;
        UserId = userId;
    }

    public Guid UserId { get; }
    public Guid EmailVerificationToken { get; }
}