namespace PriorityQueueAndHeapSort
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> heap;

        public PriorityQueue()
        {
            heap = new List<T>();
        }

        public void Enqueue(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Черга порожня!");

            T root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return root;
        }

        public bool IsEmpty()
        {
            return heap.Count == 0;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].CompareTo(heap[parentIndex]) > 0)
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChild, rightChild, largest;

            while (true)
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;
                largest = index;

                if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[largest]) > 0)
                {
                    largest = leftChild;
                }

                if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[largest]) > 0)
                {
                    largest = rightChild;
                }

                if (largest == index)
                {
                    break;
                }

                Swap(index, largest);
                index = largest;
            }
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }

    public class Program
    {
        public static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            // Будуємо купу
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                Heapify(array, array.Length, i);
            }

            // Виймаємо елементи з купи
            for (int i = array.Length - 1; i > 0; i--)
            {
                Swap(array, 0, i);
                Heapify(array, i, 0);
            }
        }

        private static void Heapify<T>(T[] array, int heapSize, int rootIndex) where T : IComparable<T>
        {
            int largest = rootIndex;
            int leftChild = 2 * rootIndex + 1;
            int rightChild = 2 * rootIndex + 2;

            if (leftChild < heapSize && array[leftChild].CompareTo(array[largest]) > 0)
            {
                largest = leftChild;
            }

            if (rightChild < heapSize && array[rightChild].CompareTo(array[largest]) > 0)
            {
                largest = rightChild;
            }

            if (largest != rootIndex)
            {
                Swap(array, rootIndex, largest);
                Heapify(array, heapSize, largest);
            }
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static void Main(string[] args)
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>();
            priorityQueue.Enqueue(10);
            priorityQueue.Enqueue(20);
            priorityQueue.Enqueue(5);
            Console.WriteLine("Черга з прiоритетом:");
            while (!priorityQueue.IsEmpty())
            {
                Console.WriteLine(priorityQueue.Dequeue());
            }

            int[] array = { 4, 10, 3, 5, 1 };
            Console.WriteLine("\nМасив перед сортуванням:");
            Console.WriteLine(string.Join(" ", array));

            HeapSort(array);

            Console.WriteLine("Масив пiсля пiрамiдального сортування:");
            Console.WriteLine(string.Join(" ", array));
        }
    }
}
