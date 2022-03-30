using System;
using Com.Codebyte.Solutions.Helpers;

namespace Com.Codebyte.Solutions.Solutions
{
    public static class MinWindowSubstringMain
    {
        private static string MinWindowSubstring(string[] strArr)
        {
            // code goes here  
            var wordToCheck = strArr[0];
            var lettersRequired = strArr[1];
            string smallestCompination = null;

            var tempWord = wordToCheck;
            while (tempWord.Length > 0)
            {
                var tempCombination = GetCombination(tempWord, lettersRequired);
                tempWord = tempWord.Remove(0, 1);
                if (tempCombination == null) continue;
                if (smallestCompination == null || smallestCompination.Length > tempCombination.Length)
                {
                    smallestCompination = tempCombination;
                }
            }

            return smallestCompination ?? "";
        }

        private static string GetCombination(string word, string lettersRequired)
        {
            string result = null;
            foreach (var c in word.ToCharArray())
            {
                if (lettersRequired.Length == 0) return result;

                result += c;
                if (lettersRequired.Contains(c))
                {
                    var charIndex = lettersRequired.IndexOf(c);
                    lettersRequired = lettersRequired.Remove(charIndex, 1);
                }
            }

            return lettersRequired.Length != 0 ? null : result;
        }

        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("Min Window Substring");

            Run(new string[] { "aaabaaddae", "aed" }, "dae");
            Run(new string[] { "aabdccdbcacd", "aad" }, "aabd");
            Run(new string[] { "ahffaksfajeeubsne", "jefaa" }, "aksfaje");
            Run(new string[] { "aaffhkksemckelloe", "fhea" }, "affhkkse");
        }

        private static void Run(string[] input, string outputExpected)
        {
            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = MinWindowSubstring(input);
            Console.WriteLine($"OUTPUT: {output}");
            Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }
    }
}