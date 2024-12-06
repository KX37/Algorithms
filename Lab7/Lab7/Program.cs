namespace Lab7
{
    public class HashTable<TKey, TValue> where TKey : notnull
    {
        private class Bucket
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Bucket Next { get; set; }

            public Bucket(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private Bucket[] buckets;
        private int count;
        private const double LoadFactorThreshold = 0.75;

        public HashTable(int initialCapacity = 16)
        {
            buckets = new Bucket[initialCapacity];
        }

        private int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % buckets.Length;
        }

        public void Add(TKey key, TValue value)
        {
            if (count >= buckets.Length * LoadFactorThreshold)
            {
                Resize();
            }

            int index = GetBucketIndex(key);
            Bucket current = buckets[index];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    current.Value = value;
                    return;
                }
                current = current.Next;
            }

            Bucket newBucket = new Bucket(key, value)
            {
                Next = buckets[index]
            };
            buckets[index] = newBucket;
            count++;
        }

        public TValue Search(TKey key)
        {
            int index = GetBucketIndex(key);
            Bucket current = buckets[index];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }

            throw new KeyNotFoundException("Key not found.");
        }

        public void Remove(TKey key)
        {
            int index = GetBucketIndex(key);
            Bucket current = buckets[index];
            Bucket previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previous == null)
                    {
                        buckets[index] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    count--;
                    return;
                }

                previous = current;
                current = current.Next;
            }

            throw new KeyNotFoundException("Key not found.");
        }

        private void Resize()
        {
            int newCapacity = buckets.Length * 2;
            Bucket[] newBuckets = new Bucket[newCapacity];

            foreach (Bucket bucket in buckets)
            {
                Bucket current = bucket;
                while (current != null)
                {
                    int newIndex = Math.Abs(current.Key.GetHashCode()) % newCapacity;

                    Bucket newBucket = new Bucket(current.Key, current.Value)
                    {
                        Next = newBuckets[newIndex]
                    };
                    newBuckets[newIndex] = newBucket;

                    current = current.Next;
                }
            }

            buckets = newBuckets;
        }

        public void Traverse(Action<TKey, TValue> action)
        {
            foreach (Bucket bucket in buckets)
            {
                Bucket current = bucket;
                while (current != null)
                {
                    action(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
    }
        internal class Program
    {
        static void Main(string[] args)
        {
            var hashTable = new HashTable<int, string>();

            hashTable.Add(1, "One");
            hashTable.Add(2, "Two");
            hashTable.Add(3, "Three");
            hashTable.Add(17, "Seventeen");

            Console.WriteLine("Traverse all items:");
            hashTable.Traverse((key, value) => Console.WriteLine($"Key: {key}, Value: {value}"));

            Console.WriteLine($"\nSearch key 2: {hashTable.Search(2)}");

            hashTable.Remove(2);
            Console.WriteLine("\nAfter removing key 2:");
            hashTable.Traverse((key, value) => Console.WriteLine($"Key: {key}, Value: {value}"));
        }
    }
}
