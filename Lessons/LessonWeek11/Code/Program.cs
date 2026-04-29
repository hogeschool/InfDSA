using Solution;


//Lesson 11 Stack/Queue

//Stack array based
IStack<char> stk = new Solution.Stack<char>(1);
stk.Push('a');
stk.Push('b');
char[] actual = new char[4];
actual[0] = stk.Pop();
stk.Push('c');
actual[1] = stk.Pop();
actual[2] = stk.Pop();
actual[3] = stk.Pop();
System.Console.WriteLine();
var word = "abcdefghijklmnopqrst";
IStack<char> charStack = new Solution.Stack<char>();
for(int i = 0; i < word.Length; ++i)
{
    charStack.Push(word[i]);
}
char[] reversedChars = new char[word.Length];
for(int i = 0; i < word.Length; ++i)
{
    reversedChars[i]=charStack.Pop();
}

var reversedWord = new String(reversedChars);
System.Console.Write(word);
System.Console.WriteLine($" Reversed: {reversedWord}");
System.Console.WriteLine();

//Queue array based
List<string?> aList = new List<string?>();
IQueue<string?> q = new Solution.Queue<string?>(4); ;
q.Enqueue("Alpha");
q.Enqueue("Bravo");
q.Enqueue("Charlie");
aList.Add(q.Dequeue());
q.Enqueue("Delta");
q.Enqueue("Delta2");
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
q.Enqueue("Alpha1");
q.Enqueue("Bravo1");
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());

foreach(var el in aList)
    System.Console.WriteLine(el);


//Stack Linked List (node) based

IStack<string> stkLL =  new StackLL<string>();

for(int i = 1; i <= 10; ++i) {
  if(i % 3 == 0) 
    Console.WriteLine($"Peek() => {(stkLL.Peek() == null? "<NULL>" : stkLL.Peek())}, after Peek -> Count: {stkLL.Count}");
  stkLL.Push(i + "");
  Console.WriteLine($"Push({i}); after Push -> Count: {stkLL.Count} ");
  if(i % 5 == 0) {
    var val = stkLL.Pop();
    Console.WriteLine($"Pop() => {(val == null? "<NULL>" : val)}, after Pop -> Count: {stkLL.Count}");
  }
}
System.Console.WriteLine($"Pop() until Count is 0, Now -> Count: {stkLL.Count}");
var idx = 1;
while(stkLL.Count > 0){
    var val = stkLL.Pop();
    Console.WriteLine($"{idx++}) Pop() => {(val == null? "<NULL>" : val)}, after Pop -> Count: {stkLL.Count}");
}
System.Console.WriteLine($"----- Stack Count: {stkLL.Count}  -----");
var res = stkLL.Pop();
Console.WriteLine($"Pop() => {(res == null? "<NULL>" : res)}, after Pop -> Count: {stkLL.Count}");
res = stkLL.Peek();
Console.WriteLine($"Peek() => {(res == null? "<NULL>" : res)}, after Peek -> Count: {stkLL.Count}");
System.Console.WriteLine();

//Queue Linked List (node) based

List<string?> aList_ = new List<string?>();
IQueue<string?> qLL = new QueueLL<string?>(); ;
qLL.Enqueue("Alpha");
qLL.Enqueue("Bravo");
qLL.Enqueue("Charlie1");

aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());

qLL.Enqueue("Charlie2");
qLL.Enqueue("Delta");

aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());
aList_.Add(qLL.Dequeue());

int cnt = 1;
foreach(var el in aList_){
    System.Console.WriteLine($"{(el != null ? el : $"<NULL>{cnt++}")}");
}
System.Console.WriteLine();