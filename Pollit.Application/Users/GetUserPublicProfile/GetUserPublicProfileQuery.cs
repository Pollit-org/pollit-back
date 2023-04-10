namespace Pollit.Application.Users.GetUserPublicProfile;

[OperationAuthorizedForAnyone]
public class GetUserPublicProfileQuery : IOperation
{
    public GetUserPublicProfileQuery(Guid userId)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; }
}