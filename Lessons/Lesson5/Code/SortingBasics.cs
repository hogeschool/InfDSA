
public static class SortingBasics<T> where T : IComparable<T>
{
	public static void SelectionSort(T[] array) {
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
				//Swap(array, i , tmpMinIdx);
				T tmp = array[i];
				array[i] = array[tmpMinIdx];
				array[tmpMinIdx] = tmp;
			}
		}
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
					//Swap(array, i , i + 1);
					T tmp = array[i];
					array[i] = array[i + 1];
					array[i + 1] = tmp;
					swapped = true;
				}
			}	
			n--;
		}
		while(swapped); // while(n >= 2); => NOT Adaptive!
	}

	public static void MergeSort(T[] array, int low, int high)
	{
		if(array == null || array.Length <= 1) return;

		if(low >= high) return;
		
		int middle = (low + high)/2;
		MergeSort(array, low, middle);
		MergeSort(array, middle + 1, high);
		Merge(array, low, middle, high);
	
	}

	static void Merge(T[] array, int low, int middle, int high)
	{
		//left idx from low to middle
		//size: middle - low + 1
		T[] left = new T[middle - low + 1];
		//right idx from middle + 1 to high
		//size: high - (middle + 1) + 1 => high - middle, 
		//size(low, high) - size(left) = high - low + 1 - (middle - low + 1) => high - middle
		T[] right = new T[high - middle];

		//Copying elements from idx p to r (original array) into left partition 
		for(int i = 0; i < left.Length; ++i)
		{
			left[i] = array[low + i];
		}

		for(int j = 0; j < right.Length; ++j)
		{
			right[j] = array[middle + 1 + j];
		}

		//Merge
		int leftIdx = 0, rightIdx = 0;
		int arrIdx = low;
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

	public static bool isOrdered(T[] array)
	{
		bool ordered = true;
		for(int i = 0; ordered && i < array.Length - 1; ++i)
		{
			ordered = array[i].CompareTo(array[i + 1]) <= 0; // array[i] <= array[i + 1]
		}
		return ordered;
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
}