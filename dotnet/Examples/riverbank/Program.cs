// See https://aka.ms/new-console-template for more information

using riverbank.Dtos;
using riverbank.Helpers;

var lines = await File.ReadAllLinesAsync("C:\\Users\\vplav\\Gits\\Learn\\dotnet\\Examples\\riverbank\\Files\\book.csv");
var headers = lines[0].Split(',');
lines = lines[1..];

var employees = new List<Employee>();
foreach (var line in lines)
{
    var cellsOfLine = line.Contains('"') ? CsvHelpers.GetCellsContainingComma(line) : line.Split(',').ToList();
    employees.Add(new Employee
    {
        Name = cellsOfLine[0],
        Department = cellsOfLine[1],
        Country = cellsOfLine[2]
    });
}

foreach (var employee in employees)
{
    Console.WriteLine($"{headers[0]}: {employee.Name}; {headers[1]}: {employee.Department}; {headers[2]}: {employee.Country}; ");
}