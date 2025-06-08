using System.Security.Cryptography;

namespace Examples.y24.NanoIds.Helpers;

internal static class RandomHelper
{
    public static long GetLong(int size)
    {
        var result = "";
        while (size > 0)
        {
            var randomIntToString = RandomNumberGenerator.GetInt32(100000000, 999999999).ToString();
            result = randomIntToString.Length > size ? $"{result}{randomIntToString[..size]}" : $"{result}{randomIntToString}";
            size -= randomIntToString.Length;
        }

        return long.Parse(result);
    }
}