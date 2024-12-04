using System;
using System.Linq;

class Program
{
    // Linear Search
    public static int LinearSearch(int[] data, int target, out int comparisons)
    {
        comparisons = 0;
        for (int i = 0; i < data.Length; i++)
        {
            comparisons++;
            if (data[i] == target)
                return i;
        }
        return -1;
    }

    // Binary Search
    public static int BinarySearch(int[] data, int target, out int comparisons)
    {
        comparisons = 0;
        int left = 0, right = data.Length - 1;
        while (left <= right)
        {
            comparisons++;
            int mid = left + (right - left) / 2;
            if (data[mid] == target)
                return mid;
            else if (data[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }

    // Interpolation Search
    public static int InterpolationSearch(int[] data, int target, out int comparisons)
    {
        comparisons = 1;
        int left = 0, right = data.Length - 1;
        while (left <= right && target >= data[left] && target <= data[right])
        {
            comparisons++;
            if (left == right)
            {
                if (data[left] == target) return left;
                return -1;
            }

            int pos = left + (target - data[left]) * (right - left) / (data[right] - data[left]);

            if (data[pos] == target)
                return pos;
            if (data[pos] < target)
                left = pos + 1;
            else
                right = pos - 1;
        }
        return -1;
    }

    // Generate Random Array
    public static int[] GenerateRandomArray(int size, int maxValue)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
            arr[i] = rand.Next(1, maxValue + 1); // Генерация чисел от 1 до maxValue
        return arr;
    }

    // Test Search Algorithms
    public static void TestSearchAlgorithms()
    {
        int[] sizes = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        Random rand = new Random();

        foreach (int size in sizes)
        {
            int[] data = GenerateRandomArray(size, size);
            int[] sortedData = (int[])data.Clone();
            Array.Sort(sortedData);

            // Генерация случайного индекса и целевого значения
            int randomIndex = rand.Next(size);
            int target = data[randomIndex];

            Console.WriteLine($"Array Size: {size}, Target Value: {target}");

            // Linear Search
            LinearSearch(data, target, out int linearComparisons);
            Console.WriteLine($"Linear Search Comparisons: {linearComparisons}");

            // Binary Search
            BinarySearch(sortedData, target, out int binaryComparisons);
            Console.WriteLine($"Binary Search Comparisons: {binaryComparisons}");

            // Interpolation Search
            InterpolationSearch(sortedData, target, out int interpolationComparisons);
            Console.WriteLine($"Interpolation Search Comparisons: {interpolationComparisons}");
            Console.WriteLine("--------------------------------------");
        }
    }

    static void Main()
    {
        TestSearchAlgorithms();
    }
}
