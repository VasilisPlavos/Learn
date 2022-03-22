using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Example.Cloudon.API.Helpers
{
    public class StringHelper
    {
        public static string ToSha256String(string value)
        {
            using var hash = SHA256.Create();
            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(x => x.ToString("x2"))
            );
        }
    }
}