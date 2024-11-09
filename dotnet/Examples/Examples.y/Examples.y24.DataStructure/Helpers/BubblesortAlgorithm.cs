using System.Text.Json;

namespace Examples.y24.DataStructure.Helpers;

public class BubbleSortAlgorithm
{
    public static void Examples()
    {
        //var arr = new[] { 2, 1, 4, 3 };
        //var bubbleSort = new BubbleSort<int>();

        var arr = new[] { "Bob", "Henry", "Andy", "Greg" };
        var bubbleSort = new BubbleSort<string>();
        bubbleSort.Sort(arr);
        Console.WriteLine(string.Join(", ", arr));

        Employee[] employees = 
        [ 
            new() { Id = 4, Name = "John" },
            new() { Id = 2, Name = "Bob" },
            new() { Id = 3, Name = "Greg" },
            new() { Id = 1, Name = "Tom" }
        ];

        var empBubbleSort = new BubbleSort<Employee>();
        empBubbleSort.Sort(employees);

        foreach (var x in employees)
        {
            Console.WriteLine(JsonSerializer.Serialize(x));
        }
    }

    private class BubbleSort<T> where T : IComparable<T>
    {
        public void Sort(T[] arr)
        {
            var n = arr.Length;

            for (var i = 0; i < n - 1; i++)
            {
                for (var j = 0; j < n - i - 1; j++)
                {
                    if (arr[j].CompareTo(arr[j+1]) > 0)
                    {
                        Swap(arr, j);
                    }
                }
            }
        }

        private static void Swap(T[] arr, int j)
        {
            (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
        }
    }



}

public class Employee : IComparable<Employee>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CompareTo(Employee? other)
    {
        //return Id.CompareTo(other.Id);
        return Name.CompareTo(other.Name);
    }
}