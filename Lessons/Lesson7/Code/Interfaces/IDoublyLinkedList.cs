
public interface IDoublyLinkedList<T> : IEnumerable<T> where T : IComparable<T>
{
    DoubleNode<T>? Search(T value);
    void AddFirst(T value);
    void AddLast(T value); 
    void AddSorted(T value);
    bool Remove(T value);
    void Clear();
    IEnumerator<T> GetEnumerator();
}