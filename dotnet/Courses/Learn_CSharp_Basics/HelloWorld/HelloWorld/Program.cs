using System.Collections;
using Amazon.Models;

namespace HelloWorld
{
	// here: C:\Users\vplav\Bill\Downloads\Tor\Files\Courses\[FreeCourseLab.com] Udemy - C# Intermediate Classes, Interfaces and OOP\6\1


	class Program
	{
		static void Main(string[] args)
		{
			Amazon.App.Run();
			var car = new Car();
			car = new Car("2");
		}
	}

}

