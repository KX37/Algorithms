
namespace BinarySearchTreeExample
{
    public class TreeNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public TreeNode<TKey, TValue> Left { get; set; }
        public TreeNode<TKey, TValue> Right { get; set; }

        public TreeNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class AssociativeArray<TKey, TValue> where TKey : IComparable<TKey>
    {
        private TreeNode<TKey, TValue> root;

        public void Add(TKey key, TValue value)
        {
            root = AddRecursive(root, key, value);
        }

        private TreeNode<TKey, TValue> AddRecursive(TreeNode<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null)
                return new TreeNode<TKey, TValue>(key, value);

            if (key.CompareTo(node.Key) < 0)
                node.Left = AddRecursive(node.Left, key, value);
            else if (key.CompareTo(node.Key) > 0)
                node.Right = AddRecursive(node.Right, key, value);
            else
                node.Value = value;

            return node;
        }

        public TValue Find(TKey key)
        {
            var node = FindRecursive(root, key);
            if (node == null)
                throw new KeyNotFoundException($"Ключ {key} не знайдено.");
            return node.Value;
        }

        private TreeNode<TKey, TValue> FindRecursive(TreeNode<TKey, TValue> node, TKey key)
        {
            if (node == null || key.CompareTo(node.Key) == 0)
                return node;

            if (key.CompareTo(node.Key) < 0)
                return FindRecursive(node.Left, key);

            return FindRecursive(node.Right, key);
        }

        public void Remove(TKey key)
        {
            root = RemoveRecursive(root, key);
        }

        private TreeNode<TKey, TValue> RemoveRecursive(TreeNode<TKey, TValue> node, TKey key)
        {
            if (node == null)
                return null;

            if (key.CompareTo(node.Key) < 0)
                node.Left = RemoveRecursive(node.Left, key);
            else if (key.CompareTo(node.Key) > 0)
                node.Right = RemoveRecursive(node.Right, key);
            else
            {
                if (node.Left == null)
                    return node.Right;

                if (node.Right == null)
                    return node.Left;

                var minLargerNode = FindMin(node.Right);
                node.Key = minLargerNode.Key;
                node.Value = minLargerNode.Value;
                node.Right = RemoveRecursive(node.Right, minLargerNode.Key);
            }

            return node;
        }

        public TKey FindMinKey()
        {
            if (root == null)
                throw new InvalidOperationException("Дерево порожнє.");
            return FindMin(root).Key;
        }

        private TreeNode<TKey, TValue> FindMin(TreeNode<TKey, TValue> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public TKey FindMaxKey()
        {
            if (root == null)
                throw new InvalidOperationException("Дерево порожнє.");
            return FindMax(root).Key;
        }

        private TreeNode<TKey, TValue> FindMax(TreeNode<TKey, TValue> node)
        {
            while (node.Right != null)
                node = node.Right;
            return node;
        }

        public void TraverseDepthFirst(Action<TKey, TValue> action)
        {
            TraverseDepthFirstRecursive(root, action);
        }

        private void TraverseDepthFirstRecursive(TreeNode<TKey, TValue> node, Action<TKey, TValue> action)
        {
            if (node == null)
                return;

            TraverseDepthFirstRecursive(node.Left, action);
            action(node.Key, node.Value);
            TraverseDepthFirstRecursive(node.Right, action);
        }

        public void TraverseBreadthFirst(Action<TKey, TValue> action)
        {
            if (root == null)
                return;

            var queue = new Queue<TreeNode<TKey, TValue>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                action(node.Key, node.Value);

                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var associativeArray = new AssociativeArray<int, string>();

            associativeArray.Add(5, "П'ять");
            associativeArray.Add(3, "Три");
            associativeArray.Add(7, "Сiм");
            associativeArray.Add(2, "Два");
            associativeArray.Add(4, "Чотири");

            Console.WriteLine("Обхiд в глибину:");
            associativeArray.TraverseDepthFirst((key, value) => Console.WriteLine($"{key}: {value}"));

            Console.WriteLine("\nОбхiд в ширину:");
            associativeArray.TraverseBreadthFirst((key, value) => Console.WriteLine($"{key}: {value}"));

            Console.WriteLine($"\nПошук за ключем 3: {associativeArray.Find(3)}");

            Console.WriteLine($"\nМiнiмальний ключ: {associativeArray.FindMinKey()}");
            Console.WriteLine($"Максимальний ключ: {associativeArray.FindMaxKey()}");

            associativeArray.Remove(3);
            Console.WriteLine("\nПiсля видалення ключа 3:");
            associativeArray.TraverseDepthFirst((key, value) => Console.WriteLine($"{key}: {value}"));
        }
    }
}
