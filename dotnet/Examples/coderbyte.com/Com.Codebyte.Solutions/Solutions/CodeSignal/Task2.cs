using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.CodeSignal
{
    public static class Task2
    {
        public static void Test()
        {
            var input = "codesignal";

            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            // Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = Solution("codesignal");
            Console.WriteLine($"OUTPUT: {output}");
            // Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }

        private static string Solution(string s)
        {
            var repeat = true;

            while (repeat)
            {
                var prefixes = GetPrefixes(s);
                var chosenPrefix = GetLongestPalindrome(prefixes);
                if (chosenPrefix.Length < 2)
                {
                    repeat = false;
                }
                else
                {
                    s = s.Replace(chosenPrefix, "");
                }
            }

            return s;
        }



        private static string GetLongestPalindrome(List<string> prefixes)
        {
            var longestPalindrome = "";
            foreach (var prefix in prefixes)
            {
                if (!IsPalindrome(prefix)) continue;
                if (longestPalindrome.Length < prefix.Length) longestPalindrome = prefix;
            }

            return longestPalindrome;
        }

        private static bool IsPalindrome(string value)
        {
            var reversValue = "";
            for (int i = value.Length - 1; i >= 0; i--)
            {
                reversValue += value[i].ToString();
            }
            return reversValue == value;
        }

        private static List<string> GetPrefixes(string pattern)
        {
            return Enumerable.Range(1, pattern.Length).Select(p => pattern.Substring(0, p)).ToList();
        }
    }
}