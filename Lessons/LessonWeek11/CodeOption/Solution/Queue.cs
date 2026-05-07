
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

        if (_count == data.Length)//Full) // queue is full
        {
            //Resize:
            var newArray = new T[Size * 2];
            //Copy from front to the index before front using the wrap around
            int i = front;
            for(int j = 0; j < _count; ++j)
            {
                newArray[j] = data[i];
                i = (i + 1) % Size;
            }
            data = newArray;
            front = 0;
            back = _count - 1;
        }

        back = (back + 1) % data.Length;
        data[back] = element;
        _count++;

    }

    public Option<T> Dequeue()
    {
        if (Empty) // queue is empty
        {
            return new None<T>();
        }
        T element = data[front]; data[front] = default(T);
        front = (front + 1) % data.Length;
        _count--;
        return new Some<T>(element);
    }

    public Option<T> Peek()
    {
        if (Empty) // queue is empty
        {
            return new None<T>();
        }
        return new Some<T>(data[front]);
    }

}
