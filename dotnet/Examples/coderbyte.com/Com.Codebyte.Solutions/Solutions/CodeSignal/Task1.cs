using System;
using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.CodeSignal
{
    public class Task1
    {
        public static void Test()
        {
            var n = 5;
            int[] a = { 4, 0, 1, -2, 3 };
            var input = new { n, a };

            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            // Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

            var output = Solution(n, a);
            Console.WriteLine($"OUTPUT: {string.Join(", ", output)}");
            // Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }

        private static int[] Solution(int n, int[] a)
        {
            int[] b = Enumerable.Range(0, n).ToArray();
            for (int i = 0; i < n; i++)
            {
                var el1 = 0;
                var el2 = 0;
                var el3 = 0;

                try { el1 = a[i - 1]; }
                catch { }
                try { el2 = a[i]; }
                catch { }
                try { el3 = a[i + 1]; }
                catch { }

                b[i] = el1 + el2 + el3;
            }

            return b;
        }
    }
}