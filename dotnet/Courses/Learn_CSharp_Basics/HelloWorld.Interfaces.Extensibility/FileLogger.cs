namespace HelloWorld.Interfaces.Extensibility;

public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path;
    }

    public void LogError(string message)
    {
        Log(message, "Error");
    }

    public void LogInfo(string message)
    {

        Log(message, "Info");
    }

    private void Log(string message, string messageType)
    {
        using var streamWriter = new StreamWriter(_path, true);
        streamWriter.WriteLine($"{messageType}: {message}");
    }
}