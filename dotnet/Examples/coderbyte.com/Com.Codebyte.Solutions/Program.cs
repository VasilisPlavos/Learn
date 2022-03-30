using System;
using Com.Coderbyte.Solutions.Solutions;

namespace Com.Coderbyte.Solutions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== RUNNING TEST CASES ==");
            Console.WriteLine("");
            FirstFactorialMain.Test();
            MinWindowSubstringMain.Test();
            SecondSmallestSecondGreatestMain.Test();
            FindWordsFromDictionaryMain.Test();
            var a = Console.ReadKey();
        }
    }
}
