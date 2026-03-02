

using System.Data;
using System.Diagnostics;


//Check whether an array is ordered:


int[] arr = {2, 3, 5, -8, 20, 1, 3, };

string[] stringArray = {"apple", 
                        "bike", 
                        "bike",
                        "book", 
                        "bread",
                        "cheese", 
                        "mozzarella",
                        "soup", 
                        "zero"};


System.Console.WriteLine(LinSearch(stringArray, "hello"));
System.Console.WriteLine(LinSearch(stringArray, "apple"));
System.Console.WriteLine(isOrdered(arr));
System.Console.WriteLine(isOrdered(stringArray));

System.Console.WriteLine(isOrderedRec(arr));
System.Console.WriteLine(isOrderedRec(stringArray));

System.Console.WriteLine(isOrderedTCO(arr));
System.Console.WriteLine(isOrderedTCO(stringArray));

System.Console.WriteLine(Search.BinSearch(stringArray, "hello"));
System.Console.WriteLine(Search.BinSearch(stringArray, "book"));

int n = 900;
Random rnd = new Random();
int[] orderedArray = Enumerable
    .Range(0, n)
    .Select(_ => rnd.Next(_, _ + 2000))
    .ToArray();
Array.Sort(orderedArray);

System.Console.WriteLine($"\nLooking for the first element (key=array[0]): log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearch(orderedArray, orderedArray[0]));
System.Console.WriteLine($"\nLooking for the last element: log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearch(orderedArray, orderedArray[orderedArray.Length - 1]));
System.Console.WriteLine($"\nLooking for a non existing key: log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearch(orderedArray, -20));


System.Console.WriteLine($"\nLooking for the first element (key=array[0]): log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearchIterative(orderedArray, orderedArray[0]));
System.Console.WriteLine($"\nLooking for the last element: log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearchIterative(orderedArray, orderedArray[orderedArray.Length - 1]));
System.Console.WriteLine($"\nLooking for a non existing key: log2({n})={Math.Log2(n)}");
System.Console.WriteLine(Search.BinSearchIterative(orderedArray, -20));

System.Console.WriteLine();

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