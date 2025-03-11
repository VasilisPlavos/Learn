namespace Examples.y24.DataStructure.Helpers;

internal static class DictionariesHelper
{
    public static void Examples()
    {
        // https://www.youtube.com/watch?v=R94JHIXdTk0

        Dictionary<int, string> rookieOfTheYear = new()
        {
            { 2020, "Ja Morant" },
            { 2019, "Luka Doncic" },
            { 2018, "Ben Simmons" },
            // rookieOfTheYear.Add(2018, "Ben Simmons"); // Duplicate key exception
            { 2017, "Ben Simmons" }
        };

        Console.WriteLine(rookieOfTheYear[2020]);
        if (rookieOfTheYear.TryGetValue(2020, out var value))
        {
            Console.WriteLine(value);
        }

        Dictionary<string, List<string>> wishList = new()
        {
            { "John Doe", new() { "Xbox", "Playstation", "Nintendo" }},
            { "Jane Doe", new() { "Nintendo", "Playstation", "Xbox" }}
        };

        foreach (var (key, valueList) in wishList)
        {
            Console.WriteLine($"{key} wants {string.Join(", ", valueList)}");
        }
    }
}