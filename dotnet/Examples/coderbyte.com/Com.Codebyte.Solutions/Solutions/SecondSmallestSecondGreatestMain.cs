using System;
using System.Linq;
using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions
{
    internal class SecondSmallestSecondGreatestMain
    {
        // Array Challenge
        // Have the function ArrayChallenge(arr) take the array of numbers stored in arr and return the second lowest and second greatest numbers, respectively, separated by a space. For example: if arr contains [7, 7, 12, 98, 106] the output should be 12 98. The array will not be empty and will contain at least 2 numbers. It can get tricky if there's just two numbers!
        // Once your function is working, take the final output string and combine it with your ChallengeToken, both in reverse order and separated by a colon.
        // 
        // Your ChallengeToken: token
        // Examples
        // Input: new int[] {1, 42, 42, 180}
        // Output: 42 42
        // Final Output: 24 24:nekot
        // Input: new int[] {4, 90}
        // Output: 90 4
        // Final Output: 4 09:nekot

        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("Second Smallest - Second Greatest");
            Run(new[] { 7, 7, 12, 98, 106 }, "89 21:nekot");
            Run(new[] { 1, 42, 42, 180 }, "24 24:nekot");
            Run(new[] { 4, 90 }, "4 09:nekot");
        }

        private static void Run(int[] input, string finalOutputExpected)
        {
            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            Console.WriteLine($"OUTPUT EXPECTED: {finalOutputExpected}");

            var finalOutput = SecondSmallestSecondGreatest(input);
            Console.WriteLine($"OUTPUT: {finalOutput}");
            Console.WriteLine(finalOutput == finalOutputExpected ? "CORRECT" : "NOT CORRECT");
            System.Console.WriteLine("");
        }

        private static string SecondSmallestSecondGreatest(int[] arr)
        {
            if (arr.Length == 0) return "array will not be empty";
            if (arr.Length < 2) return "array will contain at least 2 numbers";
            
            var secondSmallest = arr.OrderBy(x => x).Distinct().ToList()[1];
            var secondGreatest = arr.OrderByDescending(x => x).Distinct().ToList()[1];
            
            var finalOutput = $"{secondSmallest} {secondGreatest}";
            var finalOutputReversed = StringHelper.Reverse(finalOutput);
            
            var challengeTokenReversed = StringHelper.Reverse("token");
            
            return $"{finalOutputReversed}:{challengeTokenReversed}";
        }
    }
}
