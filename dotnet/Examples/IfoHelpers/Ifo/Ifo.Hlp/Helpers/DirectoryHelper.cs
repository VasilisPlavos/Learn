namespace Ifo.Hlp.Helpers;

public class DirectoryHelper
{
    public static string GetProjectDirectory()
    {
        var dir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        if (dir == null) throw new Exception();
        return dir;
    }
}