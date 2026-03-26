using System.Collections;
using System.Text;

namespace ToDo;

public class SinglyLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    public SingleNode<T> Head;
    public int Count{get => GetCountFrom(Head, 0);}
    //public int Count => count; //if we use the private field count to keep track of additiond/deletions
    public bool IsEmpty() => Head == null; //Count == 0;
    private int count;

    public SinglyLinkedList()
    {
        Head = null;
        count = 0;
    }

    public void AddFirst(T value)
    {    
      SingleNode<T> newNode = new SingleNode<T>(value, Head);
      Head = newNode;
      //Head = new SingleNode<T>(value, Head);
      count++;
    }

    public void AddLast(T value)
    {
       SingleNode<T> lastNode = Head;

       if(lastNode == null) // => Head == null, therefore Empty List => as for AddFirst;
       {	
          Head = new SingleNode<T>(value, Head);
          count++;
          return;
       }
       //Search for Last:
       while(lastNode.Next != null)
       {
			 		lastNode = lastNode.Next;
       }
       //Addition:
       lastNode.Next = new SingleNode<T>(value);
       count++;
    }

    public bool Remove(T value)
    {
			if(Head == null) return false;
      var currentNode = Head;
			if(currentNode.Value.CompareTo(value) == 0) //Remove current Head
			{
				Head = Head.Next;
				count--;
				return true;
			}
			while(currentNode.Next != null && currentNode.Next.Value.CompareTo(value) != 0)
			{
				currentNode = currentNode.Next;
			}
			
			if(currentNode.Next == null) return false; //Not Found
			
			currentNode.Next = currentNode.Next.Next;
			count--;
			return true;
    }

		public bool Remove_From_OrderedList(T value)
    {
			if(Head == null) return false;
      var currentNode = Head;
			if(currentNode.Value.CompareTo(value) == 0) //Remove current Head
			{
				Head = Head.Next;
				count--;
				return true;
			}
			while(currentNode.Next != null && currentNode.Next.Value.CompareTo(value) < 0) //earlier stop
			{
				currentNode = currentNode.Next;
			}
			
			if(currentNode.Next == null || currentNode.Value.CompareTo(value) > 0) return false; //Not Found
			
			currentNode.Next = currentNode.Next.Next;
			count--;
			return true;
    }

		public bool Remove_Search(T value)
    {
			var nodeFound = Search(value);
      if(nodeFound == null) return false;
			return DeleteNode(nodeFound);    
    }

		public bool DeleteNode(SingleNode<T> nodeToDelete)
		{
			if(Head == null || nodeToDelete == null) return false;
			var currentNode = Head;
			if(currentNode == nodeToDelete) //Remove current Head
			{
				Head = Head.Next;
				count--;
				return true;
			}
			while(currentNode.Next != null && currentNode.Next != nodeToDelete)
			{
				currentNode = currentNode.Next;
			}

			if(currentNode.Next == null) return false; //Not Found
			
			currentNode.Next = currentNode.Next.Next;
			count--;
			return true;
		}

    public bool RemoveLast()
		{
			if(Head == null) return false;
			var currentNode = Head;
			if(currentNode.Next == null) //Remove current Head
			{
				Head = currentNode.Next;
				count--;
				return true;
			}

			while(currentNode.Next.Next != null)
			{
				currentNode = currentNode.Next;
			}

			currentNode.Next = currentNode.Next.Next;
			count--;
			return true;
			
		}

    public SingleNode<T> GetLast()
    {
       SingleNode<T> currentNode = Head;
       if(currentNode == null) return currentNode;
       while(currentNode.Next != null)
       {
         currentNode = currentNode.Next;
       }
       return currentNode;
    }

    public SingleNode<T> Search(T value)
    {
      if(Head == null) return null; //Empty List
      SingleNode<T> currentNode = Head;
			while(currentNode != null)// && currentNode.Value.CompareTo(value) <= 0) //<== OrderedList
			{
				if(currentNode.Value.CompareTo(value) == 0) return currentNode; //Found
				currentNode = currentNode.Next;
			}
			return null; //NotFound
    }

    public bool Contains(T value) => Search(value) != null && 
                                     Search(value).Value.CompareTo(value) == 0;

    public void AddSorted(T value)
    {
				var currentNode = Head;
        if(Head == null || currentNode.Value.CompareTo(value) >= 0){
				  Head = new SingleNode<T>(value, Head);
					count++;
					return;
				}

				while(currentNode.Next != null && currentNode.Next.Value.CompareTo(value) < 0)
				{
					currentNode = currentNode.Next;
				}
        
				var newNode = new SingleNode<T>(value, currentNode.Next);
				currentNode.Next = newNode;
				count++;
    }

    public void AddSorted(SingleNode<T> node)
    {
        if(node == null) return;
				var currentNode = Head;
        if(Head == null || currentNode.Value.CompareTo(node.Value) >= 0){
          node.Next = Head;
				  Head = node;
					count++;
					return;
				}

				while(currentNode.Next != null && currentNode.Next.Value.CompareTo(node.Value) < 0)
				{
					currentNode = currentNode.Next;
				}
        
				node.Next = currentNode.Next;
				currentNode.Next = node;
				count++;
    }

    public bool DeleteFirst()
    {
      if(Head == null) return false;
      Head = Head.Next;
      return true;
    }

    public bool DeleteLast()
    {
      if(Head == null) return false;
      var currentNode = Head;
      if(Head.Next == null)
      {
        Head = Head.Next;
        return true;
      }
      while(currentNode.Next.Next != null)
      {
        currentNode = currentNode.Next;
      }
      currentNode.Next = currentNode.Next.Next;
      return true;
      
    }
    
    public bool IsOrdered() => IsOrderedRec(Head);

    public bool IsOrderedRec(SingleNode<T> currentNode, bool flag = true) => 
                              Head == null || Head.Next == null || currentNode == null? true :
                              currentNode.Next == null ? flag :
                              flag && IsOrderedRec(currentNode.Next, currentNode.Value.CompareTo(currentNode.Next.Value) <= 0);
    
    public void InsertionSort()
    {
      if (Head == null || Head.Next == null)
          return;

      SingleNode<T> current = Head.Next;
      SingleNode<T> lastSorted = Head;

      while (current != null)
      {
          // If current is already >= lastSorted, it is in correct position
          if (current.Value.CompareTo(lastSorted.Value) >= 0)
          {
              lastSorted = current;
          }
          else //Not ordered
          {
              // Deletion of current
              lastSorted.Next = current.Next;
              // Insert it on the left using AddSorted
              AddSorted(current.Value);
          }
          current = lastSorted.Next;
      }
    }

    public void Clear()
    {
      Head = null;
      count = 0;
    }

    public int GetCountFrom(SingleNode<T> start, int acc = 0) => 
                           start == null ? acc : 
                           GetCountFrom(start.Next, acc + 1);
   
	  public int GetCountFrom(SingleNode<T> start)
    {
        if(start == null) return 0;
        return 1 + GetCountFrom(start.Next);
    }

    public int GetCountFrom_(SingleNode<T> start)
    {
        if(start == null) return 0;
        int count = 0;
        while(start != null)
        {
            start = start.Next;
            count++;
        }
        return count;
    }

    public string Display(SingleNode<T> start, string acc = "") => 
                          start == null? acc :
                          Display(start.Next, acc + $"[v:{start.Value}]──▶");

    public T[] ToArray()
    {
      if(Head == null) return null;
      var array = new T[Count];
      int idx = 0;
      var currentNode = Head;
      while(currentNode != null && idx < array.Length)
      {
        array[idx] = currentNode.Value;
        idx++;
        currentNode = currentNode.Next;
      }
      return array;   
    }

    public IEnumerable<T> ToCollection()
    {
      if(Head == null) return null;
      var collection = new List<T>();
      var currentNode = Head;
      while(currentNode != null)
      {
        collection.Add(currentNode.Value);
        currentNode = currentNode.Next;
      }
      return collection;   
    }

    public string VisualizeList(SingleNode<T> start)
    { 
        var current = start;

        var result = new StringBuilder();

        var top = new StringBuilder();
        var mid1 = new StringBuilder();
        var mid2 = new StringBuilder();
        var bottom = new StringBuilder();

        while (current != null)
        {           
          top.Append( "┌────────────┐┌───────┐      ");
          mid1.Append("│ListNode    ││       │      ");
          mid2.Append(current.Next != null
              ? $"│Value: {current.Value, -3}  ││ Next:─┼────▶ "
              : $"│Value: {current.Value, -3}  ││ Next:─┼────▶ <NULL>");
          bottom.Append("└────────────┘└───────┘      ");

          current = current.Next;
        }

        result.AppendLine(top.ToString());
        result.AppendLine(mid1.ToString());
        result.AppendLine(mid2.ToString());
        result.AppendLine(bottom.ToString());

        // Add head pointer
        result.AppendLine("    ▲");
        result.AppendLine("    |");
        result.AppendLine("   Head");

        return result.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        SingleNode<T>? current = Head;
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