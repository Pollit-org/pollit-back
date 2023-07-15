using OneOf;
using OneOf.Types;
using Pollit.Domain.Users.ResetPasswordLinks.Errors;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.ResetPasswordLinks;

[GenerateOneOf]
public partial class ConsumeResetPasswordLinkError : OneOfBase<Success, PasswordResetTokenMismatchError, PasswordResetTokenExpiredError> { }

public class ResetPasswordLink : EntityBase<ResetPasswordLinkId>
{
    private static readonly TimeSpan ResetPasswordLinkValiditySpan = TimeSpan.FromMinutes(10);

    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    public ResetPasswordLink()
    {
        
    }
    
    internal ResetPasswordLink(ResetPasswordLinkId id, ResetPasswordToken token, DateTime issuedAt, DateTime? invalidatedAt)
    {
        Id = id;
        Token = token;
        IssuedAt = issuedAt;
        ForcedExpiryAt = invalidatedAt;
    }

    public static ResetPasswordLink NewResetPasswordLink()
    {
        return new ResetPasswordLink(ResetPasswordLinkId.NewResetPasswordLinkId(), ResetPasswordToken.Generate(), DateTime.UtcNow, null);
    }

    public override ResetPasswordLinkId Id { get; protected set; }
    public readonly ResetPasswordToken Token;
    public readonly DateTime IssuedAt;
    public DateTime? ForcedExpiryAt { get; protected set; }
    public DateTime? ConsumedAt { get; protected set; }

    private DateTime NaturallyExpiresAt => IssuedAt.Add(ResetPasswordLinkValiditySpan);
    
    public DateTime ExpiresAt => (DateTime) DateTimeExtensions.Min(NaturallyExpiresAt, ForcedExpiryAt, ConsumedAt)!;

    public bool HasExpired() => ExpiresAt <= DateTime.UtcNow;

    public bool IsConsumable() => !HasExpired();
    
    public bool IsConsumableByToken(ResetPasswordToken token) => IsConsumable() && token == Token;

    public ConsumeResetPasswordLinkError ConsumeWith(ResetPasswordToken token)
    {
        if (token != Token)
            return new PasswordResetTokenMismatchError();

        if (HasExpired())
            return new PasswordResetTokenExpiredError();

        ConsumedAt = DateTime.UtcNow;

        return new Success();
    }
}