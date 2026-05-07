using Solution;


//Lesson 11 Stack/Queue using Option pattern

//Stack array based
IStack<char> stk = new Solution.Stack<char>(1);
stk.Push('a');
stk.Push('b');
Option<char>[] actual = new Option<char>[4];
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
    if(charStack.Peek() is Some<char>)
        reversedChars[i]= charStack.Pop().Value;
}

var reversedWord = new String(reversedChars);
System.Console.Write(word);
System.Console.WriteLine($" Reversed: {reversedWord}");
System.Console.WriteLine();

//Queue array based
List<Option<string>> aList = new List<Option<string>>();
IQueue<string> q = new Solution.Queue<string>(4); ;
q.Enqueue("Alpha");
q.Enqueue("Bravo");
q.Enqueue("Charlie");
aList.Add(q.Dequeue());
q.Enqueue("Delta");
q.Enqueue("Delta2");
q.Enqueue("Alpha1");
aList.Add(q.Peek());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());
q.Enqueue("Alpha2");
q.Enqueue("Bravo1");
aList.Add(q.Dequeue());
aList.Add(q.Dequeue());

foreach(var el in aList)
    Console.WriteLine(el is None<string> ? "<NULL>" : el.Value);


//Stack Linked List (node) based

IStack<string> stkLL =  new StackLL<string>();

for(int i = 1; i <= 10; ++i) {
  if(i % 3 == 0) 
    Console.WriteLine($"Peek() => {(stkLL.Peek() is None<string> ? "<NULL>" : stkLL.Peek().Value)}, after Peek -> Count: {stkLL.Count}");
  stkLL.Push(i + "");
  Console.WriteLine($"Push({i}); after Push -> Count: {stkLL.Count} ");
  if(i % 5 == 0) {
    var val = stkLL.Pop();
    Console.WriteLine($"Pop() => {(val is None<string> ? "<NULL>" : val.Value)}, after Pop -> Count: {stkLL.Count}");
  }
}
System.Console.WriteLine($"Pop() until Count is 0, Now -> Count: {stkLL.Count}");
var idx = 1;
while(stkLL.Count > 0){
    var val = stkLL.Pop();
    Console.WriteLine($"{idx++}) Pop() => {(val is None<string> ? "<NULL>" : val.Value)}, after Pop -> Count: {stkLL.Count}");
}
System.Console.WriteLine($"----- Stack Count: {stkLL.Count}  -----");
var res = stkLL.Pop();
Console.WriteLine($"Pop() => {(res is None<string> ? "<NULL>" : res.Value)}, after Pop -> Count: {stkLL.Count}");
res = stkLL.Peek();
Console.WriteLine($"Peek() => {(res is None<string> ? "<NULL>" : res.Value)}, after Peek -> Count: {stkLL.Count}");
System.Console.WriteLine();

//Queue Linked List (node) based

List<Option<string>> aList_ = new List<Option<string>>();
IQueue<string> qLL = new QueueLL<string>(); ;
qLL.Enqueue("Alpha");
qLL.Enqueue("Bravo");
qLL.Enqueue("Charlie1");

aList_.Add(qLL.Peek());
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
    System.Console.WriteLine($"{(el is Some<string> ? el.Value : $"<NULL>{cnt++}")}");
}
System.Console.WriteLine();