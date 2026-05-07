public interface IQueue<T>{
  void Enqueue(T Item);
  Option<T> Dequeue();
  Option<T> Peek();

  bool Empty { get; }
  bool Full { get; }
  int Count { get; }  
  int Size { get; }  
}