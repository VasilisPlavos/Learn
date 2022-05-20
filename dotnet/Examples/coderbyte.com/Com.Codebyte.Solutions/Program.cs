using System;
using Com.Coderbyte.Solutions.Solutions;
using Com.Coderbyte.Solutions.Solutions.CodeSignal;

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
            CodeSignal.Tests();
            var a = Console.ReadKey();
        }
    }
}
