namespace Amazon.Models;

public class Canvas
{
	public static void DrawShapes(List<Shape> shapes)
	{
		foreach (var shape in shapes)
		{
			shape.Draw();
		}
	}
}