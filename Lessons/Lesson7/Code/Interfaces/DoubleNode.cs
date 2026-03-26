// class for a DoublyLinked List Node
public class DoubleNode<T> where T : IComparable<T>
{
    public T Value { get; private set; }
    public DoubleNode<T>? Next { get; set; }
    public DoubleNode<T>? Previous { get; set; }

    public DoubleNode(T value, 
                      DoubleNode<T>? next = null, 
                      DoubleNode<T>? previous = null)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }
}
