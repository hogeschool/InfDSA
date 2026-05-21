namespace Solution;

public static class DynamicProgramming {

    public static long FibonacciDynamic(long n, long[] storedResults)
    {
        Utils.ShowCallStack(false); //DO NOT comment this line of code

        if (n == 0) return 0;
        if (storedResults[n] == 0)
        {
            var nMinOne = FibonacciDynamic(n - 1, storedResults);
            var nMinTwo = FibonacciDynamic(n - 2, storedResults);
            storedResults[n] = nMinOne + nMinTwo;
        }
        return storedResults[n];
    }
}    
