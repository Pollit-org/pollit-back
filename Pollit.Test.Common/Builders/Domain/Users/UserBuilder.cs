using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;
using Pollit.Domain.Users.EncryptedPasswords;

namespace Pollit.Test.Common.Builders.Domain.Users;

public class UserBuilder : IFluentBuilder<User>
{
    private UserId _id = UserId.NewUserId();
    private Email _email = new ("john.doe@pollit.me");
    private UserName _userName = new("John-Doe");
    private EncryptedPassword _encryptedPassword;
    private bool _hasTemporaryUserName = false;
    private HashSet<RefreshToken> _refreshTokens = new();
    private bool _isEmailVerified = false;
    private EGender? _gender;
    private DateTime? _birthday;
    private GoogleProfile? _googleProfile;
    private DateTime _createdAt = DateTime.UtcNow;
    private DateTime? _lastLoginAt;

    public UserBuilder WithId(UserId id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithId(Guid id) => WithId(new UserId(id));
    public UserBuilder WithId(string id) => WithId(Guid.Parse(id));

    public UserBuilder WithEmail(Email email)
    {
        _email = email;
        return this;
    }

    public UserBuilder WithEmail(string email) 
        => WithEmail(new Email(email));
    
    public UserBuilder WithUserName(UserName userName)
    {
        _userName = userName;
        return this;
    }

    public UserBuilder WithUserName(string userName) 
        => WithUserName(new UserName(userName));
    
    public UserBuilder WithTemporaryUserName(bool hasTemporaryUserName = true)
    {
        this._hasTemporaryUserName = hasTemporaryUserName;
        return this;
    }

    public UserBuilder WithPermanentUserName(bool hasPermanentUserName = true) => WithTemporaryUserName(!hasPermanentUserName);

    public UserBuilder WithEncryptedPassword(EncryptedPassword encryptedPassword)
    {
        _encryptedPassword = encryptedPassword;
        return this;
    }
    
    public UserBuilder WithRefreshTokens(params RefreshToken[] refreshTokens)
    {
        _refreshTokens = new HashSet<RefreshToken>(refreshTokens);
        return this;
    }
    
    public UserBuilder WithEmailVerified(bool isEmailVerified = true)
    {
        _isEmailVerified = isEmailVerified;
        return this;
    }
    
    public UserBuilder WithEmailNotVerified() => WithEmailVerified(false);
    
    public UserBuilder WithGender(EGender? gender)
    {
        _gender = gender;
        return this;
    }
    
    public UserBuilder WithBirthday(DateTime? birthday)
    {
        _birthday = birthday;
        return this;
    }
    
    public UserBuilder WithGoogleProfile(GoogleProfile? googleProfile)
    {
        _googleProfile = googleProfile;
        return this;
    }

    public UserBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }
    
    public UserBuilder WithLastLoginAt(DateTime? lastLoginAt)
    {
        _lastLoginAt = lastLoginAt;
        return this;
    }

    public User Build()
    {
        return new User(_id, _email, _userName, _hasTemporaryUserName, _encryptedPassword, _refreshTokens, _isEmailVerified, _gender, _birthday, _googleProfile, _createdAt, _lastLoginAt);
    }
}