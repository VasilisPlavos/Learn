namespace Y25.ManyProcessors.Dtos.Flatfox;

public class GetPinsRequestDto
{
    public double East { get; set; }
    public double North { get; set; }
    public double South { get; set; }
    public double West { get; set; }
    public int MaxCount { get; set; }
}