using Pollit.Domain.Users;

namespace Pollit.Application.Users.SetPermanentUserName;

public class SetPermanentUserNameCommand
{
    public SetPermanentUserNameCommand(UserId userId, UserName userName)
    {
        UserId = userId;
        UserName = userName;
    }

    public UserId UserId { get; }
    public UserName UserName { get; }
}