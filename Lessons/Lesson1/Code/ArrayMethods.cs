
public static class ArrayMethods<T> where T: IComparable<T>{

    public static int Find_(T[] array, T el) => FindRec(array, el, 0);

    public static int Find(T[] array, T el){
        if(array == null || array.Length == 0) return -1;
        //int idx = 0;
        //while(idx < array.Length){
        for(int idx = 0; idx < array.Length; ++idx ){
            if(array[idx].CompareTo(el)==0) return idx;
            //idx++;
        }
        return -1; //ToDo...
    }
    //    {3, 6, 5, 0, 9, 8, 1, 4, 2 ,7}
    //idx: 0, 1, 2, 3, 4, 5, 6, 7, 8 ,9

    /*
    // swap(0, 9) {7, 6, 5, 0, 9, 8, 1, 4, 2 ,3}
    // swap(1, 8) {7, 2, 5, 0, 9, 8, 1, 4, 6, 3}
    // swap(2, 7) {7, 2, 4, 0, 9, 8, 1, 5, 6, 3}
    // swap(3, 6) {7, 2, 4, 1, 9, 8, 0, 5, 6, 3}
    // swap(4, 5) {7, 2, 4, 1, 8, 9, 0, 5, 6, 3}
    // swap(5, 4) => NO!
    */
    public static void Reverse(T[] arr)
    {
        if(arr == null || arr.Length <= 1)
            return;
        //for(int i = 0; i < arr.Length / 2; ++i) {
        for(int i = 0, j = arr.Length - 1; i < j ; ++i, --j ) {
           //Swap(arr, i, arr.Length - 1 - i);
           Swap(arr, i, j);
        //    var tmp = arr[i];
        //    arr[i] = arr[arr.Length - 1 - i];
        //    arr[arr.Length - 1 - i] = tmp;
        }

    }

    public static void Swap(T[] arr, int i, int j)
    {
        if(arr == null || arr.Length <= 1 || 
           i < 0 || i >= arr.Length || 
           j < 0 || j >= arr.Length || 
           i == j)
            return;

        var tmp = arr[i];
        arr[i] = arr[j];
        arr[j] = tmp;
    }

    public static int FindRec(T[] array, T el, int idx){
        if(array == null || array.Length == 0 || idx < 0) return -1;
        
        if(idx == array.Length) return -1;
        
        if(array[idx].CompareTo(el) == 0) return idx;
        return FindRec(array, el, idx + 1);
    }

    public static T? Max(T[] array){
        if(array == null || array.Length == 0) return default(T);
        
        var tmpMax = array[0];
        int idx = 0;
        while(idx < array.Length){
            tmpMax = array[idx].CompareTo(tmpMax) > 0 ? array[idx] : tmpMax;
            idx++;
        }
        return tmpMax;
    }

    public static T? Min(T[] array){
        if(array == null || array.Length == 0) return default(T);

        var tmpMax = array[0];
        int idx = 0;
        while(idx < array.Length){
            tmpMax = array[idx].CompareTo(tmpMax) < 0 ? array[idx] : tmpMax;
            idx++;
        }
        return tmpMax;
    }
}

public static class MDArrayMethods<T> where T: IComparable<T>{
    
    //overload for MultiDimensional arrays (Slightly easier)
    //array.GetLength(0) => number of rows
    //array.GetLength(1) => number of columns
    public static Tuple<int, int>? Find(T[,] array, T el){
        if(array == null) return default;
        for(int i = 0; i < array.GetLength(0); ++i){
            for(int j = 0; j < array.GetLength(1); ++j){
                if(array[i, j].CompareTo(el)==0) return Tuple.Create(i, j);
            }
        }
        return Tuple.Create(-1, -1);
    }
  
    //overload for jagged arrays (array of arrays) 
    public static Tuple<int, int>? Find(T[][] array, T el){
        if(array == null || array.Length == 0) return default;
        for(int i = 0; i < array.Length; ++i){
            for(int j = 0; array[i] != null && j < array[i].Length; ++j){
                if(array[i][j].CompareTo(el)==0) return Tuple.Create(i, j);
            }
        }
        return Tuple.Create(-1, -1);
    }
}

