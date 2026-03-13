

using System.Data;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();

//Stability
Console.WriteLine("\nInput Array:");
Student[] studentArray = {  new Student("Dave",'A'), 
                            new Student("Alice",'B'),
                            new Student("Ken",'A'),
                            new Student("Eric",'B'),
                            new Student("Carol",'A')
                          };
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("----------\n");
//Using Linq:
var orderedStudentArray = studentArray.OrderBy(_ => _.Name).ToArray();
System.Console.WriteLine("\n------Linq---------OrderBy(_ => _.Name)-------");
orderedStudentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n------Linq---------OrderBy(_ => _.Section)----");
orderedStudentArray = orderedStudentArray.OrderBy(_ => _.Section).ToArray();
orderedStudentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n------Linq---------OrderBy(_ => _.Name)--- => ---OrderBy(_ => _.Section)----");
orderedStudentArray = studentArray.OrderBy(_ => _.Name).OrderBy(_ => _.Section).ToArray();
orderedStudentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n------Linq---------OrderBy(_ => _.Section)--- => ---ThenBy(_ => _.Name)----");
orderedStudentArray = studentArray.OrderBy(_ => _.Section).ThenBy(_ => _.Name).ToArray();
orderedStudentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

//using SelectionSortBy:
System.Console.WriteLine("\n----selectionSortBy Name------");
Func<Student, string> selectName = a => a.Name;
Func<Student, char> selectSection= a => a.Section;
SelectionSortBy(studentArray, selectName);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

System.Console.WriteLine("\n---selectionSortBy Name -> (by) Section-------");                  
SelectionSortBy(studentArray, selectSection);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");

//using MergeSortBy:
System.Console.WriteLine("\n----MergeSortBy Name------");
Func<Student, string> selectName_ = a => a.Name;
Func<Student, char> selectSection_= a => a.Section;
OrderBy(studentArray, selectName);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

System.Console.WriteLine("\n---MergeSortBy Name -> (by) Section-------");                  
OrderBy(studentArray, selectSection);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");


System.Console.WriteLine();

int k = 4;
int n = k * 8000;
Random rnd = new Random();
bool ordered = false; //true;
int[] unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---selectionSort---");
stopwatch.Start();                  
Sorting<int>.SelectionSortBy(unorderedArray, _ => _);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---insertionSort---");

stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.InsertionSortBy(unorderedArray, _ => _);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---bubbleSort---");
stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.BubbleSortBy(unorderedArray, _ => _);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine($"\n---MergeSort--- log2(n) = {Math.Log2(n)}");
stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.MergeSort(unorderedArray, 0, unorderedArray.Length - 1);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine($"\n---Split--- log2(n) = {Math.Log2(n)}");
stopwatch.Reset(); 
stopwatch.Start();                  
Split(unorderedArray, 0, unorderedArray.Length - 1);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine($"\n---MergeSortBy--- log2(n) = {Math.Log2(n)}");
stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.MergeSortBy(unorderedArray,  _ => _);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");

static int[] GenerateArray(int n, bool ordered = true) {
  Random rnd = new Random();
  return ordered?
    Enumerable
    .Range(0, n)
    .ToArray() :
    Enumerable
    .Range(0, n)
    .Select(_ => rnd.Next(_, _ + 2000))
    .ToArray() 
    ;
}

// //                   [0...7]
// //         [0...3]            //[4...7]
// //   [0...1]      [2...3]           //[4...5]     //[6..7]
// // [0]     [1]  [2]     [3]         //[4]   //[5]    //[6] //[7]

static void Split<T>(T[] arr, int low, int high) where T : IComparable<T> {
   
   int middle = (low + high) / 2;

   if(low >= high) return;      //Base Case
  
   //Divide: 
   Split(arr, low, middle);         //LEFT PART
   Split(arr, middle + 1, high);    //RIGHT PART 
   
   //Conquer:
     //Create a Left and Right partitions from the array at the moment (from low to high)
   T[] left = new T[middle - low + 1];
   T[] right = new T[high - middle];

   for(int i = 0; i < middle - low + 1; ++i){
      left[i] = arr[low + i];
   }

   for(int j = 0; j < high - middle; ++j){
      right[j] = arr[middle + 1 + j];
   }
      
     //Combine/Merge the two partitions
   var sortedSubArray = Combine(left, right);
     //change the values of arr accordingly, from low to high
   for(int idx = 0; idx <= high - low; ++idx)
   {
      arr[low + idx] = sortedSubArray[idx];
   }
}

static T[] Combine<T>(T[] left, T[] right) where T : IComparable<T> {
    
    if(left == null || left.Length == 0) return right;
    if(right == null || right.Length == 0) return left;
    int size = left.Length + right.Length;
    T[] res = new T[size];

    int i = 0;
    int j = 0;

    for(int idx = 0; idx < res.Length; idx++) {
      if(i < left.Length && j < right.Length) {
        if(left[i].CompareTo(right[j]) <= 0){ 
          res[idx] = left[i++];
        }
        else 
          res[idx] = right[j++];
      }
      else if(j >= right.Length)
        res[idx] = left[i++];
      else 
        res[idx] = right[j++];
    }
    return res;
}


static void MergeSort<T>(T[] array, int p, int r) where T : IComparable<T>
{
  if(array == null || array.Length <= 1) return;

  if(p >= r) return;
   
  int q = (p + r)/2;
  MergeSort(array, p, q);
  MergeSort(array, q + 1, r);
  Merge(array, p, q, r); 
}

static void Merge<T>(T[] array, int p, int q, int r) where T : IComparable<T>
{
  //left idx from p to q
  //size: q - p + 1
  T[] left = new T[q - p + 1];
  //right idx from q + 1 to r
  //size: r - (q + 1) + 1 => r - q, size(p,r) - size(left) = r - p + 1 - (q - p + 1) => r - q
  T[] right = new T[r - q];

  //Copying elements from idx p to r (original array) into left partition 
  for(int i = 0; i < left.Length; ++i)
  {
    left[i] = array[p + i];
  }

  for(int j = 0; j < right.Length; ++j)
  {
    right[j] = array[q + 1 + j];
  }

  //Merge
  int leftIdx = 0, rightIdx = 0;
  int arrIdx = p;
  while( leftIdx < left.Length && rightIdx < right.Length)
  {
    if(left[leftIdx].CompareTo(right[rightIdx]) <= 0)
    {
      array[arrIdx++] = left[leftIdx];
      leftIdx++;
    }
    else
    {
      array[arrIdx++] = right[rightIdx];
      rightIdx++;
    } 
  }

  while( leftIdx < left.Length)
  {
    array[arrIdx++] = left[leftIdx];
    leftIdx++;
  } 
  
  while(rightIdx < right.Length)
  {
    array[arrIdx++] = right[rightIdx];
    rightIdx++;
  } 
  
}

static void OrderBy<T, R>(T[] array, Func<T, R> keySelector) 
         where R : IComparable<R> => MergeSortBy(array, 0, array.Length - 1, keySelector);

static void MergeSortBy<T, R>(T[] array, int p, int r, Func<T, R> keySelector) where R : IComparable<R>
{
  if(array == null || array.Length <= 1) return;

  if(p >= r) return;
   
  int q = (p + r)/2;
  MergeSortBy(array, p, q, keySelector);
  MergeSortBy(array, q + 1, r, keySelector);
  MergeBy(array, p, q, r, keySelector);
}

static void MergeBy<T, R>(T[] array, int p, int q, int r, Func<T, R> keySelector) where R : IComparable<R>
{
  //left idx from p to q
  //size: q - p + 1
  T[] left = new T[q - p + 1];
  //right idx from q + 1 to r
  //size: r - (q + 1) + 1 => r - q, size(p,r) - size(left) = r - p + 1 - (q - p + 1) => r - q
  T[] right = new T[r - q];

  //Copying elements from idx p to r (original array) into left partition 
  for(int i = 0; i < left.Length; ++i)
  {
    left[i] = array[p + i];
  }

  for(int j = 0; j < right.Length; ++j)
  {
    right[j] = array[q + 1 + j];
  }

  //Merge
  int leftIdx = 0, rightIdx = 0;
  int arrIdx = p;
  while( leftIdx < left.Length && rightIdx < right.Length)
  {
    if(keySelector(left[leftIdx]).CompareTo(keySelector(right[rightIdx])) <= 0)
    {
      array[arrIdx++] = left[leftIdx];
      leftIdx++;
    }
    else
    {
      array[arrIdx++] = right[rightIdx];
      rightIdx++;
    } 
  }

  while( leftIdx < left.Length)
  {
    array[arrIdx++] = left[leftIdx];
    leftIdx++;
  } 
  
  while(rightIdx < right.Length)
  {
    array[arrIdx++] = right[rightIdx];
    rightIdx++;
  } 
  
}

static void SelectionSort<T>(T[] array) where T : IComparable<T>{
    if(array == null || array.Length == 0) return;
    for(int i = 0; i < array.Length - 1; ++i){
      T tmpMin = array[i];
      int tmpMinIdx = i;

      for(int j = i + 1; j < array.Length; ++j)
      {
        if(array[j].CompareTo(tmpMin) < 0)
        {
          tmpMin = array[j];
          tmpMinIdx = j;
        }
      }

      if(tmpMinIdx != i) {
        T tmp = array[i];
        array[i] = array[tmpMinIdx];
        array[tmpMinIdx] = tmp;
      }
    }
}

static void BubbleSort(int[] arr) {
    if(arr== null || arr.Length <= 1) return;
    bool swapped = false;
    int n = arr.Length;
    do{
        swapped = false;
        for(int i = 0; i <= n - 2; ++i){ 
           if(arr[i] > arr[i + 1]){ 
            //----swap:-----
            var temp = arr[i + 1];
            arr[i + 1] = arr[i];
            arr[i] = temp;
            //--------------
            swapped = true;
           }

        }
        n--;
    }
    while(swapped); // while(n >= 2); => NOT Adaptive!
}

static void InsertionSort(int[] arr) {
    if(arr== null || arr.Length <= 1) return;

    for(int j = 1; j <= arr.Length - 1; ++j){
        
        int key = arr[j];
        int i = j - 1;

        while(i >= 0 && arr[i] > key){
            arr[i + 1] = arr[i];
            i--;
        }
        arr[i + 1] = key;

    }
}

static void SelectionSort_<T>(T[] array) where T : IComparable<T>{
    if(array == null || array.Length == 0) return;
    for(int i = 0; i < array.Length - 1; ++i){
      int tmpMinIdx = Min(array, i);
      if(tmpMinIdx != i) {
        Swap(array, i, tmpMinIdx);
      }
    }
}

static int Min<T>(T[] array, int begin = 0) where T : IComparable<T>
{
    if(array == null || array.Length == 0 || begin < 0 || begin >= array.Length) return -1;
    if(begin == array.Length - 1) return array.Length - 1;

    int tmpMinIdx = begin;
    T tmpMin = array[tmpMinIdx];
    for(int i = begin + 1; i < array.Length; i++){
        if (array[i].CompareTo(tmpMin) < 0){
            tmpMinIdx = i;
            tmpMin = array[i];
        }
    }
    return tmpMinIdx;
}

static void Swap<T>(T[] array, int i, int j) {
  if(array == null || array.Length == 0 ||
     i < 0 || i >= array.Length ||
     j < 0 || j >= array.Length)
     return;

  T tmp = array[i];
  array[i] = array[j];
  array[j] = tmp;
}

static int minBy<T, R>(T[] array, Func<T, R> keySelector, int begin = 0) where R : IComparable<R>
{
    if(array == null || array.Length == 0 || begin < 0 || begin >= array.Length) return -1;
    if(begin == array.Length - 1) return array.Length - 1;

    int tmpMinIdx = begin;
    R tmpMin = keySelector(array[tmpMinIdx]);
    for(int i = begin + 1; i < array.Length; i++){
      if (keySelector(array[i]).CompareTo(tmpMin) < 0){
        tmpMinIdx = i;
        tmpMin = keySelector(array[i]);
      }
    }
    return tmpMinIdx;
}

static void SelectionSortBy<T, R>(T[] array, Func<T, R> keySelector) 
    where R : IComparable<R>
{
    if(array == null || array.Length == 0) return;
    for(int i = 0; i < array.Length - 1; ++i){
       var minIdx = minBy(array, keySelector, i);
       if(i != minIdx)
         Swap(array, i, minIdx);
    }
}

//Search
static int SequentialSearch<T>(T[] array, T key) where T : IComparable<T>
{
  for(int i = 0; i < array.Length; ++i)
  {
    if(array[i].CompareTo(key) == 0)
       return i;
  }
  return -1;
}

static int SequentialSearchRec<T>(T[] array, int i, T key) where T : IComparable<T>
{
  if(array[i].CompareTo(key) == 0)
       return i;
  if(i == array.Length) return -1;
  return SequentialSearchRec(array, i + 1, key);
  
}

static int LinSearch<T>(T[] array, T key) where T : IComparable<T>
{
  if(array == null || array.Length == 0) return -1;
  
  for(int idx = 0; idx < array.Length; ++idx)
  {
    if(array[idx].CompareTo(key) == 0) return idx;
  }
  return -1;

}

static int LinSearch_<T>(T[] array, T key) where T : IComparable<T> => LinSearchRec(array, 0, key);

static int LinSearchRec<T>(T[] array, int idx, T key) where T : IComparable<T> =>
     idx == array.Length ? -1 :
     array[idx].CompareTo(key) == 0 ? idx : 
     LinSearchRec<T>(array, idx + 1, key);

static bool isOrdered_<T>(T[] array) where T : IComparable<T>
{
  for(int idx = 0; idx <= array.Length - 2; ++idx)
  {
    if(array[idx].CompareTo(array[idx + 1]) > 0) //array[idx] > array[idx + 1]
      return false;
  }
  return true;
}

static bool isOrdered<T>(T[] array) where T:IComparable<T>
{
  bool ordered = true;

  for(int i = 0; ordered && i < array.Length - 1; ++i)
  {
    ordered = array[i].CompareTo(array[i + 1]) <= 0; // array[i] <= array[i + 1]
  }

  return ordered;
}

static bool isOrderedBy<T, R>(T[] array, Func<T, R> keySelector) where R : IComparable<R>
{
  bool ordered = true;
  for(int i = 0; ordered && i < array.Length - 1; ++i)
  {
    ordered = keySelector(array[i]).CompareTo(keySelector(array[i + 1])) <= 0;
  }
  return ordered;
}

static bool isOrderedRec<T>(T[] array, int idx = 0) where T : IComparable<T>
{
  if(idx == array.Length - 1) return true;
  return array[idx].CompareTo(array[idx + 1]) <= 0 &&
         isOrderedRec(array, idx + 1);
}

static bool isOrderedTCO<T>(T[] array, int idx = 0) where T : IComparable<T>
{
  if(idx == array.Length - 1) return true;
  if(array[idx].CompareTo(array[idx + 1]) > 0) return false;
  return isOrderedTCO(array, idx + 1);
}


public record Student(string Name, char Section); 
public record Product(int Quantity, decimal Price, string Model, Guid code);

public static class Search{

  public static int BinSearch<T>(T[] array, T key) where T : IComparable<T> =>
        BinSearch<T>(array, 0, array.Length - 1, key);

  public static int BinSearch<T>(T[] array, int low, int high, T key) where T : IComparable<T>
  {
    System.Console.WriteLine($"{array[(low + high) / 2]} low:{low} high:{high} middle:{(low + high) / 2}, key: {key}");
    if(array == null || array.Length == 0 ||
      low > high) return -1;
    
    int middle = (low + high) / 2;
    if(array[middle].CompareTo(key) == 0) return middle;
    else if(array[middle].CompareTo(key) < 0) 
          return BinSearch(array, middle + 1, high, key);
    return BinSearch(array, low, middle - 1, key);
  }

  public static int BinSearchIterative<T> (T[] array, T el) where T : IComparable<T> {
    
    if(array == null || array.Length == 0) return -1;

    int low = 0;
    int high = array.Length - 1;

    while(low <= high) {
      int middle = (low + high) / 2;
      System.Console.WriteLine($"{array[(low + high) / 2]} low:{low} high:{high} middle:{(low + high) / 2}, key: {el}");
      if(array[middle].CompareTo(el) == 0) return middle;
      else if(array[middle].CompareTo(el) > 0)  // el < array[middle]
        high = middle - 1;
      else                                      // el > array[middle]
        low = middle + 1;                     
    }
    
    return -1;
  }

}







