using System.Globalization;
using System.Text.RegularExpressions;
using Pollit.SeedWork;

namespace Pollit.Domain.ValueObjects.Email;

public class Email : ValueObject
{
    public const int EmailMaxLength = 255;
            
    public Email(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new EmailException();

        Value = email.Trim();

        if (Value.Length >= EmailMaxLength || !IsValidEmail(Value))
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
}