using Pollit.Domain.Users;

namespace Pollit.Application.Users.SetUserGender;

public class SetUserGenderCommand : ICommand
{

    public SetUserGenderCommand(Guid userId, EGender gender)
    {
        Gender = gender;
        UserId = userId;
    }

    public Guid UserId { get; }
    public EGender Gender;
}