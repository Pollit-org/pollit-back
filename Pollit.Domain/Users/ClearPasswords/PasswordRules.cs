using Pollit.SeedWork;

namespace Pollit.Domain.Users.ClearPasswords;

public class PasswordMustBeatLeastTenCharactersRule : IBusinessRule
{
    private readonly bool _isValidated;

    public PasswordMustBeatLeastTenCharactersRule(string password)
    {
        _isValidated = password.Length >= 10;
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => "Password must be at least 10 characters.";
}

public class PasswordMustHaveAtLeastOneUppercaseLetterRule : IBusinessRule
{
    private readonly bool _isValidated;

    public PasswordMustHaveAtLeastOneUppercaseLetterRule(string password)
    {
        _isValidated = password.Any(char.IsUpper);
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => "Password must contain at least one uppercase letter.";
}

public class PasswordMustHaveAtLeastOneLowercaseLetterRule : IBusinessRule
{
    private readonly bool _isValidated;

    public PasswordMustHaveAtLeastOneLowercaseLetterRule(string password)
    {
        _isValidated = password.Any(char.IsLower);
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => "Password must contain at least one lowercase letter.";
}

public class PasswordMustHaveAtLeastOneSpecialCharacterRule : IBusinessRule
{
    private readonly bool _isValidated;

    public PasswordMustHaveAtLeastOneSpecialCharacterRule(string password)
    {
        _isValidated = password.Any(c => ! char.IsLetterOrDigit(c));
    }

    public bool IsBroken() => ! _isValidated;

    public string Message => "Password must contain at least one special character.";
}