
using System.Diagnostics;
using System.Text;
using ToDo;

    var inf = double.PositiveInfinity;
    
    //Adjacency matrix:

    double[,] graph = {
            {inf,   3, inf,   5},
            { 2 , inf, inf, inf},
            {inf,   7, inf,   1},
            {inf, inf,   6, inf}
        };    

    //Init Test:    

    double[,] expectedDistances = {
            {  0,   3, inf,   5},
            {  2,   0, inf, inf},
            {inf,   7,   0,   1},
            {inf, inf,   6,   0}
        };

    int[,] expectedNextNodes = {
            {-1,  1, -1,  3},
            { 0, -1, -1, -1},
            {-1,  1, -1,  3},
            {-1, -1,  2, -1}
        };

    var (distances, nextNodes) = FloydWarshall.Init(graph);

    var msg = FormativeFeedback(graph, expectedDistances, expectedNextNodes, distances, nextNodes);

    var flag = MatrixEquality(expectedDistances, distances) &&
               MatrixEquality(expectedNextNodes, nextNodes);
    
    System.Console.WriteLine("\n\n-----Init Test:-----\n  ");
    System.Console.WriteLine(msg);
    System.Console.WriteLine($"Matrix Equality: {flag}\n");           
    
    //AllPairShortestPath Test:
      
    expectedDistances = new double[,] {
            { 0,  3, 11, 5},
            { 2,  0, 13, 7},
            { 9,  7,  0, 1},
            {15, 13,  6, 0}
        };

    expectedNextNodes = new int[,] {
            {-1,  1,  3,  3},
            { 0, -1,  0,  0},
            { 1,  1, -1,  3},
            { 2,  2,  2, -1}
        };


    //(distances, nextNodes) = FloydWarshall.AllPairShortestPathBottomUp(graph);
    (distances, nextNodes) = FloydWarshall.AllPairShortestPath(graph);
    (distances, nextNodes) = FloydWarshall.AllPairShortestPathTopDown(graph);
    //var (distances1, nextNodes1, prevNodes) = FloydWarshall.AllPairShortestPath_Prev(graph);
    
    var g___ = new double[,]
                {
                    { inf,  2 ,  15, inf, inf,  9 , inf,  7 , inf,  2 ,  15, inf, inf,  9 , inf,  7  },
                    {  3 , inf,  1 ,  5 ,  3 , inf,  4 , inf,  3 , inf,  1 ,  5 ,  3 , inf,  4 , inf },
                    { inf,  6 , inf,  7 , inf,  19, inf, inf, inf,  6 , inf,  7 , inf,  19, inf, inf },
                    { inf,  4 ,  1 , inf,  1 , inf, inf,  6 , inf,  4 ,  1 , inf,  1 , inf, inf,  6  },
                    {  2 , inf, inf,  -3 , inf,  23, inf, inf,  2 , inf, inf,  3 , inf,  23, inf, inf },
                    { inf,  3 , inf,  2 , 13 , inf,  12, inf, inf,  3 , inf,  2 , 13 , inf,  12, inf },
                    { inf, inf,  4 ,  3 , inf,  3 , inf,  9 , inf, inf,  4 ,  3 , inf,  3 , inf,  9  },
                    {  1 , inf, inf,  2 , 13 , inf,  2 , inf,  1 , inf, inf,  2 , 13 , inf,  2 , inf },
                    { inf,  2 ,  15, inf, inf,  9 , inf,  7 , inf,  2 ,  15, inf, inf,  9 , inf,  7  },
                    {  3 , inf,  1 ,  5 ,  3 , inf,  4 , inf,  3 , inf,  1 ,  5 ,  3 , inf,  4 , inf },
                    { inf,  6 , inf,  7 , inf,  19, inf, inf, inf,  6 , inf,  7 , inf,  19, inf, inf },
                    { inf,  4 ,  1 , inf,  1 , inf, inf,  6 , inf,  4 ,  1 , inf,  1 , inf, inf,  6  },
                    {  2 , inf, inf,  3 , inf,  23, inf, inf,  2 , inf, inf,  3 , inf,  23, inf, inf },
                    { inf,  3 , inf,  2 , 13 , inf,  12, inf, inf,  3 , inf,  2 , 13 , inf,  12, inf },
                    { inf, inf,  4 ,  3 , inf,  3 , inf,  9 , inf, inf,  4 ,  3 , inf,  3 , inf,  9  },
                    {  1 , inf, inf,  2 , 13 , inf,  2 , inf,  1 , inf, inf,  2 , 13 , inf,  2 , inf }  
                };
    var g_ = new double[,]
       {
            { inf,  2 ,  15, inf, inf,  9 , inf,  7  },
            {  3 , inf,  1 ,  5 ,  -3 , inf,  4 , inf },
            { inf,  6 , inf,  7 , inf,  19, inf, inf },
            { inf,  4 ,  1 , inf,  1 , inf, inf,  6  },
            {  2 , inf, inf,  3 , inf,  23, inf, inf },
            { inf,  3 , inf,  2 , 13 , inf,  12, inf },
            { inf, inf,  4 ,  -3 , inf,  3 , inf,  9  },
            {  1 , inf, inf,  2 , 13 , inf,  2 , inf },
       };

    var g = new double[,]
       {
            { inf,  2 ,  15, inf, inf,  9  , inf,  7 , inf},
            {  3 , inf,  1 ,  5 ,  -3 , inf,  4 , inf,  4 },
            { inf,  6 , inf,  7 , inf,  19 , inf, inf, inf},
            { inf,  4 ,  1 , inf,  1 , inf , inf,  6 , inf},
            {  2 , inf, inf,  3 , inf,  23 , inf, inf, inf},
            { inf,  3 , inf,  2 , 13 , inf ,  12, inf,  12},
            { inf, inf,  4 ,  -3 , inf,  3 , inf,  9 , inf},
            {  1 , inf, inf,  2 , 13 , inf ,  2 , inf,  2 },
            { inf, inf,  4 ,  -3 , inf,  3 , inf,  9 , inf},
       };

    double[,] g__ = {
        {inf, inf, -2 , inf},
        { 4 , inf,  3 , inf},
        {inf, inf, inf,   2},
        {inf, -1 , inf, inf}
    }; 

    Stopwatch s = new Stopwatch();
    s.Start();
    var (distances1, nextNodes1) = FloydWarshall.AllPairShortestPath(g); //BottomUp
    s.Stop();
    System.Console.WriteLine($"\nElapsed Iterative Bottom Up version {Math.Pow(g.GetLength(0), 3)}: {s.Elapsed}\n \n  ");
    s.Reset();
    Utils.SetToZero();
    s.Start();
    var (distances2, nextNodes2) = FloydWarshall.AllPairShortestPathTopDown(g);
    s.Stop();
    System.Console.WriteLine($"\nElapsed Top Down Memoized {Utils.counter}: {s.Elapsed}\n \n  ");
    s.Reset();
    Utils.SetToZero();
    s.Start();
    var (distances3, nextNodes3) = FloydWarshall.AllPairShortestPathRec(g);
    s.Stop();
    System.Console.WriteLine($"\nElapsed Recursive (definition based) {Utils.counter}: {s.Elapsed}\n \n  ");
     var (dist, next, prev) = FloydWarshall.AllPairShortestPathBottomUpNextPrev(g);
    System.Console.WriteLine(DisplayMatrix(dist));
    System.Console.WriteLine(DisplayMatrix(next));
    System.Console.WriteLine(DisplayMatrix(prev));

    msg = FormativeFeedback(graph, expectedDistances, expectedNextNodes, distances, nextNodes);
    flag = MatrixEquality(expectedDistances, distances) && 
           MatrixEquality(expectedNextNodes, nextNodes);

    System.Console.WriteLine("\n\n-----AllPairShortestPath Test:-----\n  ");       
    System.Console.WriteLine(msg);
    System.Console.WriteLine($"Matrix Equality: {flag}\n");
    System.Console.WriteLine();
    
    static string FormativeFeedback(double[,] graph, double[,] expectedDistances, int[,] expectedNextNodes, double[,] actualDistances, int[,] actualNextNodes)
    {
        StringBuilder ret = new StringBuilder();
        ret.AppendLine("Formative Feedback:\n");
        ret.AppendLine("Input: Graph");
        ret.AppendLine(DisplayMatrix(graph));

        ret.AppendLine("  --Expected Output:--\n");
        ret.AppendLine("Expected Distance");
        ret.AppendLine(DisplayMatrix(expectedDistances));

        ret.AppendLine("Expected Next");
        ret.AppendLine(DisplayMatrix(expectedNextNodes));

        ret.AppendLine("  --Actual Output:--\n"); 
        ret.AppendLine("Actual Distance");
        ret.AppendLine(DisplayMatrix(actualDistances));

        ret.AppendLine("Actual Next");
        ret.AppendLine(DisplayMatrix(actualNextNodes));
        return ret.ToString();
    }

    static string DisplayMatrix<T>(T[,] matrix)
    {
        var ret = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(0); ++i)
        {
            for (int j = 0; j < matrix.GetLength(1); ++j)
            {
                ret.Append($"{matrix[i, j]}\t");
            }
            ret.AppendLine();
        }
        return ret.ToString();
    }
    
    static bool MatrixEquality<T>(T[,] m1, T[,] m2)
    {
        if (m1 == null || m2 == null || m1.Length != m2.Length || m1.GetLength(0) != m2.GetLength(0)) return false;
        for (int i = 0; i < m1.GetLength(0); i++)
        {
            for (int j = 0; j < m1.GetLength(1); j++)
            {
                if (m1[i, j].Equals(m2[i, j]) == false) return false;
            }
        }
        return true;
    }


     