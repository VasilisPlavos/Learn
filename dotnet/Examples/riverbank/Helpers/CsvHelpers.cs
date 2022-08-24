namespace riverbank.Helpers;

public static class CsvHelpers
{
    public static List<string> GetCellsContainingComma(string line)
    {
        var cellsOfLine = new List<string>();
        var tempLineSpit = line.Replace("\",", ";").Split(";");
        foreach (var s in tempLineSpit)
        {
            if (s.Contains('"'))
            {
                var tempString = s[1..];
                cellsOfLine.Add(tempString);
            }
            else
            {
                var tempList = s.Split(",");
                cellsOfLine = cellsOfLine.Union(tempList).ToList();
            }
        }
        return cellsOfLine;
    }
}