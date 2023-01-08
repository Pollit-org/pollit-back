using System.Security.Claims;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.EncryptedPasswords;

namespace Pollit.Domain.Users;

public class User
{
    public User(UserId id, Email email, UserName userName, EncryptedPassword encryptedPassword, bool isEmailVerified, DateTime createdAt, DateTime? lastLoginAt)
    {
        Id = id;
        Email = email;
        UserName = userName;
        EncryptedPassword = encryptedPassword;
        IsEmailVerified = isEmailVerified;
        CreatedAt = createdAt;
        LastLoginAt = lastLoginAt;
    }

    public static User NewUser(Email email, UserName userName, EncryptedPassword encryptedPassword) 
        => new (UserId.NewUserId(), email, userName, encryptedPassword, false, DateTime.UtcNow, null);

    public UserId Id { get; protected set; }
    
    public Email Email { get; protected set; }
    
    public UserName UserName { get; protected set; }

    public EncryptedPassword EncryptedPassword { get; protected set; }
    
    public bool IsEmailVerified { get; protected set; }

    public DateTime CreatedAt { get; protected set; }
    
    public DateTime? LastLoginAt { get; protected set; }

    public void OnLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public IEnumerable<Claim> GetClaims()
    {
        return new[]
        {
            new Claim(CClaimTypes.UserId, Id.Value.ToString()),
            new Claim(CClaimTypes.Email, Email.Value),
        };
    }
}