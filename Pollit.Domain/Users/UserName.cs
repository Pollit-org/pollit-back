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
}