
public interface ILinkedList<T> : IEnumerable<T> where T : IComparable<T>
{
    void AddFirst(T value);
    void AddLast(T value);
    void AddSorted(T value);
    bool Remove(T value);
    SingleNode<T>? Search(T value);
    bool Contains(T value);
    void Clear();
    int Count { get; }
    IEnumerator<T> GetEnumerator();
}