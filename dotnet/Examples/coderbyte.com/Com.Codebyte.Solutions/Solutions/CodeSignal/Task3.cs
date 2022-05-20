using System;

namespace Com.Coderbyte.Solutions.Solutions.CodeSignal
{
    public class Task3
    {
        public static void Test()
        {
            
            Console.WriteLine($"INPUT: {string.Join(", ", "super", "tower")}");
            // Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = Solution("super", "tower");
            Console.WriteLine($"OUTPUT: {output}");
            // Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }

        private static string Solution(string s1, string s2)
        {
            var result = "";
            while (s1.Length + s2.Length > 0)
            {
                var lexSmaller = GetLexicographicallySmaller(s1, s2);
                if (lexSmaller == s1)
                {
                    var s1FirstLetter = s1[..1];
                    s1 = s1[1..];
                    result = $"{result}{s1FirstLetter}";
                }
                else
                {
                    var s2FirstLetter = s2[..1];
                    s2 = s2[1..];
                    result = $"{result}{s2FirstLetter}";
                }
            }

            return result;
        }

        private static string GetLexicographicallySmaller(string str1, string str2)
        {
            if (str1 == "") return str2;
            if (str1 == "") return str1;
            if (string.CompareOrdinal(str1, str2) < 0) return str1;
            return str2;
        }

    }
}