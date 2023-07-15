using System;
using System.Security.Claims;
using OneOf;
using OneOf.Types;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.Birthdates;
using Pollit.Domain.Users.EncryptedPasswords;
using Pollit.Domain.Users.Errors;
using Pollit.Domain.Users.Events;
using Pollit.Domain.Users.ResetPasswordLinks;
using Pollit.Domain.Users.UserNames;
using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class User : EntityBase<UserId>
{
    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    private User()
    {
        
    }
    
    public User(UserId id, Email email, UserName userName, bool hasTemporaryUserName, EncryptedPassword? encryptedPassword, HashSet<RefreshToken> refreshTokens, bool isEmailVerified, EGender? gender, Birthdate? birthdate, GoogleProfileDto? googleProfile, DateTime createdAt, DateTime? lastLoginAt, EmailVerificationToken emailVerificationToken, ICollection<ResetPasswordLink> resetPasswordLinks)
    {
        Id = id;
        Email = email;
        UserName = userName;
        HasTemporaryUserName = hasTemporaryUserName;
        EncryptedPassword = encryptedPassword;
        RefreshTokens = refreshTokens;
        IsEmailVerified = isEmailVerified;
        Gender = gender;
        Birthdate = birthdate;
        GoogleProfile = googleProfile;
        CreatedAt = createdAt;
        LastLoginAt = lastLoginAt;
        EmailVerificationToken = emailVerificationToken;
        ResetPasswordLinks = resetPasswordLinks;
    }

    public static User NewUser(Email email, UserName userName, EncryptedPassword encryptedPassword)
    {
        var user = new User(UserId.NewUserId(), email, userName, false, encryptedPassword, new HashSet<RefreshToken>(), false, null, null, null, DateTime.UtcNow, null, EmailVerificationToken.NewToken(), null);
        user.AddDomainEvent(new UserCreatedEvent(user));

        return user;
    }

    public static User NewUser(Email email)
    {
        var user = new User(UserId.NewUserId(), email, UserName.RandomTemporary(), true, null, new HashSet<RefreshToken>(), true, null, null, null, DateTime.UtcNow, null, EmailVerificationToken.NewToken(), null);
        user.AddDomainEvent(new UserCreatedEvent(user));

        return user;
    }

    public sealed override UserId Id { get; protected set; }
    
    public Email Email { get; protected set; }
    
    public bool HasTemporaryUserName { get; protected set; }

    public bool HasPermanentUserName => !HasTemporaryUserName;
    
    public UserName UserName { get; protected set; }

    public EncryptedPassword? EncryptedPassword { get; protected set; }
    
    public HashSet<RefreshToken> RefreshTokens { get; protected set;  }

    public bool IsEmailVerified { get; protected set; }

    public EGender? Gender { get; protected set; } = EGender.PreferNotToSay;
    
    public Birthdate? Birthdate { get; protected set; }

    public GoogleProfileDto? GoogleProfile { get; protected set; }

    public DateTime CreatedAt { get; protected set; }
    
    public DateTime? LastLoginAt { get; protected set; }
    
    public EmailVerificationToken EmailVerificationToken { get; protected set; }

    public ICollection<ResetPasswordLink> ResetPasswordLinks { get; set; } = new List<ResetPasswordLink>();

    public UserPrivateProfileDto PrivateProfile => new(this);
    
    public UserPublicProfileDto PublicProfile => new(this);

    public void OnSigninWithCredentials()
    {
        LastLoginAt = DateTime.UtcNow;
    }
    
    public void OnSigninWithRefreshToken()
    {
        LastLoginAt = DateTime.UtcNow;
    }
    
    public void OnLoginWithGoogle(GoogleProfileDto googleProfile)
    {
        GoogleProfile = googleProfile;
        IsEmailVerified = true;
        LastLoginAt = DateTime.UtcNow;

        if (googleProfile is {BirthdayYear: { }, BirthdayMonth: { }, BirthdayDay: { }})
            Birthdate = new Birthdate(googleProfile.BirthdayYear.Value, googleProfile.BirthdayMonth.Value, googleProfile.BirthdayDay.Value);

        if (googleProfile.Gender is not null)
            Gender = googleProfile.Gender.ToLower() switch
            {
                "male" => EGender.Male,
                "female" => EGender.Female,
                null => EGender.PreferNotToSay,
                _ => EGender.Other
            };
    }

    public OneOf<Success, EmailVerificationTokenMismatchError> VerifyEmail(EmailVerificationToken emailVerificationToken)
    {
        if (EmailVerificationToken is null || EmailVerificationToken != emailVerificationToken)
            return new EmailVerificationTokenMismatchError();

        IsEmailVerified = true;
        
        return new Success();
    }

    public void RequestResetPasswordLink()
    {
        var resetPasswordLink = ResetPasswordLink.NewResetPasswordLink();
        ResetPasswordLinks.Add(resetPasswordLink);
        
        AddDomainEvent(new ResetPasswordLinkCreatedEvent(resetPasswordLink, this));
    }

    public bool HasAnyResetPasswordLinkConsumableByToken(ResetPasswordToken token)
    {
        return ResetPasswordLinks.Any(link => link.IsConsumableByToken(token));
    }

    public OneOf<Success, ResetPasswordLinkNotFoundOrExpiredError> ResetPasswordFromResetPasswordLinkToken(ResetPasswordToken token, EncryptedPassword encryptedPassword)
    {
        var resetPasswordLink = ResetPasswordLinks.FirstOrDefault(link => link.IsConsumableByToken(token));
        
        if (resetPasswordLink is null)
            return new ResetPasswordLinkNotFoundOrExpiredError();

        return resetPasswordLink
            .ConsumeWith(token)
            .Match<OneOf<Success, ResetPasswordLinkNotFoundOrExpiredError>>(
                success =>
                {
                    EncryptedPassword = encryptedPassword;
                    AddDomainEvent(new PasswordResetEvent(this));
                    
                    return new Success();
                },
                passwordResetTokenMismatchError => new ResetPasswordLinkNotFoundOrExpiredError(),
                passwordResetTokenExpiredError => new ResetPasswordLinkNotFoundOrExpiredError()
            );
    }

    public bool HasRefreshToken(RefreshToken refreshToken)
        => RefreshTokens.Contains(refreshToken);

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        // degueu mais EFCore detecte pas la modif sur les hashet si je fais pas ca
        RefreshTokens = new HashSet<RefreshToken>(RefreshTokens) { refreshToken };
    }

    public bool RemoveRefreshToken(RefreshToken refreshToken)
    {
        // degueu mais EFCore detecte pas la modif sur les hashet si je fais pas ca
        RefreshTokens = new HashSet<RefreshToken>(RefreshTokens);
        return RefreshTokens.Remove(refreshToken);
    }
    
    public OneOf<Success, UserNameIsAlreadyPermanentError> SetPermanentUserName(UserName userName)
    {
        if (HasPermanentUserName)
            return new UserNameIsAlreadyPermanentError();

        UserName = userName;
        HasTemporaryUserName = false;

        return new Success();
    }

    public void SetGender(EGender gender)
    {
        Gender = gender;
    }
    
    public void SetBirthdate(Birthdate? birthdate)
    {
        Birthdate = birthdate;
    }

    public IEnumerable<Claim> GetClaims()
    {
        return new[]
        {
            new Claim(CClaimTypes.UserId, Id.Value.ToString()),
            new Claim(CClaimTypes.Email, Email.Value),
            new Claim(CClaimTypes.IsEmailVerified, IsEmailVerified.ToString()),
            new Claim(CClaimTypes.HasTemporaryUserName, HasTemporaryUserName.ToString()),
            new Claim(CClaimTypes.UserName, UserName.ToString()),
        };
    }
}