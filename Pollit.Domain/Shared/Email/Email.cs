using System.Globalization;
using System.Text.RegularExpressions;
using Pollit.SeedWork;

namespace Pollit.Domain.Shared.Email;

public class Email : ValueObject
{
    public const int MaxLength = 255;
            
    public Email(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new EmailException();

        Value = email.Trim();

        if (Value.Length >= MaxLength || !IsValidEmail(Value))
            throw new EmailException();
    }

    public string Value { get; }

    public override string ToString() => Value;

    private static bool IsValidEmail(string email)
    {
        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool TryParse(string emailStr, out Email email)
    {
        try
        {
            email = new Email(emailStr);
            return true;
        }
        catch (EmailException)
        {
            email = null!;
            return false;
        }
    }
}