using System.Linq;

namespace Com.Coderbyte.Solutions.Solutions.Codility
{
    public class Task2
    {
        public static void Test()
        {
            var A = new int[50];
            A[0] = 7;
            A[1] = 3;
            A[2] = 7;
            A[3] = 3;
            A[4] = 1;
            A[5] = 3;
            A[6] = 4;
            A[7] = 1;

            var output = solution(A);
        }

        public static int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            A = A.Where(x => x != 0).ToArray();
            var locations = A.ToList();
            var shortestTrip = A.Length;

            for (int i = 0; i < locations.Count; i++)
            {
                var availableLocations = locations.Distinct().ToList();
                var locationsToCompare = locations.Skip(i).ToList();
                var days = 0;
                foreach (var l in locationsToCompare)
                {
                    if (availableLocations.Count > 0)
                    {
                        availableLocations.Remove(l);
                        days++;
                    }
                }

                if (availableLocations.Count == 0 && days < shortestTrip) shortestTrip = days;
            }

            return shortestTrip;
        }

    }
}