using Pollit.Domain.Users.Birthdates.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.Birthdates;

public class Birthdate : DateTimeValueBase
{
    public Birthdate(int year, int month, int day) : base(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc))
    {
    }

    public static Birthdate Parse(string birthdate)
    {
        var split = birthdate.Split("-");

        try
        {
            return new Birthdate(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }
        catch (Exception e)
        {
            throw new BirthdateMalformedException();
        }
    }

    public override string ToString()
    {
        return $"{Value.Year}-{Value.Month.ToString().PadLeft(2, '0')}-{Value.Day.ToString().PadLeft(2, '0')}";
    }
    
    public static implicit operator Birthdate(DateTime dateTime) => new (dateTime.Year, dateTime.Month, dateTime.Day);
    public static implicit operator DateTime(Birthdate birthdate) => birthdate.Value;
}