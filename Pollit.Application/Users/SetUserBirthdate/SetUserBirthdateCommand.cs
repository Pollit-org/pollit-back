namespace Pollit.Application.Users.SetUserBirthdate;

public class SetUserBirthdateCommand : IOperation
{
    public SetUserBirthdateCommand(Guid userId, int year, int month, int day)
    {
        UserId = userId;
        Year = year;
        Month = month;
        Day = day;
    }

    [OperationAuthorizedFor] public Guid UserId { get; }
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
}