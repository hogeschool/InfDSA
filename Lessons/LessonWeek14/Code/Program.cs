
using Solution;
using System.Diagnostics;

var rand = new Random();
Stopwatch stopwatch_ = new Stopwatch();

//Fibonacci recursive (non memoized => O(2^n))
Func<long, long> fib = null;
fib = n => n <= 1 ? n : fib(n - 1) + fib(n - 2);
for(int num = 30; num <= 46; num++){
    stopwatch_.Reset();
    stopwatch_.Start();
    var result = fib(num);
    stopwatch_.Stop();
    System.Console.WriteLine($"Fib({num}) = {result}, Time elapsed: {stopwatch_.Elapsed}\n");
}
System.Console.WriteLine("\n");
long n = rand.Next(50, 65);

//>>>>> DO NOT uncomment the following line as this would take a lot of time...<<<<<
//var fibNumber = fib(n);

long[] intermediateResultsActual = new long[n + 1];
// initialize the first two values in the array which we use as a map
intermediateResultsActual[0] = 0;
intermediateResultsActual[1] = 1;

Utils.SetToZero();
stopwatch_.Reset();
stopwatch_.Start();
var actual = DynamicProgramming.FibonacciDynamic(n, intermediateResultsActual);
stopwatch_.Stop();

System.Console.WriteLine($"\nFibMemoized({n}) = {actual}, Time elapsed: {stopwatch_.Elapsed}, recursive calls: {Utils.counter})");
System.Console.WriteLine("\nIntermediate results:");
var totalValues = intermediateResultsActual.ToList().Count(x => x != 0) + 1;
intermediateResultsActual.Take(totalValues).ToList().ForEach(System.Console.WriteLine);

Utils.SetToZero();
stopwatch_.Reset();
n = rand.Next(50, 65); 
intermediateResultsActual = new long[n + 1];
// initialize the first two values in the array which we use as a map
intermediateResultsActual[0] = 0;
intermediateResultsActual[1] = 1;
stopwatch_.Start();
var res = DynamicProgramming.FibonacciDynamic(n, intermediateResultsActual);
stopwatch_.Stop();
System.Console.WriteLine($"\n\nFibMemoized({n}) = {res}, Time elapsed: {stopwatch_.Elapsed}, recursive calls: {Utils.counter}\n");
System.Console.WriteLine();

//totalValues = intermediateResultsActual.ToList().Count(x => x != 0) + 1;
//intermediateResultsActual.Take(totalValues).ToList().ForEach(System.Console.WriteLine);
System.Console.WriteLine();