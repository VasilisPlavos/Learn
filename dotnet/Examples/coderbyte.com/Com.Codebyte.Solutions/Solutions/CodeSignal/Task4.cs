using System;
using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.CodeSignal
{
    public class Task4
    {
        public static void Test()
        {
            var segments = new[] { new[] { -1, 3 }, new[] { -5, -3 }, new[] { 3, 5 }, new[] { 2, 4 }, new[] { -3, -2 }, new[] { -1, 4 }, new[] { 5, 5 } };

            // Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            // Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = Solution(segments);
            Console.WriteLine($"OUTPUT: {output}");
            // Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }

        private static int Solution(int[][] segments)
        {
            var lowerCoord = GetLowerCoord(segments);
            var higherCoord = GetHigherCoord(segments);

            int? bestOption = null;
            int bestOptionFitTimes = 0;
            for (int i = lowerCoord; i < higherCoord; i++)
            {
                int currentOption = i;
                int currentOptionFitTimes = 0;
                foreach (var segment in segments)
                {
                    var small = segment.Min();
                    var large = segment.Max();
                    if (currentOption >= small && currentOption <= large) currentOptionFitTimes += 1;
                }

                if (currentOptionFitTimes > bestOptionFitTimes)
                {
                    bestOptionFitTimes = currentOptionFitTimes;
                    bestOption = currentOption;
                }
            }

            if (bestOption == null) throw new NullReferenceException();
            return (int)bestOption;
        }


        private static int GetHigherCoord(int[][] segments)
        {
            int? higher = null;
            foreach (var segment in segments)
            {
                foreach (var i in segment)
                {
                    if (higher == null) higher = i;
                    if (higher < i) higher = i;
                }
            }

            if (higher == null) throw new NullReferenceException("");
            return (int)higher;
        }

        private static int GetLowerCoord(int[][] segments)
        {
            int? lower = null;
            foreach (var segment in segments)
            {
                foreach (var i in segment)
                {
                    if (lower == null) lower = i;
                    if (lower > i) lower = i;
                }
            }

            if (lower == null) throw new NullReferenceException("");
            return (int)lower;
        }



    }
}