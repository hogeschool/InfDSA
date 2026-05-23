namespace Solution;

public static class DynamicProgramming {

    public static long FibonacciDynamic(long n, long[] storedResults = null)
    {
        Utils.ShowCallStack(false); //DO NOT comment this line of code
        
        //initialization of memo:
        if(storedResults == null)
        {
            storedResults = new long[n + 1];
            storedResults[0] = 0;
            storedResults[1] = 1;
        }

        if (n <= 1) return n;
        if (storedResults[n] == 0)
        {
            var nMinOne = FibonacciDynamic(n - 1, storedResults);
            var nMinTwo = FibonacciDynamic(n - 2, storedResults);
            storedResults[n] = nMinOne + nMinTwo;
            //equivalent using one line:
            //storedResults[n] = FibonacciDynamic(n - 1, storedResults) + FibonacciDynamic(n - 2, storedResults);
        }
        return storedResults[n];
    }

    //Not properly implemented, no actual memoization:
    public static long FibonacciDynamic_Wrong(long n, long[] storedResults = null)
    {
        Utils.ShowCallStack(false); //DO NOT comment this line of code
        
        //initialization of memo:
        if(storedResults == null)
        {
            storedResults = new long[n + 1];
            storedResults[0] = 0;
            storedResults[1] = 1;
        }
        
        if (n <= 1) return n;
        //if (storedResults[n] == 0) //Missing part to achieve memoization 
        {
            var nMinOne = FibonacciDynamic_Wrong(n - 1, storedResults);
            var nMinTwo = FibonacciDynamic_Wrong(n - 2, storedResults);
            storedResults[n] = nMinOne + nMinTwo;
            //equivalent using one line:
            //storedResults[n] = FibonacciDynamic_Wrong(n - 1, storedResults) + FibonacciDynamic_Wrong(n - 2, storedResults);
        }
        return storedResults[n];
    }

    public static long[] FibonacciBottomUp(long n, long[] memo = null){
        //initialization of memo:
        if(memo == null)
        {
            memo = new long[n + 1];
            memo[0] = 0;
            memo[1] = 1;
        }

        for(long i = 2; i < memo.Length; ++i){
            memo[i] = memo[i - 1] + memo[i - 2];
        }
        return memo;
    }

}    
