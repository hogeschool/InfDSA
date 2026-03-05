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
					Swap(array, i , i + 1);
					swapped = true;
				}
			}	
			n--;
		}
		while(swapped);//(n > 0);
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

}