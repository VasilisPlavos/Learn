using System;
using System.Text;



Console.WriteLine("Hello, World!");

// here: C:\Users\vplav\Bill\Downloads\Tor\Files\Courses\[FreeCourseLab.com] Udemy - C# Intermediate Classes, Interfaces and OOP\2. Classes\8. Indexers.mp4



var builder = new StringBuilder();
builder.Append('-', 10);
Console.WriteLine(builder);

var person = new Person("Jack");

person.PhoneList.Add("1");
person.PhoneList.Add("2");

foreach (var i in person.PhoneList)
{
	Console.WriteLine(i);
}

Console.WriteLine(person.Name);

public class Person
{
	public readonly string? Name;
	public readonly List<string> PhoneList = new(){"7"};

	public Person(string? name)
	{
		Name = name;
	}
}
