

using System.Data;
using System.Diagnostics;


//Recursion Demo
//Standard Recursion
System.Console.WriteLine(sumRec(5));
//Tail Recursion / Tail Call Optimization
System.Console.WriteLine(sumRec_TCO(5));


/*
//Tree recursion
//Fibonacci numbers
for(long i = -5; i < 20; ++i)
  System.Console.WriteLine($"Fib({i}): {Fib(i)}");
*/
/*

//Empirical analysis of time complexity
Stopwatch stopwatch_ = new Stopwatch();

for(long i = -4; i <= 5; ++i){
  stopwatch_.Start();
  var n = 40 + i; 
  var res = Fib(n);
  stopwatch_.Stop();
  System.Console.WriteLine($"n: {n} result: {res}, Time elapsed: {stopwatch_.Elapsed}, 2^n: {Math.Pow(2,n)}\n");
}
System.Console.WriteLine();

*/

int[] arr = {2, 3, 5, -8, 20, 1, 3, };

var linqSum = arr.Aggregate((a, b) => a + b);
linqSum = arr.Aggregate<int, int>(default, (a, b) => a + b);

var total = Sum(arr);
total = Aggregate((a, b) => a + b, arr);
total = Reduce<int, int>((a, b) => a + b, arr);
total = Fold<int, int>((a, b) => a + b, arr, default);

var max = Max(arr);
max = Aggregate((a, b) => a > b ? a : b, arr);
max = Reduce<int, int>((a, b) => a > b ? a : b, arr);
max = Fold<int, int>((a, b) => a > b ? a : b, arr, arr[0]);

var filtered = Filter(_ => Math.Abs(_) > 2, arr);
var mapped = Map(_ => "[" + _ + "]", arr);
var res_ = Fold((n, s) => s == "" ? "" + n : s + ", " + n, arr, "");
var res__ = FoldRec((n, s) => s == "" ? "" + n : s + ", " + n, arr, 0, "");


var strArray = new string[]{
                            "Rotterdam",
                            "London", 
                            "Rome", 
                            "Paris",
                            "Rotterdam",
                            "Amsterdam",
                            "New York",
                            "London",
                            "Milan",
                            "Rotterdam", 
                            "Rome", 
                            "Paris",
                            "Rotterdam",
                            "New York",
                            "Rotterdam",
                            };

//How to create a Dictionary<string, int> containing the city names (keys), 
//and their occurrences (values) in the given array 
//using a call of Fold with the correct lambda and accumulator?
 
  
 
 var lambda = (string city, Dictionary<string, int> dict) => {
                                if(dict.ContainsKey(city))
                                  dict[city] = dict[city] + 1; 
                                else
                                  dict.Add(city, 1);
                                return dict;
                              };


Dictionary<string, int> dict = strArray.Aggregate(new Dictionary<string, int>(),
                                                  (dict, city) =>
                                                  {
                                                    if(dict.ContainsKey(city))
                                                      dict[city] = dict[city] + 1; 
                                                    else
                                                      dict.Add(city, 1);
                                                    return dict;
                                                  });

Dictionary<string, int> dict_ = Fold(
                                    (city, dict) => {
                                      if(dict.ContainsKey(city))
                                        dict[city] = dict[city] + 1; 
                                      else
                                        dict.Add(city, 1);
                                      return dict;
                                    },
                                    strArray,
                                    new Dictionary<string, int>()
                                    );

System.Console.WriteLine();

static long Fib(long n)
{
  if(n <= 1) return n;
  return Fib(n - 1) + Fib(n - 2);
}

static long Func_(long n)
{
  if(n <= 4) return n;
  return Func_(n - 1) + Func_(n - 2) + Func_(n - 3) + Func_(n - 4);
}

static long sumRecDC(long n)
{
  if (n < 0) return -1; //fallback
  
  if (n <= 1) return n;
  
  return n % 2 != 0 ? (n/2 + 1)*(n/2 + 1) + 2 * sumRecDC(n/2) : (n/2) * (n/2) + 2 * sumRecDC(n/2);
  
}

static long sumRecStd(long n) => n < 0 ? -1 : n == 1 ? 1 : n + sumRecStd(n - 1);

static long sumRec(long n) 
{
  if (n < 0) return -1;

  if (n == 1) return 1;
  return n + sumRec(n - 1);
}

static long sumRec_TCO(long n, long acc = 0) 
{
  if (n < 0) return -1;
  //if (n == 1) return 1 + acc;
  if (n == 0) return acc;
  return sumRec_TCO(n - 1, n + acc);
}

static long sumStack(long n)
{
    Stack<long> stack = new Stack<long>();

    //Populate the stack
    while (n >= 1)
    {
        stack.Push(n);
        n--;
    }

    long result = 0;
    
    //Pop and compute the sum
    while (stack.Count > 0)
    {
        result = result + stack.Pop();
    }

    return result;
}

 
static int Max(int[] array)
{
  var max = array[0];
  for(int i = 1; i < array.Length; ++i)
    max = array[i].CompareTo(max) > 0 ? array[i] : max;

  return max;
}

static int Min(int[] array)
{
  var min = array[0];
  for(int i = 1; i < array.Length; ++i)
    min = array[i].CompareTo(min) < 0 ? array[i] : min;

  return min;
}

static int Sum(int[] array)
{
  var sum = array[0];
  for(int i = 1; i < array.Length; ++i)
    sum = sum + array[i];

  return sum;
}

static int AggregateInt(Func<int, int, int> f, int[] array)
{
  var res = array[0];
  for(int i = 1; i < array.Length; ++i)
    res = f(res, array[i]);

  return res;
}

static T Aggregate<T>(Func<T, T, T> f, T[] array)
{
  var res = array[0];
  for(int i = 1; i < array.Length; ++i)
    res = f(res, array[i]);

  return res;
}

static R Reduce<T, R>(Func<T, R, R> f, T[] array)
{
  var acc = default(R);
  for(int i = 0; i < array.Length; ++i)
    acc = f(array[i], acc);
  
  return acc;
}

static R? Fold<T, R>(Func<T, R, R> reducer, T[] array, R acc)
{
  for(int i = 0; i < array.Length ; ++i) 
    acc = reducer(array[i], acc);
  
  return acc;
}
  
static R[] Map<T, R>(Func<T, R> transformer, T[] array)
{ 

  var result = new R[array.Length];
  for(int i = 0; i < array.Length ; ++i) 
  {
      result[i] = transformer(array[i]);
  }
  return result;
}

static T[] Filter<T>(Func<T, bool> predicate, T[] array)
{ 
  var size = 0;
  for(int i = 0; i < array.Length; ++i)
    size += predicate(array[i])? 1 : 0;

  var result = new T[size];
  for(int i = 0, j = 0; i < array.Length && j < size; ++i) 
  {
    if(predicate(array[i]))
      result[j++] = array[i];
  }
  return result;
}

//Recursive Fold (Tail Recursion)
static R? FoldRec<T, R>(Func<T, R, R> reducer, T[] array, int idx, R acc)
{
  if(idx == array.Length) return acc;
  return FoldRec(reducer, array, idx + 1, reducer(array[idx], acc));
}