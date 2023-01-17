using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class UserName : StringValueBase
{
    public UserName(string value) : base(value)
    {
        var brokenRule = new IBusinessRule[]
        {
            new UserNameMustNotBeTooShortRule(value),
            new UserNameMustNotBeTooLongRule(value),
            new UserNameMustContainOnlyAlphanumericCharactersAndDashesRule(value)
        }.FirstOrDefault(rule => rule.IsBroken());

        if (brokenRule is not null)
            throw new BusinessRuleValidationException(brokenRule);
    }

    public static UserName RandomTemporary()
    {
        return new UserName(StringExtensions.RandomString(UserNameMustNotBeTooLongRule.MaxLength, StringExtensions.AlphaNumericLowercaseCharacterSet));
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
}