public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    private Node root;

    private int Height(Node node) => node?.Height ?? 0;

    private int BalanceFactor(Node node) => Height(node.Left) - Height(node.Right);

    private void UpdateHeight(Node node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private Node Balance(Node node)
    {
        UpdateHeight(node);

        if (BalanceFactor(node) > 1)
        {
            if (BalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (BalanceFactor(node) < -1)
        {
            if (BalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    public void Add(TKey key, TValue value)
    {
        root = Add(root, key, value);
    }

    private Node Add(Node node, TKey key, TValue value)
    {
        if (node == null)
            return new Node(key, value);

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
            node.Left = Add(node.Left, key, value);
        else if (compare > 0)
            node.Right = Add(node.Right, key, value);
        else
            node.Value = value; 

        return Balance(node);
    }

    public TValue Search(TKey key)
    {
        Node node = Search(root, key);
        if (node == null)
            throw new KeyNotFoundException("Key not found.");
        return node.Value;
    }

    private Node Search(Node node, TKey key)
    {
        if (node == null || key.CompareTo(node.Key) == 0)
            return node;

        if (key.CompareTo(node.Key) < 0)
            return Search(node.Left, key);
        else
            return Search(node.Right, key);
    }

    public void Remove(TKey key)
    {
        root = Remove(root, key);
    }

    private Node Remove(Node node, TKey key)
    {
        if (node == null)
            return null;

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
            node.Left = Remove(node.Left, key);
        else if (compare > 0)
            node.Right = Remove(node.Right, key);
        else
        {
            if (node.Left == null || node.Right == null)
            {
                node = node.Left ?? node.Right;
            }
            else
            {
                Node minNode = GetMinNode(node.Right);
                node.Key = minNode.Key;
                node.Value = minNode.Value;
                node.Right = Remove(node.Right, minNode.Key);
            }
        }

        return node == null ? null : Balance(node);
    }

    private Node GetMinNode(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    private Node GetMaxNode(Node node)
    {
        while (node.Right != null)
            node = node.Right;
        return node;
    }

    public TKey FindMin()
    {
        if (root == null)
            throw new InvalidOperationException("Tree is empty.");
        return GetMinNode(root).Key;
    }

    public TKey FindMax()
    {
        if (root == null)
            throw new InvalidOperationException("Tree is empty.");
        return GetMaxNode(root).Key;
    }

    // Traversals
    public void TraverseInDepth(Action<TKey, TValue> action)
    {
        TraverseInDepth(root, action);
    }

    private void TraverseInDepth(Node node, Action<TKey, TValue> action)
    {
        if (node == null) return;

        TraverseInDepth(node.Left, action);
        action(node.Key, node.Value);
        TraverseInDepth(node.Right, action);
    }

    public void TraverseInBreadth(Action<TKey, TValue> action)
    {
        if (root == null) return;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();
            action(node.Key, node.Value);

            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
    }
}


class Program
{
    static void Main()
    {
        var tree = new AVLTree<int, string>();

        tree.Add(10, "Ten");
        tree.Add(20, "Twenty");
        tree.Add(5, "Five");
        tree.Add(6, "Six");
        tree.Add(3, "Three");

        Console.WriteLine("In-depth traversal:");
        tree.TraverseInDepth((key, value) => Console.WriteLine($"Key: {key}, Value: {value}"));

        Console.WriteLine("\nIn-breadth traversal:");
        tree.TraverseInBreadth((key, value) => Console.WriteLine($"Key: {key}, Value: {value}"));

        Console.WriteLine($"\nMinimum key: {tree.FindMin()}");
        Console.WriteLine($"Maximum key: {tree.FindMax()}");

        Console.WriteLine($"\nSearch key 10: {tree.Search(10)}");

        tree.Remove(10);
        Console.WriteLine("\nAfter removing key 10:");
        tree.TraverseInDepth((key, value) => Console.WriteLine($"Key: {key}, Value: {value}"));
    }
}
