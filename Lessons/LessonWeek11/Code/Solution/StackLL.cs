namespace Solution;

public class StackLL<T> : IStack<T>
{
  public Node<T>? Top;
  public int Count => computeCount(Top);

  public bool Empty => Top == null; //Count == 0;

  public bool Full => throw new NotImplementedException("Never Full");

  public int Size => Count;

  public StackLL()
  {
    Top = null;
  }

  public void Push(T val)
  {  
    Top = new Node<T>(val, Top);
  }

  public T? Pop()
  {  
    if(Top==null) return default;
    var r = Top.Value;
    Top = Top.Next;
    return r;
  }

  public T? Peek()
  {
    if(Top==null) return default;
    return  Top.Value;
  }

  int computeCount(Node<T> node) => (node == null) ? 0 : 1 + computeCount(node.Next);

}
