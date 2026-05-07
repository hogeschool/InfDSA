namespace Solution;

public class QueueLL<T> : IQueue<T>
{
    public Node<T>? Front, Back;

    public bool Empty => Front == null && Back == null; //Count == 0;
    public int Count => computeCount(Front, 0);

    public bool Full => throw new NotImplementedException("Never Full");

    public int Size => Count;

    public QueueLL()
    {
        Front = Back = null;
    }

    public void Enqueue(T value)
    {
        //AddLast
        var newNode = new Node<T>(value);
        if(Front == null) {
            Front = newNode;
            Back = Front;
            return;
        }

        //In case Back (Queue tail/last node) not directly available
        // var curr = Front;
        // while(curr.Next != null)
        // {
        //     curr = curr.Next;
        // }
        // Back = curr;//var Back = curr;

        Back.Next = newNode;
        Back = newNode;
 
    }

    public Option<T> Dequeue()
    { 
        //Read/DeleteFront

        if(Front == null) {
            return new None<T>();
        }

        var res = Front.Value;
        
        if (Front == Back) {
            Front = null;
            Back = Front;
            return new Some<T>(res);
        }
        
        Front = Front.Next;
        return new Some<T>(res);
    }

    public Option<T> Peek()
    { 
        //ReadFront
        
        if(Front == null) {
            return new None<T>();
        }

        return new Some<T>(Front.Value);
    }

    int computeCount() {
        var total = 0;
        var node = Front;
        while(node != null) {
            total++;
            node = node.Next;
        }
        return total;
    }

    int computeCount<T>(Node<T> node, int total) => (node == null) ? total : 
                                                    computeCount(node.Next, total + 1);

}


