
public interface IBST<T> where T : IComparable<T>
{
    void Insert(T value);
    bool Remove(T value);
    bool Contains(T value);
    string PreOrderTraversal();
    string InOrderTraversal();
    string PostOrderTraversal();

    List<T>? Traversal(TraversalOrder traversalOrder);

}
public enum TraversalOrder { PreOrder, InOrder, PostOrder };