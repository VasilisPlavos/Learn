using System;
using Com.Coderbyte.Solutions.Solutions;
using Com.Coderbyte.Solutions.Solutions.CodeSignal;
using Com.Coderbyte.Solutions.Solutions.Codility;

namespace Com.Coderbyte.Solutions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== RUNNING TEST CASES ==");
            Console.WriteLine("");
            // DragonsUnfinishedMain.Test();
            FirstFactorialMain.Test();
            MinWindowSubstringMain.Test();
            SecondSmallestSecondGreatestMain.Test();
            FindWordsFromDictionaryMain.Test();
            CodeSignal.Tests();
            Codility.Tests();

            var a = Console.ReadKey();
        }
    }
}
