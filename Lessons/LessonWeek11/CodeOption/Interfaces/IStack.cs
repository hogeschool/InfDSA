public interface IStack<T>{
  void Push(T Item);
  Option<T> Pop();
  Option<T> Peek();

  bool Empty { get; }
  bool Full { get; }
  int Count { get; }  
  int Size { get; }  
}