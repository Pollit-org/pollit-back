using Pollit.Domain.Users;

namespace Pollit.Application.Users.SetUserGender;

public class SetUserGenderCommand : IOperation
{

    public SetUserGenderCommand(Guid userId, EGender gender)
    {
        Gender = gender;
        UserId = userId;
    }

    [OperationAuthorizedFor] public Guid UserId { get; }
    public EGender Gender;
}