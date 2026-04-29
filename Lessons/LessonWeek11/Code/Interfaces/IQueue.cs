public interface IQueue<T>{
  void Enqueue(T Item);
  T? Dequeue();

  bool Empty { get; }
  bool Full { get; }
  int Count { get; }  
  int Size { get; }  
}