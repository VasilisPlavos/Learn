namespace Com.Coderbyte.Solutions.Helpers
{
    public static class TestHelper
    {
        public static class Console
        {
            public static class WriteLine
            {
                public static void Start(string testTitle)
                {
                    System.Console.WriteLine($"== TEST: {testTitle} ==");
                    System.Console.WriteLine("");
                }
            }
        }
    }
}