using System.Text;

namespace Examples.y24.NanoIds.Helpers;

public static class YoutubeConverter
{
    //Payload ASCII/Unicode      Base64     YouTube
    // -------  -------------     ---------  ---------
    //  0...25  \x41 ... \x5A     'A'...'Z'  'A'...'Z'
    // 26...51  \x61 ... \x7A     'a'...'z'  'a'...'z'
    // 52...61  \x30 ... \x39     '0'...'9'  '0'...'9'
    //    62    \x2F vs. \x2D  →   '/' (2F)   '-' (2D)
    //    63    \x2B vs. \x5F  →   '+' (2B)   '_' (5F)

    public static ulong Decode(string youtubeId)
    {
        youtubeId = youtubeId.Replace("-", "+".Replace("_", "/")) + "=";
        var a = Convert.FromBase64String(youtubeId);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(a);
        }

        var output = BitConverter.ToUInt64(a, 0);
        return output;
    }
    public static string GenerateYoutubeIdFromRandomLong()
    {
        return GenerateYoutubeIdFromLong(RandomHelper.GetLong(13));
    }

    public static string GenerateYoutubeIdFromUnixTimeMilliseconds()
    {
        return GenerateYoutubeIdFromLong(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }

    private static string GenerateYoutubeIdFromLong(long l)
    {
        Task.Delay(1).Wait();
        var characterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var charSet = characterSet.ToCharArray();
        var targetBase = charSet.Length;

        string? output = null;

        do
        {
            output += charSet[l % targetBase];
            l = l / targetBase;
        } while (l > 0);

        output = new string(output.Reverse().ToArray());
        output = Convert.ToBase64String(Encoding.UTF8.GetBytes(output))
            .Replace("/", "_")
            .Replace("+", "-")
            .Replace("==", "")
            .Replace("=", "")
            ;

        return output;
    }

    // I think this one is the fastest
    public static string GenerateYoutubeId3()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxy-_";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 13).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}