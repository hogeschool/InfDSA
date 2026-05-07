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

  public Option<T> Pop()
  {  
    if(Top==null) return new None<T>();
    var r = Top.Value;
    Top = Top.Next;
    return new Some<T>(r);
  }

  public Option<T> Peek()
  {
    if(Top==null) return new None<T>();
    return new Some<T>(Top.Value); 
  }

  int computeCount<T>(Node<T> node) => (node == null) ? 0 : 1 + computeCount(node.Next);

}
