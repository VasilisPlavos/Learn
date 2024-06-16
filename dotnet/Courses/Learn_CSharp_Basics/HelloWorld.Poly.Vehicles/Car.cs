namespace HelloWorld;

public class Car : Vehicle
{
	public Car()
	{
		Console.WriteLine("Car 1");
	}

	public Car(string registrationNumber) : base(registrationNumber)
	{
		Console.WriteLine("Car 2");
	}
}