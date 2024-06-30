namespace Amazon.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Promote()
    {
	    var calc = new RateCalculator();
	    var rating = calc.Calculate(this);
	    Console.WriteLine($"Prom log changed. {rating}");
    }
}