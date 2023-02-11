using System;

namespace Com.Coderbyte.Solutions.Solutions.Codility;

public class BinaryGap
{
	public static void Test()
	{
		var input = 1041;
		var outputExpected = 5;
		Run(input, outputExpected);

		input = 15;
		outputExpected = 0;
		Run(input, outputExpected);

		input = 32;
		outputExpected = 0;
		Run(input, outputExpected);
	}

	private static void Run(int input, int outputExpected)
	{
		Console.WriteLine($"INPUT: {string.Join(", ", input)}");
		Console.WriteLine($"OUTPUT EXPECTED: {outputExpected}");

		var output = Solution(input);
		Console.WriteLine($"OUTPUT: {output}");

		Console.WriteLine(output == outputExpected ? "CORRECT" : "NOT CORRECT");
		System.Console.WriteLine("");
	}

	private static int Solution(int N)
	{
		var binary = Convert.ToString(N, 2);

		while (binary.EndsWith("0")) binary = binary.Remove(binary.Length - 1, 1);
		while (binary.StartsWith("0")) binary = binary.Remove(1);

		var longest = 0;
		var curCount = 0;
		foreach (var c in binary)
		{
			if (c == '0')
			{
				if (curCount > 0) curCount++;
				else curCount = 1;
			}
			else curCount = 0;
			if (curCount > longest) longest = curCount;
		}

		return longest;
	}
}