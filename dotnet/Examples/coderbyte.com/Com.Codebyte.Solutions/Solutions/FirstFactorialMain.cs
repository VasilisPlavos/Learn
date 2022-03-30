using System;
using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions
{
    public class FirstFactorialMain
    {
        private static int FirstFactorial(int num)
        {
            // code goes here
            int res = 1, i;
            for (i = 2; i <= num; i++) res *= i;
            num = res;

            return num;
        }
        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("First Factorial");
            Run(4, 24);
            Run(8, 40320);
        }

        private static void Run(int input, int outputExpected)
        {
            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = FirstFactorial(input);
            Console.WriteLine($"OUTPUT: {output}");
            Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            System.Console.WriteLine("");
        }


    }
}