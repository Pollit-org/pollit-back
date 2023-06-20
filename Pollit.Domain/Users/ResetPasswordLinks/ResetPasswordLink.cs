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
    
    internal ResetPasswordLink(ResetPasswordLinkId id, PasswordResetToken token, DateTime issuedAt, DateTime? invalidatedAt)
    {
        Id = id;
        Token = token;
        IssuedAt = issuedAt;
        ForcedExpiryAt = invalidatedAt;
    }

    public static ResetPasswordLink NewResetPasswordLink(DateTime utcNow)
    {
        return new ResetPasswordLink(ResetPasswordLinkId.NewResetPasswordLinkId(), PasswordResetToken.Generate(), utcNow, null);
    }

    public override ResetPasswordLinkId Id { get; protected set; }
    public readonly PasswordResetToken Token;
    public readonly DateTime IssuedAt;
    public DateTime? ForcedExpiryAt { get; protected set; }
    public DateTime? ConsumedAt { get; protected set; }

    private DateTime NaturallyExpiresAt => IssuedAt.Add(ResetPasswordLinkValiditySpan);
    
    public DateTime ExpiresAt => (DateTime) DateTimeExtensions.Min(NaturallyExpiresAt, ForcedExpiryAt, ConsumedAt)!;

    public bool HasExpired(DateTime utcNow) => ExpiresAt <= utcNow;

    public bool IsConsumable(DateTime utcNow) => !HasExpired(utcNow);
    
    public bool IsConsumableByToken(PasswordResetToken token, DateTime utcNow) => IsConsumable(utcNow) && token == Token;

    public ConsumeResetPasswordLinkError ConsumeWith(PasswordResetToken token, DateTime utcNow)
    {
        if (token != Token)
            return new PasswordResetTokenMismatchError();

        if (HasExpired(utcNow))
            return new PasswordResetTokenExpiredError();

        ConsumedAt = utcNow;

        return new Success();
    }
}