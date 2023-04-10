namespace Pollit.Domain.Users;

public class UserPrivateProfileDto
{
    public UserPrivateProfileDto(User user) : this(user.Id, user.Email, user.UserName, user.Birthdate?.ToString(), user.Gender!.Value, "")
    {
        
    }
    
    public UserPrivateProfileDto(Guid userId, string email, string userName, string? birthdate, EGender gender, string avatarUrl)
    {
        UserId = userId;
        Email = email;
        UserName = userName;
        Birthdate = birthdate;
        Gender = gender;
        AvatarUrl = avatarUrl;
    }

    public Guid UserId { get; }
    
    public string Email { get; }
    
    public string UserName { get; }
    
    public string? Birthdate { get; }
    
    public EGender Gender { get; }
    
    public string AvatarUrl { get; }
}