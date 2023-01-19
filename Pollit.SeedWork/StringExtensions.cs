using System;
using System.Linq;

namespace Pollit.SeedWork;

public class StringExtensions
{
    public const string AlphaNumericLowercaseCharacterSet = "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string RandomString(int length, string characterSet)
    {
        var random = new Random();
        return string.Concat(Enumerable.Range(0, length).Select(_ => characterSet[random.Next(characterSet.Length)]));
    }
}