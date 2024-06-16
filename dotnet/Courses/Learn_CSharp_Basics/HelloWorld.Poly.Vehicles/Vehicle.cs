namespace HelloWorld;

public class Vehicle
{
	private string _registrationNumber;

	protected Vehicle()
	{
		Console.WriteLine("Vehicle 1");
	}
	public Vehicle(string registrationNumber)
	{
		_registrationNumber = registrationNumber;
	}

}