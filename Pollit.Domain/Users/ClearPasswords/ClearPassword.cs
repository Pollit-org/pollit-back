using System.Linq;
using Pollit.Domain.Users.ClearPasswords.Exceptions;
using Pollit.SeedWork;

namespace Pollit.Domain.Users.ClearPasswords;

public class ClearPassword : ValueObject
{
    public ClearPassword(string? value)
    {
        value ??= "";

        if (value.Length < 10)
            throw new PasswordTooShortException();
        
        if (value.All(c => !char.IsLower(c)))
            throw new PasswordHasNoLowerCaseLetterException();

        if (value.All(c => !char.IsUpper(c)))
            throw new PasswordHasNoUpperCaseLetterException();

        if (value.All(char.IsLetterOrDigit))
            throw new PasswordHasNoSpecialCharacterException();

        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
    
    public static implicit operator ClearPassword(string clearPassword) => new (clearPassword);
    public static implicit operator string(ClearPassword clearPassword) => clearPassword.Value;
}
