using System.Collections;
using Amazon.Models;

namespace HelloWorld
{
	// here: C:\Users\vplav\Bill\Downloads\Tor\Files\Courses\[FreeCourseLab.com] Udemy - C# Intermediate Classes, Interfaces and OOP\6\1


	class Program
	{
		static void Main(string[] args)
		{

			var shapes = new List<Shape>
			{
				new Circle(),
				new Rectangle()
			};

			var canvas = new Canvas();
			Canvas.DrawShapes(shapes);




			 var list = new ArrayList
			{
				1,
				2,
				3,
				4,
				"ddd"
			};
			var ints = list.ToArray().OfType<int>().Where(x => x < 2).ToList();

			var customer = new Customer();
		}
	}

}

