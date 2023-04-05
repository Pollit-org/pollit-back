namespace Pollit.Application.Users.GetUserPrivateProfile;

public class GetUserPrivateProfileQuery : IOperation
{
    public GetUserPrivateProfileQuery(Guid userId)
    {
        UserId = userId;
    }

    [OperationAuthorizedFor]
    public Guid UserId { get; }
}