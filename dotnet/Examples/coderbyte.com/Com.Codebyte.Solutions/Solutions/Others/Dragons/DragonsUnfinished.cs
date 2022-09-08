using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions.Others.Dragons
{
    public class DragonsUnfinishedMain
    {
        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("Dragons");
            TestHelper.Console.WriteLine.Start("!! This solution is unfinished !!");
            var fireDragon = new FireDragon();
            var egg = fireDragon.Lay();
            var childFireDragon = egg.Hatch();
        }
    }
}