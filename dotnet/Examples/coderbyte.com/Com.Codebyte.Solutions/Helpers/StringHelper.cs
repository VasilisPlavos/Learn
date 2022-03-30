using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Coderbyte.Solutions.Helpers
{
    internal static class StringHelper
    {
        public static string Reverse(string value)
        {
            var valueChars = value.ToCharArray().Reverse().ToArray();
            var valueString = string.Join("", valueChars);
            return valueString;
        }
    }
}
