public static class Sorting<T> where T : IComparable<T>
{
	public static void SelectionSort(T[] array)
	{
		if(array == null || array.Length <= 1) return;

		for(int i = 0; i < array.Length - 1; ++i)
		{
			//Find idx of minimum starting from i:
			int minIdx = Min(array, i); 
			if(minIdx != i)
			  Swap(array, i, minIdx);
		}
	}

	public static void SelectionSortBy<R>(T[] array, Func<T, R> keySelector) 
		where R : IComparable<R>
	{
		if(array == null || array.Length <= 1) return;
		for(int i = 0; i < array.Length - 1; ++i){
		var minIdx = minBy(array, keySelector, i);
		if(i != minIdx)
			Swap(array, i, minIdx);
		}
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

	public static void InsertionSort(T[] array)
	{
		if(array == null || array.Length <= 1) return;

		for(int i = 1; i < array.Length; ++i)
		{
			T key = array[i];
			int j = i - 1;
			while(j >= 0 && array[j].CompareTo(key) > 0)
			{
				array[j + 1] = array[j];
				j--;
			}
			array[j + 1] = key;
		}
	}

	public static void InsertionSortBy<R>(T[] array, Func<T, R> keySelector) where R : IComparable<R>
	{
		if(array == null || array.Length <= 1) return;

		for(int i = 1; i < array.Length; ++i)
		{
			T key = array[i];
			int j = i - 1;
			while(j >= 0 && keySelector(array[j]).CompareTo(keySelector(key)) > 0)
			{
				array[j + 1] = array[j];
				j--;
			}
			array[j + 1] = key;
		}
	}

	public static void BubbleSort(T[] array)
	{
		if(array == null || array.Length <= 1) return;
		bool swapped;
        
		int n = array.Length;

		do
		{
		  swapped = false;
		  for(int i = 0; i <= n - 2; ++i)
			{
				if(array[i].CompareTo(array[i + 1]) > 0)
				{
					Swap(array, i , i + 1);
					swapped = true;
				}
			}	
			n--;
		}
		while(swapped); // while(n >= 2); => NOT Adaptive!
	}

    public static void BubbleSortBy<R>(T[] array, Func<T, R> keySelector) where R : IComparable<R>
	{
		if(array == null || array.Length <= 1) return;
		bool swapped;
        
		int n = array.Length;

		do
		{
		  swapped = false;
		  for(int i = 0; i <= n - 2; ++i)
	      {
			if(keySelector(array[i]).CompareTo(keySelector(array[i + 1])) > 0)
			{
				Swap(array, i , i + 1);
				swapped = true;
			}
		  }	
		  n--;
		}
		while(swapped); // while(n >= 2); => NOT Adaptive!
	}
 
	public static void MergeSort(T[] array, int p, int r)
	{
		if(array == null || array.Length <= 1) return;

		if(p >= r) return;
		
		int q = (p + r)/2;
		MergeSort(array, p, q);
		MergeSort(array, q + 1, r);
		Merge(array, p, q, r);
	
	}

	static void Merge(T[] array, int p, int q, int r)
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
	
	public static void Swap(T[] array, int i, int j)
	{
		if(array == null || array.Length <= 1 || 
		    i < 0 || i >= array.Length ||
			j < 0 || j >= array.Length ||
			array[i].CompareTo(array[j]) == 0)
			return;

			T tmp = array[i];
			array[i] = array[j];
			array[j] = tmp;
	}

	public static int Min(T[] array, int start = 0)
	{
		if(array == null || array.Length <= 1 || 
		   start < 0 || start >= array.Length)
			 return -1;
		if(start == array.Length - 1) return array.Length - 1;

		int minIdx = start;
		T tmpMin = array[minIdx];
		for(int i = start + 1; i < array.Length; ++i)
		{
			if(array[i].CompareTo(tmpMin) < 0)
			{
				tmpMin = array[i];
				minIdx = i;
			}
		}
		return minIdx;
	}

	public static void MergeSortBy<R>(T[] array, Func<T, R> keySelector) 
         where R : IComparable<R> => MergeSortBy(array, 0, array.Length - 1, keySelector);

	public static void MergeSortBy<R>(T[] array, int p, int r, Func<T, R> keySelector) where R : IComparable<R>
	{
		if(array == null || array.Length <= 1) return;

		if(p >= r) return;
		
		int q = (p + r)/2;
		MergeSortBy(array, p, q, keySelector);
		MergeSortBy(array, q + 1, r,keySelector);
		MergeBy(array, p, q, r, keySelector);
	}

	static void MergeBy<R>(T[] array, int p, int q, int r, Func<T, R> keySelector) where R : IComparable<R>
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

	public static bool isOrdered(T[] array)
	{
		bool ordered = true;
		for(int i = 0; ordered && i < array.Length - 1; ++i)
		{
			ordered = array[i].CompareTo(array[i + 1]) <= 0; // array[i] <= array[i + 1]
		}
		return ordered;
	}


	public static bool isOrderedBy<R>(T[] array, Func<T, R> keySelector) where R : IComparable<R>
	{
		bool ordered = true;
		for(int i = 0; ordered && i < array.Length - 1; ++i)
		{
			ordered = keySelector(array[i]).CompareTo(keySelector(array[i + 1])) <= 0;
		}
		return ordered;
	}

}