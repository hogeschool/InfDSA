namespace Solution;
public class Stack<T>:IStack<T>
{
    protected int capacity;
    protected int top = -1;
    protected T[] array;

    public bool Empty => top==-1;

    public bool Full => top == capacity-1;

    public int Count => top+1;

    public int Size=> capacity;

    public Stack(int size = 10)
    {
        capacity = size;
        array = new T[capacity];
    }


    public Option<T> Pop()
    {
        if (!Empty) // stack is not empty
        {
            T value = array[top];
            array[top] = default(T);
            top--;
            return new Some<T>(value);
        }
        return new None<T>();
    }

    public void Push(T Item)
    {
        if (top == capacity - 1) // stack is full
        {
            capacity = capacity * 2;
            var newArray = new T[capacity];
            for (int i = 0; i <= top; i++)
                newArray[i] = array[i];
            array = newArray;
        }
        array[++top] = Item;
    }

    public Option<T> Peek()
    {
        if (!Empty) return new Some<T>(array[top]);
        return new None<T>();
    }
}