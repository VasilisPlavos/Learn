using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Com.Coderbyte.Solutions.Solutions.Codility
{
    public class Task1
    {
        public static void Test()
        {
            var T = new string[50];
            var R = new string[50];

            T[0] = "codility1";
            R[0] = "Wrong answer";
            T[1] = "codility3";
            R[1] = "OK";
            T[2] = "codility2";
            R[2] = "OK";
            T[3] = "codility4b";
            R[3] = "Runtime error";
            T[4] = "codility4a";
            R[4] = "OK";


            var output = solution(T, R);
        }

        // T = Testname / R = Test result
        private static int solution(string[] T, string[] R)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            var unsuccessfulResults = new List<string>() { "Wrong answer", "Runtime error", "Time limit exceeded" };

            var tests = new List<TestCase>();
            foreach (var testResult in R.Select((value, index) => new { index, value }))
            {
                var index = testResult.index;

                var testName = T[index];
                if (testName == null) continue;

                testName = Regex.Match(testName, @"\d+").Value;
                var group = Convert.ToInt32(testName);
                if (group == 0) continue;
                var test = new TestCase { Group = group, Result = testResult.value };
                tests.Add(test);
            }

            tests = tests.OrderBy(x => x.Group).ToList();
            var unsuccessfulTests = tests.Where(x => unsuccessfulResults.Contains(x.Result)).Select(x => x.Group).Distinct().ToList();
            var successfulTests = tests.Where(x => !unsuccessfulTests.Contains(x.Group) && x.Result == "OK").ToList();

            var numberOfGroups = tests.Select(x => x.Group).Distinct().ToList().Count;
            var numberOfSuccessfulTests = successfulTests.Distinct().ToList().Count;
            var score = numberOfSuccessfulTests * 100 / numberOfGroups;

            return score;
        }

        class TestCase
        {
            public int Group { get; set; }
            public string Result { get; set; }
        }
    }
}