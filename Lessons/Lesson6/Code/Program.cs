

using ToDo;

SinglyLinkedList<int> intList = new SinglyLinkedList<int>();
Random rand = new Random(); 

var array = GenerateArray(4, false);
System.Console.Write("Array values: ");
array.ToList().ForEach(_ => System.Console.Write($"{_} : "));
System.Console.WriteLine();

    for(int i = 0; i < array.Length; ++i){
        //Addition based on value
        intList.AddSorted(array[i]);
        System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
        System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
        //Thread.Sleep(1200);
    }
    //System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
    SingleNode<int> newNode = new SingleNode<int>(250);
    //Addition based on value
    intList.AddLast(newNode.Value);
    System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");

    //Addition based on node (reference)
    newNode = new SingleNode<int>(35);
    intList.AddSorted(newNode);
    System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");

    intList.AddFirst(-25);
    System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");

    //Node deletion based on node (reference)
    intList.DeleteNode(newNode);
    System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
    //Node deletion based on value
    var val = intList.GetLast().Value;
    var test = intList.Remove(val);
    System.Console.WriteLine($"\nRemoval of {val}: {test}\n\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");

    var n = intList.Head.Next.Value;
    test = intList.Remove_From_OrderedList(n);
    System.Console.WriteLine($"\nRemoval of {n}: {test}\n\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
    //Node deletion based on non existing value
    int num = 250000; 
    test = intList.Remove(num);
    System.Console.WriteLine($"\nRemoval of {num}: {test}\n\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
    //Node deletion based on non existing node
    SingleNode<int> nullNode = null;
    test = intList.DeleteNode(nullNode);
    System.Console.WriteLine($"\nRemoval of null:{test}\n{intList.VisualizeList(intList.Head)}");
    System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
  

    //Other compacter visualizations
    System.Console.WriteLine(intList.Display(intList.Head));
    System.Console.WriteLine("\n" + intList.VisualizeList(intList.Head));
    foreach(var el in intList) {
        System.Console.Write($"[v:{el}]──▶");
        //Thread.Sleep(500);
    }

    var arrayFromLinkedList = intList.ToArray();
    var list = intList.ToCollection();
    System.Console.WriteLine("\n" + intList.VisualizeList(intList.Head));
    
    int size = 200;
    array = GenerateArray(size, false);
    SinglyLinkedList<string> stringList = new SinglyLinkedList<string>();
    int cnt = 0;
/*  
    for(int i = 0; i < array.Length; ++i){
        //Addition based on value
        stringList.AddSorted(array[i].ToString());
    }

    foreach(var el in stringList) {
        //System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        if(cnt % 20 == 0 && el != stringList.Head.Value) 
            System.Console.Write($"[v:\"{el}\"]──▶\n");
        else
            System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        cnt++;
        Thread.Sleep(200);
    }
*/
    System.Console.WriteLine("\n \n  \n");

    size = 20;
    array = GenerateArray(size, false);
    var intList2 = new SinglyLinkedList<int>();
    for(int i = 0; i < array.Length; ++i){
        //Addition based on value
        intList2.AddFirst(array[i]);
    }

    foreach(var el in intList2) {
        //System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        if(cnt % 20 == 0 && el != intList2.Head.Value) 
            System.Console.Write($"[v:\"{el}\"]──▶\n");
        else
            System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        cnt++;
        Thread.Sleep(200);
    }

    intList2.InsertionSort();
    System.Console.WriteLine("\n\nSorted List:");
    foreach(var el in intList2) {
        //System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        if(cnt % 20 == 0 && el != intList2.Head.Value) 
            System.Console.Write($"[v:\"{el}\"]──▶\n");
        else
            System.Console.Write($"[v:\"{el}\"]──▶" ) ;
        cnt++;
        Thread.Sleep(200);
    }


static int[] GenerateArray(int n, bool ordered = true) {
  Random rnd = new Random();
  return ordered?
    Enumerable
    .Range(0, n)
    .ToArray() :
    Enumerable
    .Range(0, n)
    .Select(_ => rnd.Next(_, _ + 40))
    .ToArray() 
    ;
}










