

using System.Data;
using System.Diagnostics;


//Problem: compute the sum of the first n integer numbers
//for instance n: 6
// sum = 1 + 2 + 3 + 4 + 5 + 6

Stopwatch stopwatch_ = new Stopwatch();

long sum = 0;
long num = 600;
num = 900000000;
num = 2590000000;
stopwatch_.Start();


for(long i = 1; i <= num; i++)
 sum = sum + i;

/*
long j = 1;
while(j <= num)
{
  sum += j;
  j++;
} 
*/

stopwatch_.Stop();
System.Console.WriteLine($"Loop n: {num} result: {sum}, Time elapsed: {stopwatch_.Elapsed}\n");
System.Console.WriteLine();

var sum_6 = 1 + 2 + 3 + 4 + 5 + 6;
/* 
//          6 + 5 + 4 + 3 + 2 + 1 = sum_6
//          7 + 7 + 7 + 7 + 7 + 7 = 2 * sum_6 = 6 * (6 + 1)

//          2 * sum_6 = 6 * (6 + 1)

//          sum_6 = 6 * (6 + 1) / 2

//          sum_n = n * (n + 1) / 2
*/



stopwatch_.Reset();
stopwatch_.Start();
sum = num * (num + 1) / 2;
stopwatch_.Stop();
System.Console.WriteLine($"Formula n: {num} result: {sum}, Time elapsed: {stopwatch_.Elapsed}\n");

System.Console.WriteLine();
//Excel sheet complexity


//Divide and Conquer (Extra: not treated during lesson)
stopwatch_.Reset();
stopwatch_.Start();

sum = sumRecDC(num);
stopwatch_.Stop();
System.Console.WriteLine($"-SumRec n: {num} result: {sum}, Time elapsed: {stopwatch_.Elapsed}\n");

System.Console.WriteLine();


// Arrays introduction: characteristics (slides)
//indices:   0  1  2   3  4   5  6  7
int[] arr = {2, 3, 5, -8, 20, 1, 3, };
//new int[]{2, 3, 5, -8, 20, 1, 3};
//new int[7]{2, 3, 5, -8, 20, 1, 3};

var strArray = new string[]{"Ciao", "Buongiorno", "ADomani", "Arrivederci"};

ArrayMethods<int>.Reverse(arr);
System.Console.WriteLine();

var idx = ArrayMethods<int>.Find(arr, -8);
var strIdx = ArrayMethods<string>.Find(strArray, "hello");
strIdx = ArrayMethods<string>.Find(strArray, "ADomani");
strIdx = ArrayMethods<string>.FindRec(strArray, "ADomani", 0);
var max = ArrayMethods<int>.Max(arr);
var min = ArrayMethods<int>.Min(arr);




// Jagged Arrays/Multidimensional arrays introduction: characteristics


int[][] jaggedArray1 = {//new int[5][]{
  new int[] {2, 3, 5, -8, 20, 1, 3},
  new int[] {3, 4, 1, 20, 100},
  new int[] {},
  null,
  arr
};

string[][] jaggedArray2 = {
  new string[] {"3", "4", "1", "20", "100"},
  new string[] {},
  new string[] {"325", "10"},
  null!,
};

//Multidimensional array
int[,] mdArr = new int[3, 3]{
                             {3, 20, 100},  
                             {13, 40, 10}, 
                             {230, 25, 51} 
                             };
//new int[3, 3]{{3, 20, 100},  {13, 40, 10}, {230, 25, 51} };


var idx1 = MDArrayMethods<int>.Find(mdArr, 40);
var idx2 = MDArrayMethods<int>.Find(jaggedArray1, 40);
var idx3 = MDArrayMethods<int>.Find(jaggedArray1, -8);
var idx4 = MDArrayMethods<string>.Find(jaggedArray2, "325");

System.Console.WriteLine();


static long sumRecDC(long n)
{ System.Console.WriteLine(n);
  if (n < 0) return -1; //fallback
  
  if (n <= 1) return n;
  
  return n % 2 != 0 ? (n/2 + 1)*(n/2 + 1) + 2 * sumRecDC(n/2) : (n/2) * (n/2) + 2 * sumRecDC(n/2);
  
}

static long sumRecStd(long n) //=> n < 0 ? -1 : n == 1 ? 1 : n + sumRecStd(n - 1);
{
  //if (n <= 0) return n;
  if (n == 1) return 1;
  return n + sumRecStd(n - 1);
  
}




 


