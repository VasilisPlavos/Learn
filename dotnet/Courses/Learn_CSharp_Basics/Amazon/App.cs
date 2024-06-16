using System.Collections;
using Amazon.Models;

namespace Amazon;

public class App
{
	public static void Run()
	{
		var shapes = new List<Shape>
		{
			new Circle(),
			new Rectangle()
		};

		var canvas = new Canvas();
		Canvas.DrawShapes(shapes);

		UncategorizedCode();

		var customer = new Customer();
	}

	private static void UncategorizedCode()
	{
		var list = new ArrayList { 1, 2, 3, 4, "ddd" };
		var ints = list.ToArray().OfType<int>().Where(x => x < 2).ToList();
	}
}