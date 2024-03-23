namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			Amazon.App.Run();
			var car = new Car();
			car = new Car("2");
			Interfaces.Examples.App.Run();
		}
	}

}

