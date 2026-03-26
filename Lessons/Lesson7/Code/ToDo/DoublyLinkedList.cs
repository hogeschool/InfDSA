using System.Collections;

namespace ToDo;

public class DoublyLinkedList<T> : IDoublyLinkedList<T> where T : IComparable<T>
{
    public DoubleNode<T>? First, Last;
    public DoublyLinkedList() => First = Last = null;
    public void Clear() => First = Last = null;
    public bool IsEmpty() => First == null && Last == null;

    //Search
    public DoubleNode<T>? Search(T value)
    {
        if(First == null && Last == null) return null;

        var currentNode = First;
        while(currentNode != null)
        {
            if(currentNode.Value.CompareTo(value)==0) 
                return currentNode;
            
            currentNode = currentNode.Next;
        }
        return currentNode;
    }

    public bool Contains(T value)
    {
        if(First == null && Last == null) return false;
        DoubleNode<T> foundNode = Search(value);
        return foundNode != null  && foundNode.Value.CompareTo(value) == 0;
    }

    #region "addNode=> first, last, sorted" 
    
    public void AddFirst(T value)
    {
        var newNode = new DoubleNode<T>(value, First, null);
        if (First == null && Last == null)
        {
            First = newNode;
            Last = First;
        }
        else if(First != null && Last != null){
            First.Previous = newNode;
            First = newNode;
        }
    }

    public void InsertAfter(DoubleNode<T> node, T value)
    {
        DoubleNode<T> newNode = new DoubleNode<T>(value);

        newNode.Next = node.Next;
        newNode.Previous = node;
        if (node.Next != null)
        {
            node.Next.Previous = newNode;
        }
        else
        {
            Last = newNode;
        }
        node.Next = newNode;
    }

    public void AddLast(T value)
    {
        var newNode = new DoubleNode<T>(value, null, Last);
        if(First == null && Last == null)
        {
            First = newNode;
            Last = First;
        }
        else
        {
            Last.Next = newNode;
            Last = newNode;
        }
    }

    public void AddSorted_(T value)
    {
        if (First == null)
        {
            AddFirst(value);
            return;
        }

        if (value.CompareTo(First.Value) <= 0)
        {
            AddFirst(value);
            return;
        }

        var current = First;

        while (current.Next != null && current.Next.Value.CompareTo(value) < 0)
        {
            current = current.Next;
        }

        if (current.Next == null)
        {
            AddLast(value);
            return;
        }

        InsertAfter(current, value);
}


    public void AddSorted(T value)
    {
        var newNode = new DoubleNode<T>(value);

        //Empty List
        if(First == null && Last == null)
        {
            First = newNode;
            Last = First;
            return;
        }

        else if(First != null && Last != null)
        {
            var currentNode = First;
            
            //AddFirst
            if(currentNode.Value.CompareTo(value) >= 0)
            {
                newNode.Next = First;
                First.Previous = newNode;
                First = newNode;
                return;
            }
            //Certainly not at the beginning...
            while(currentNode.Next != null && 
                  currentNode.Next.Value.CompareTo(value) < 0)
            {
                currentNode = currentNode.Next;
            }
            
            //AddLast
            if(currentNode == Last)  // currentNode.Next == null
            {
                newNode.Previous = Last;
                Last.Next = newNode;
                Last = newNode;
                return;
            }
            //Certainly not at the end.
            newNode.Previous = currentNode;
            newNode.Next = currentNode.Next;
            currentNode.Next.Previous = newNode;
            currentNode.Next = newNode;
        }     
    }
    #endregion

    public bool Remove(T value)
    {
       //Empty List
       if(First == null && Last == null) return false;
       DoubleNode<T> foundNode = Search(value);
       if(foundNode != null  && foundNode.Value.CompareTo(value) == 0)
       {
            Delete(foundNode);
            return true;
       }
       return false;
    }

    public void Delete_(DoubleNode<T> nodeToDelete)
    {
                  
        if (nodeToDelete.Previous != null) // check Prev
            nodeToDelete.Previous.Next = nodeToDelete.Next;
        if (nodeToDelete.Next != null) // check Next
            nodeToDelete.Next.Previous = nodeToDelete.Previous;
        if (First == nodeToDelete) // check First
            First = nodeToDelete.Next;
        if (Last == nodeToDelete) // check Last
            Last = nodeToDelete.Previous;   
        
    }
    public void Delete(DoubleNode<T> nodeToDelete)
    {
        /*          
        if (nodeToDelete.Previous != null) // check Prev
            nodeToDelete.Previous.Next = nodeToDelete.Next;
        if (nodeToDelete.Next != null) // check Next
            nodeToDelete.Next.Previous = nodeToDelete.Previous;
        if (First == nodeToDelete) // check First
            First = nodeToDelete.Next;
        if (Last == nodeToDelete) // check Last
            Last = nodeToDelete.Previous;   
        */

        if(First == null && Last == null || nodeToDelete == null) return;

        else if(First != null && Last != null && nodeToDelete != null)
        {
            //At beginning?
            if(nodeToDelete == First)//First.Value.CompareTo(nodeToDelete.Value) == 0)
            {
                if(First == Last) //Only one node, List will be empty after deletion
                {
                    First = nodeToDelete.Next; //nodeToDelete.Next: null, nodeToDelete.Previous: null, 
                    Last = First;
                    return;
                }
                else
                {
                    First = nodeToDelete.Next;
                    First.Previous = nodeToDelete.Previous; //nodeToDelete.Previous: null
                    return;    
                }

            }

            //At the End?
            if(nodeToDelete == Last)//Last.Value.CompareTo(nodeToDelete.Value) == 0) 
            {
                /*
                if(First == Last) //Only one node, List will be empty after deletion
                {
                    First = nodeTodelete.Next;
                    Last = First;
                    return;
                }
                else
                */
                //{
                    Last.Previous.Next = nodeToDelete.Next;
                    Last = nodeToDelete.Previous;
                    return;       
                //}
            }

            //In between: nodeToDelete.Previous != null && nodeToDelete.Next != null
            nodeToDelete.Previous.Next = nodeToDelete.Next;
            nodeToDelete.Next.Previous = nodeToDelete.Previous;

        }

    }

    public IEnumerator<T> GetEnumerator()
    {
        DoubleNode<T>? current = First;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}

