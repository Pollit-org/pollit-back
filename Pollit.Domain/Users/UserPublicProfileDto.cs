namespace Pollit.Domain.Users;

public class UserPublicProfileDto
{
    public UserPublicProfileDto(User user) : this(user.Id, user.UserName, "")
    {
        
    }
    
    public UserPublicProfileDto(Guid userId, string userName, string avatarUrl)
    {
        UserId = userId;
        UserName = userName;
        AvatarUrl = avatarUrl;
    }

    public Guid UserId { get; }
    
    public string UserName { get; }

    public string AvatarUrl { get; }
}