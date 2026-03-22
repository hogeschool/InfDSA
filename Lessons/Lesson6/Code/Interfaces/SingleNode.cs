// class for a Single Linked List Node

public class SingleNode<T> where T : IComparable<T>
{
    public T Value { get; private set; }
    public SingleNode<T>? Next { get; set; }

    public SingleNode(T value, SingleNode<T>? next = null)
    {
        Value = value;
        Next = next;
    }  
}

