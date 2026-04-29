public interface IStack<T>{
  void Push(T Item);
  T? Pop();
  T? Peek();

  bool Empty { get; }
  bool Full { get; }
  int Count { get; }  
  int Size { get; }  
}