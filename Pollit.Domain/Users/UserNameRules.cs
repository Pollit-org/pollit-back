using System.Linq;
using Pollit.SeedWork;

namespace Pollit.Domain.Users;

public class UserNameMustNotBeTooShortRule : IBusinessRule
{
    private const int MinLength = 3;
    private readonly bool _isValidated;

    public UserNameMustNotBeTooShortRule(string password)
    {
        _isValidated = password.Length >= MinLength;
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => $"User name must be at least {MinLength} characters long.";
}

public class UserNameMustNotBeTooLongRule : IBusinessRule
{
    public const int MaxLength = 32;
    
    private readonly bool _isValidated;

    public UserNameMustNotBeTooLongRule(string password)
    {
        _isValidated = password.Length <= MaxLength;
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => $"User name must be no more than {MaxLength} characters long.";
}

public class UserNameMustContainOnlyAlphanumericCharactersAndDashesRule : IBusinessRule
{
    private readonly bool _isValidated;

    public UserNameMustContainOnlyAlphanumericCharactersAndDashesRule(string password)
    {
        _isValidated = password.ToLower().All(c => c == '-' || StringExtensions.AlphaNumericLowercaseCharacterSet.Contains(c));
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => "User name must be made of alphanumeric characters and '-' only";
}