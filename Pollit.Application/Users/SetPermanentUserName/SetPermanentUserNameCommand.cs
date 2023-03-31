using Pollit.Domain.Users;

namespace Pollit.Application.Users.SetPermanentUserName;

public class SetPermanentUserNameCommand
{
    public SetPermanentUserNameCommand(Guid userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public Guid UserId { get; }
    public string UserName { get; }
}