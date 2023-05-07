namespace Amazon.Models;

public class Circle : Shape
{
	public override void Draw() => Console.WriteLine("Circle draw");
}
public class Rectangle : Shape
{
	public override void Draw() => Console.WriteLine("Rectange draw");
}

public abstract class Shape
{
	public int Width { get; set; }
	public int Height { get; set; }
	public Position Position { get; set; }

	public abstract void Draw();

}
public class Position	
{
}