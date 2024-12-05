using System;

class SortingAlgorithms
{
    static int BubbleSort(int[] arr)
    {
        int comparisons = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                comparisons++;
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return comparisons;
    }

    static int SelectionSort(int[] arr)
    {
        int comparisons = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                comparisons++;
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            int temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
        }
        return comparisons;
    }

    static int InsertionSort(int[] arr)
    {
        int comparisons = 0;
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int current = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > current)
            {
                comparisons++;
                arr[j + 1] = arr[j];
                j--;
            }
            comparisons++;
            arr[j + 1] = current;
        }
        return comparisons;
    }

    static int QuickSort(int[] arr)
    {
        int comparisons = 0;

        int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                comparisons++;
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return i + 1;
        }

        void QuickSortRecursive(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSortRecursive(arr, low, pivotIndex - 1);
                QuickSortRecursive(arr, pivotIndex + 1, high);
            }
        }

        QuickSortRecursive(arr, 0, arr.Length - 1);
        return comparisons;
    }

    static int MergeSort(int[] arr)
    {
        int comparisons = 0;

        int[] Merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                comparisons++;
                if (left[i] < right[j])
                {
                    result[k++] = left[i++];
                }
                else
                {
                    result[k++] = right[j++];
                }
            }
            while (i < left.Length) result[k++] = left[i++];
            while (j < right.Length) result[k++] = right[j++];
            return result;
        }

        int[] MergeSortRecursive(int[] arr)
        {
            if (arr.Length <= 1) return arr;
            int mid = arr.Length / 2;
            int[] left = MergeSortRecursive(arr[..mid]);
            int[] right = MergeSortRecursive(arr[mid..]);
            return Merge(left, right);
        }

        MergeSortRecursive(arr);
        return comparisons;
    }

    static int[] GenerateRandomArray(int size)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(1000);
        }
        return arr;
    }

    static int[] GenerateNearlySortedArray(int size)
    {
        int[] arr = new int[size];
        for (int i = 0; i < size/2; i++)
        {
            arr[i] = i;
        }
        Random rand = new Random();
        for (int i = size / 2; i < size; i++)
        {
            arr[i] = rand.Next(1000);
        }
        return arr;
    }

    static int[] GenerateReverseSortedArray(int size)
    {
        int[] arr = new int[size];
        for (int i = size; i > 0; i--)
        {
            arr[size - i] = i;
        }
        return arr;
    }

    static void Main()
    {
        int[] sizes = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

        foreach (int size in sizes)
        {
            // Random array
            int[] randomArr = GenerateRandomArray(size);
            Console.WriteLine($"Size: {size} (Random)");
            Console.WriteLine("");
            Console.WriteLine("Bubble Sort Comparisons: " + BubbleSort((int[])randomArr.Clone()));
            Console.WriteLine("Selection Sort Comparisons: " + SelectionSort((int[])randomArr.Clone()));
            Console.WriteLine("Insertion Sort Comparisons: " + InsertionSort((int[])randomArr.Clone()));
            Console.WriteLine("Quick Sort Comparisons: " + QuickSort((int[])randomArr.Clone()));
            Console.WriteLine("Merge Sort Comparisons: " + MergeSort((int[])randomArr.Clone()));
            Console.WriteLine("");

            // Nearly sorted array
            int[] nearlySortedArr = GenerateNearlySortedArray(size);
            Console.WriteLine($"Size: {size} (Nearly Sorted)");
            Console.WriteLine("");
            Console.WriteLine("Bubble Sort Comparisons: " + BubbleSort((int[])nearlySortedArr.Clone()));
            Console.WriteLine("Selection Sort Comparisons: " + SelectionSort((int[])nearlySortedArr.Clone()));
            Console.WriteLine("Insertion Sort Comparisons: " + InsertionSort((int[])nearlySortedArr.Clone()));
            Console.WriteLine("Quick Sort Comparisons: " + QuickSort((int[])nearlySortedArr.Clone()));
            Console.WriteLine("Merge Sort Comparisons: " + MergeSort((int[])nearlySortedArr.Clone()));
            Console.WriteLine("");

            // Reverse sorted array
            int[] reverseSortedArr = GenerateReverseSortedArray(size);
            Console.WriteLine($"Size: {size} (Reverse Sorted)");
            Console.WriteLine("");
            Console.WriteLine("Bubble Sort Comparisons: " + BubbleSort((int[])reverseSortedArr.Clone()));
            Console.WriteLine("Selection Sort Comparisons: " + SelectionSort((int[])reverseSortedArr.Clone()));
            Console.WriteLine("Insertion Sort Comparisons: " + InsertionSort((int[])reverseSortedArr.Clone()));
            Console.WriteLine("Quick Sort Comparisons: " + QuickSort((int[])reverseSortedArr.Clone()));
            Console.WriteLine("Merge Sort Comparisons: " + MergeSort((int[])reverseSortedArr.Clone()));
            Console.WriteLine("");
        }
    }
}
