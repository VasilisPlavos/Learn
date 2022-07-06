using System;
using System.Collections.Generic;
using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions.Codility
{
    // This is a demo task.

    // Write a function:
    // class Solution { public int solution(int[] A); }

    //that, given an array A of N integers, returns the smallest positive integer(greater than 0) that does not occur in A.

    //For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.
    //    Given A = [1, 2, 3], the function should return 4.
    //    Given A = [−1, −3], the function should return 1.

    //    Assume that:
    //  N is an integer within the range[1..100, 000];
    //  each element of array A is an integer within the range[−1, 000, 000..1, 000, 000].


    public class MissingInteger
    {
        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("First Factorial");

            var input = new [] { 1, 3, 6, 4, 1, 2 };
            var outputExpected = 5;
            Run(input, outputExpected);

            input = new[] { 1, 2, 3 };
            outputExpected = 4;
            Run(input, outputExpected);
            
            input = new[] { (-1),(-3) };
            outputExpected = 1;
            Run(input, outputExpected);
        }

        private static void Run(int[] input, int outputExpected)
        {
            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = solution(input);
            Console.WriteLine($"OUTPUT: {output}");

            Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            System.Console.WriteLine("");
        }

        private static int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int min = 1;
            var numbers = new List<int>();
            foreach (var n in A)
            {
                if (!numbers.Contains(n))
                {
                    numbers.Add(n);
                    if (n == min)
                        while (numbers.Contains(++min));
                }
            }
            return min;
        }
    }
}