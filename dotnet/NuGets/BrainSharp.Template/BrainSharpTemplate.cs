namespace BrainSharp.Template;

public class BrainSharpTemplate
{
    public string Hello(string v = "world")
    {
        var phrase = $"Hello {v}";
        Console.WriteLine(phrase);
        return phrase;
    }
}