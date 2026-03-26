

using ToDo;

Random rand = new Random(); 
/*
//SinglyLinkedList
SinglyLinkedList<int> intList = new SinglyLinkedList<int>();

var array = GenerateArray(4, false);
System.Console.Write("Array values: ");
array.ToList().ForEach(_ => System.Console.Write($"{_} : "));
System.Console.WriteLine();

    for(int i = 0; i < array.Length; ++i){
        //Addition based on value
        intList.AddSorted(array[i]);
        System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
        System.Console.WriteLine($"\nIs the List above ordered:{intList.IsOrdered()}\n");
        Thread.Sleep(1200);
    }
    intList.RemoveLast();
    System.Console.WriteLine($"\n{intList.VisualizeList(intList.Head)}");
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
  
    // for(int i = 0; i < array.Length; ++i){
    //     //Addition based on value
    //     stringList.AddSorted(array[i].ToString());
    // }

    // foreach(var el in stringList) {
    //     //System.Console.Write($"[v:\"{el}\"]──▶" ) ;
    //     if(cnt % 20 == 0 && el != stringList.Head.Value) 
    //         System.Console.Write($"[v:\"{el}\"]──▶\n");
    //     else
    //         System.Console.Write($"[v:\"{el}\"]──▶" ) ;
    //     cnt++;
    //     Thread.Sleep(200);
    // }

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

*/




//DoublyLinkedList
 var array_ = GenerateArray(5, false);
 DoublyLinkedList<int> dl_ = new DoublyLinkedList<int>();
 for(int i = 0; i < array_.Length; ++i)
 {
    dl_.AddSorted_(array_[i]);   
 }

var listNode = dl_.First;
while (listNode != null) {
    if(listNode.Equals(dl_.First)) 
        System.Console.Write($"<NULL> <--[First v:\"{listNode.Value}\"]");
    else if(listNode.Equals(dl_.Last))
        System.Console.Write($"<-->[Last v:\"{listNode.Value}\"]--> <NULL>");
    else
        System.Console.Write($"<-->[v:\"{listNode.Value}\"]" ) ;
    Thread.Sleep(200);
    listNode = listNode.Next;
 }
 System.Console.WriteLine();
 dl_.InsertAfter(dl_.First, 725705077);
 dl_.InsertAfter(dl_.First.Next, 725705077);
 dl_.InsertAfter(dl_.Last, 725705077);
 listNode = dl_.First;

 while (listNode != null) {
    if(listNode.Equals(dl_.First)) 
        System.Console.Write($"<NULL> <--[First v:\"{listNode.Value}\"]");
    else if(listNode.Equals(dl_.Last))
        System.Console.Write($"<-->[Last v:\"{listNode.Value}\"]--> <NULL>");
    else
        System.Console.Write($"<-->[v:\"{listNode.Value}\"]" ) ;
    Thread.Sleep(200);
    listNode = listNode.Next;
 }

 dl_.Delete(dl_.Last);
 System.Console.WriteLine();
 listNode = dl_.First;
 while (listNode != null) {
    if(listNode.Equals(dl_.First)) 
        System.Console.Write($"<NULL> <--[First v:\"{listNode.Value}\"]");
    else if(listNode.Equals(dl_.Last))
        System.Console.Write($"<-->[Last v:\"{listNode.Value}\"]--> <NULL>");
    else
        System.Console.Write($"<-->[v:\"{listNode.Value}\"]" ) ;
    Thread.Sleep(200);
    listNode = listNode.Next;
 }

var n1 = new DoubleNode<int>(5, null, null);
var n2 = new DoubleNode<int>(-3, null, n1);
var n3 = new DoubleNode<int>(2, null, n2);
var n4 = new DoubleNode<int>(-3, null, n3);
var n5 = new DoubleNode<int>(4, null, n4);
n1.Next = n2;
n2.Next = n3;
n3.Next = n4;
n4.Next = n5;

DoublyLinkedList<int> dl = new DoublyLinkedList<int>();
dl.First = n1;
dl.Last = n5;

System.Console.WriteLine("Double Linked List:");
foreach(var el in dl) 
    System.Console.WriteLine(el);

int[] actual_ = new int[]{ dl.First.Value, 
                           dl.First.Next.Value, 
                           dl.First.Next.Next.Value,
                           dl.First.Next.Next.Next.Value,
                           dl.First.Next.Next.Next.Next.Value 
                         };    

System.Console.WriteLine("\nFirst -> Last:");
foreach(var el in actual_) 
    System.Console.WriteLine(el);

actual_ = new int[] {
                        dl.Last.Value,
                        dl.Last.Previous.Value,
                        dl.Last.Previous.Previous.Value,
                        dl.Last.Previous.Previous.Previous.Value,
                        dl.Last.Previous.Previous.Previous.Previous.Value,
                    };

System.Console.WriteLine("\nLast -> First:");
foreach(var el in actual_) 
    System.Console.WriteLine(el);

System.Console.WriteLine();


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










