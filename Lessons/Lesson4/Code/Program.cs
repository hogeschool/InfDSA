

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

//Using SelectionSortBy:
System.Console.WriteLine("\n----selectionSortBy Name------");
Func<Student, string> selectName = a => a.Name;
Func<Student, char> selectSection= a => a.Section;
SelectionSortBy(studentArray, selectName);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));

System.Console.WriteLine("\n---selectionSortBy Name -> (by) Section-------");                  
SelectionSortBy(studentArray, selectSection);
studentArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");




int k = 4;
int n = k * 8000;
Random rnd = new Random();
bool ordered = true;
int[] unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---selectionSort---");
stopwatch.Start();                  
Sorting<int>.SelectionSort(unorderedArray);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---insertionSort---");

stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.InsertionSort(unorderedArray);
stopwatch.Stop();
System.Console.WriteLine($"n: {n} Time elapsed: {stopwatch.Elapsed}\n");
//unorderedArray.ToList().ForEach(_ => System.Console.Write($"{_} \n"));
System.Console.WriteLine("\n");

unorderedArray = GenerateArray(n, ordered);

System.Console.WriteLine($"\n is array ordered: {isOrdered(unorderedArray)}");
System.Console.WriteLine("\n---bubbleSort---");
stopwatch.Reset(); 
stopwatch.Start();                  
Sorting<int>.BubbleSort(unorderedArray);
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
//  0   1   2   3
//{56, 45, 11, 89}
  
static void SelectionSort__<T>(T[] array) where T : IComparable<T>{
  if(array == null || array.Length <= 1) return;

  for(int startIndex = 0; startIndex <= array.Length - 2; ++startIndex)
  {
    //Find minimumIndex from startIndex

    int minimumIndex = startIndex;
    for(int idx = startIndex + 1; idx < array.Length; ++idx)
    {
      if(array[idx].CompareTo(array[minimumIndex]) < 0)
      {
        minimumIndex = idx;
      }
    }
    
    //if(minimumIndex != startIndex) => swap(array, minimumIndex, startIndex)
    if(minimumIndex != startIndex)
    {
      T tmp = array[minimumIndex];
      array[minimumIndex] = array[startIndex];
      array[startIndex] = tmp;
    }
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

static int LinSearchRec<T>(T[] array, int idx,T key) where T : IComparable<T> =>
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







