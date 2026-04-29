
namespace Solution;

public class Queue<T> : IQueue<T>
{
    private int front;
    private int back;
    private T[] data;
    private int _count = 0;

    public bool Empty => _count == 0;   
    public bool Full => _count == Size; 
    public int Count => _count;
    public int Size => data.Length;

    public Queue(int capacity = 5)
    {
        data = new T[capacity];
        front = -1;
        back = -1;
    }

    public void Enqueue(T element)
    {
        if (_count == 0)//Empty) // queue is empty
        {
            front = 0; back = 0;
            data[back] = element;
            _count++;
            return;
        }

        if (_count == data.Length)//Full) // queue is full (do nothing)
        {
            throw new Exception("Queue is already full");
            //return;
        }

        back = (back + 1) % data.Length;
        data[back] = element;
        _count++;

    }

    public T? Dequeue()
    {
        if (Empty) // queue is empty
        {
            return default;
        }
        T element = data[front]; 
        data[front] = default(T);
        front = (front + 1) % data.Length;
        _count--;
        return element;
    }

}
