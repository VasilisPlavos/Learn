using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions.CodeSignal
{
    public class CodeSignal
    {
        public static void Tests()
        {
            TestHelper.Console.WriteLine.Start("CodeSignal Test1");
            Task1.Test();

            TestHelper.Console.WriteLine.Start("CodeSignal Test2");
            Task2.Test();

            TestHelper.Console.WriteLine.Start("CodeSignal Test3");
            Task3.Test();

            TestHelper.Console.WriteLine.Start("CodeSignal Test4");
            Task4.Test();
        }
    }
}