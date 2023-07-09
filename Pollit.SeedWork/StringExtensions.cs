using System;
using System.Linq;
using System.Security.Cryptography;

namespace Pollit.SeedWork;

public class StringExtensions
{
    public const string AlphaNumericLowercaseCharacterSet = "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string RandomString(int length, string characterSet)
    {
        var random = new Random();
        return string.Concat(Enumerable.Range(0, length).Select(_ => characterSet[random.Next(characterSet.Length)]));
    }
    
    public static string UrlSafeRandomToken(int length)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber)
            .Replace(' ', '.')
            .Replace('+', '.')
            .Replace('%', '.');
    }
}