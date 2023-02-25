using Pollit.Domain.Users.UserNames.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.UserNames;

public class UserName : StringValueBase
{
    private const int MinLength = 3;
    public const int MaxLength = 24;
    
    public UserName(string value) : base(value, false)
    {
        switch (value.Length)
        {
            case < MinLength:
                throw new UserNameTooShortException();
            case > MaxLength:
                throw new UserNameTooLongException();
        }

        if (value.First() == '-')
            throw new UserNameStartsWithDashException();
        
        if (value.Last() == '-')
            throw new UserNameEndsWithDashException();
        
        if (value.Contains("--"))
            throw new UserNameHasConsecutiveDashes();

        if (value.ToLower().Any(c => !StringExtensions.AlphaNumericLowercaseCharacterSet.Contains(c) && c != '-'))
        {
            throw new UserNameContainsIllegalCharacter();
        }
    }

    public static UserName RandomTemporary()
    {
        return new UserName(StringExtensions.RandomString(MaxLength, StringExtensions.AlphaNumericLowercaseCharacterSet));
    }
    
    public static bool TryParse(string userNameStr, out UserName userName)
    {
        try
        {
            userName = new UserName(userNameStr);
            return true;
        }
        catch (Exception)
        {
            userName = null!;
            return false;
        }
    }
    
    public static implicit operator UserName(string userName) => new (userName);
    public static implicit operator string(UserName userName) => userName.Value;
}