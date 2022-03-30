using System;
using System.Collections.Generic;
using System.Linq;
using Com.Coderbyte.Solutions.Helpers;

namespace Com.Coderbyte.Solutions.Solutions
{
    public class FindWordsFromDictionaryMain
    {
        // UNFINISHED

        // Array Challenge
        // Have the function ArrayChallenge(strArr) read the array of strings stored in strArr, which will contain 2 elements: the first element will be a sequence of characters, and the second element will be a long string of comma-separated words, in alphabetical order, that represents a dictionary of some arbitrary length. For example: strArr can be: ["hellocat", "apple,bat,cat,goodbye,hello,yellow,why"]. Your goal is to determine if the first element in the input can be split into two words, where both words exist in the dictionary that is provided in the second input. In this example, the first element can be split into two words: hello and cat because both of those words are in the dictionary.
        // 
        // Your program should return the two words that exist in the dictionary separated by a comma. So for the example above, your program should return hello,cat. There will only be one correct way to split the first element of characters into two words. If there is no way to split string into two words that exist in the dictionary, return the string not possible. The first element itself will never exist in the dictionary as a real word.
        // Once your function is working, take the final output string and combine it with your ChallengeToken, both in reverse order and separated by a colon.
        // 
        // Your ChallengeToken: token
        // Examples
        // Input: new string[] {"baseball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z"}
        // Output: base,ball
        // Final Output: llab,esab:nekot
        // Input: new string[] {"abcgefd", "a,ab,abc,abcg,b,c,dog,e,efd,zzzz"}
        // Output: abcg,efd
        // Final Output: dfe,gcba:nekot

        public static void Test()
        {
            TestHelper.Console.WriteLine.Start("Min Window Substring");

            Run(new string[] { "hellocat", "apple,bat,cat,goodbye,hello,yellow,why" },
                "tac,olleh:nekot");

            Run(new string[] { "baseball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z" },
                "esab,llab:nekot");

            Run(new string[] { "abcgefd", "a,ab,abc,abcg,b,c,dog,e,efd,zzzz" },
                "dfe,gcba:nekot");
        }

        private static void Run(string[] input, string finalOutputExpected)
        {
            Console.WriteLine($"INPUT: {string.Join(", ", input)}");
            Console.WriteLine($"OUTPUT EXPECTED: {finalOutputExpected}");

            var finalOutput = FindWordsFromDictionary(input);
            Console.WriteLine($"OUTPUT: {finalOutput}");
            Console.WriteLine(finalOutput == finalOutputExpected ? "CORRECT" : "NOT CORRECT");
            Console.WriteLine("");
        }

        private static string FindWordsFromDictionary(string[] strArr)
        {
            if (strArr[0] == strArr[1])
            {
                return $"The first element itself will never exist in the dictionary as a real word:{StringHelper.Reverse("zrcwli459")}";
            }

            var input = strArr[0];
            var dictionary = strArr[1].Split(",").ToList();
            dictionary = dictionary.OrderByDescending(x => x.Length).ToList();
            var words = GetWords(input, dictionary);
            var output = words == null ? "not possible" : string.Join(",", words);

            var finalOutput = $"{StringHelper.Reverse(output)}:{StringHelper.Reverse("token")}";
            return finalOutput;
        }

        private static List<string> GetWords(string input, List<string> dictionary)
        {
            var inputTemp = input;
            var wordsFound = new List<string>();
            foreach (var w in dictionary)
            {
                if (inputTemp.Length == 0)
                {
                    return wordsFound;
                }

                if (inputTemp.Contains(w))
                {
                    wordsFound.Add(w);
                    inputTemp = inputTemp.Replace(w, "");
                }
            }

            return null;
        }
    }
}