public class Stack<T>
{
    private T[] items; 
    private int count; 

    public Stack()
    {
        items = new T[1]; 
        count = 0;
    }

    public void Push(T item)
    {
        if (count == items.Length)
            Resize(items.Length + 1);
        items[count++] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack underflow");

        T item = items[--count];
        items[count] = default;
        Resize(items.Length - 1);
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty");
        return items[count - 1];
    }

    public bool IsEmpty() => count == 0;

    private void Resize(int newSize)
    {
        T[] newArray = new T[newSize];
        for (int i = 0; i < count; i++)
            newArray[i] = items[i];
        items = newArray;
    }
}
public class Queue<T>
{
    private class Node
    {
        public T Data;
        public Node Next;

        public Node(T data) => Data = data;
    }

    private Node head;
    private Node tail;

    public void Enqueue(T item)
    {
        var node = new Node(item);
        if (tail != null) tail.Next = node;
        tail = node;
        if (head == null) head = tail;
    }

    public T Dequeue()
    {
        if (head == null)
            throw new InvalidOperationException("Queue is empty");
        var data = head.Data;
        head = head.Next;
        if (head == null) tail = null;
        return data;
    }

    public bool IsEmpty() => head == null;
}
public class Deque<T>
{
    private class Node
    {
        public T Data;
        public Node Next;
        public Node Prev;

        public Node(T data) => Data = data;
    }

    private Node head;
    private Node tail;

    public void AddToFront(T item)
    {
        var node = new Node(item) { Next = head };
        if (head != null) head.Prev = node;
        head = node;
        if (tail == null) tail = head;
    }

    public void AddToBack(T item)
    {
        var node = new Node(item) { Prev = tail };
        if (tail != null) tail.Next = node;
        tail = node;
        if (head == null) head = tail;
    }

    public T RemoveFromFront()
    {
        if (head == null)
            throw new InvalidOperationException("Deque is empty");
        var data = head.Data;
        head = head.Next;
        if (head == null) tail = null;
        else head.Prev = null;
        return data;
    }

    public T RemoveFromBack()
    {
        if (tail == null)
            throw new InvalidOperationException("Deque is empty");
        var data = tail.Data;
        tail = tail.Prev;
        if (tail == null) head = null;
        else tail.Next = null;
        return data;
    }
}
public class Set<T>
{
    private T[] items;
    private int count;

    public Set()
    {
        items = new T[1];
        count = 0;
    }

    public void Add(T item)
    {
        if (Contains(item))
            return;

        if (count == items.Length)
            Resize(items.Length + 1);

        items[count++] = item;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (items[i] != null && items[i].Equals(item))
                return true;
        }
        return false;
    }

    public void Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (items[i] != null && items[i].Equals(item))
            {
                for (int j = i; j < count - 1; j++)
                {
                    items[j] = items[j + 1];
                }

                items[--count] = default;
                Resize(items.Length - 1);
                return;
            }
        }
    }

    public T[] GetItems()
    {
        T[] result = new T[count];
        for (int i = 0; i < count; i++)
            result[i] = items[i];
        return result;
    }


    private void Resize(int newSize)
    {
        T[] newArray = new T[newSize];
        for (int i = 0; i < count; i++)
        {
            newArray[i] = items[i];
        }
        items = newArray;
    }


    public int Count => count;
}
public class BracketValidator
{
    public static bool IsValid(string input)
    {
        var stack = new Stack<char>();

        foreach (char c in input)
        {
            if (c == '{')
            {
                stack.Push(c);
            }
            else if (c == '}')
            {
                if (stack.IsEmpty())
                {
                    return false;
                }
                stack.Pop();
            }
        }
        return stack.IsEmpty();
    }
}
public class Program
{
    public static void Main()
    {
        //Тести
        var stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);
        Console.WriteLine(stack.Pop());

        
        var queue = new Queue<string>();
        queue.Enqueue("Hello");
        queue.Enqueue("World");
        Console.WriteLine(queue.Dequeue());

        
        var deque = new Deque<int>();
        deque.AddToFront(1);
        deque.AddToBack(2);
        deque.AddToBack(3);
        deque.AddToFront(4);
        Console.WriteLine(deque.RemoveFromFront());
        Console.WriteLine(deque.RemoveFromBack());

        var set = new Set<int>();
        set.Add(10);
        set.Add(20);
        set.Add(30);
        set.Add(20);

        Console.WriteLine("Елементи пiсля додавання:");
        foreach (var item in set.GetItems())
        {
            Console.WriteLine(item);
        }

        string input1 = "{{}}";
        string input2 = "{{}";
        string input3 = "{}}";
        string input4 = "{{{}}}";

        Console.WriteLine($"Рядок \"{input1}\" коректний: {BracketValidator.IsValid(input1)}");
        Console.WriteLine($"Рядок \"{input2}\" коректний: {BracketValidator.IsValid(input2)}");
        Console.WriteLine($"Рядок \"{input3}\" коректний: {BracketValidator.IsValid(input3)}");
        Console.WriteLine($"Рядок \"{input4}\" коректний: {BracketValidator.IsValid(input4)}");
    }
}

