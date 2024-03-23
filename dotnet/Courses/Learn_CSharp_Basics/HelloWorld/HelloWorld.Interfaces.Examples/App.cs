using HelloWorld.Interfaces.Examples.Models;
using HelloWorld.Interfaces.Examples.Services;

namespace HelloWorld.Interfaces.Examples
{
	public class App
	{
		public static void Run()
		{
			var orderProcessor = new OrderProcessor(new ShippingCalculator());
			var order = new Order{ DatePlaced = DateTime.Now, TotalPrice = 100f };
			orderProcessor.Process(order);
		}
	}
}