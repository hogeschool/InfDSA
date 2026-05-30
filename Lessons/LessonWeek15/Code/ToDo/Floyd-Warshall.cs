

namespace ToDo;

public class FloydWarshall
{    
    private const double inf = Double.PositiveInfinity;

    public static Tuple<double[,], int[,]> Init(double[,] graph)
    {
        //Initialize the distance and next matrices

        var totalNodes = graph.GetLength(0);
        var dist = new double[totalNodes, totalNodes];
        var next = new int[totalNodes, totalNodes];
        var prev = new int[totalNodes, totalNodes];

        for(int i = 0; i < totalNodes; ++i) {
           for(int j = 0; j < totalNodes; ++j) {

            if(i == j) {
                dist[i, j] = 0;           
            }
            else{
                dist[i, j] = graph[i, j];
            }

            if(graph[i, j] < inf){
                next[i, j] = j;
            }

            else {
                next[i, j] = -1;
            }
            
         }

        }
        return Tuple.Create(dist, next);

    }
    

    public static Tuple<double[,], int[,], int[,]> InitNextPrev(double[,] graph)
    {
        //Initialize the distance, prev and next matrices

        var totalNodes = graph.GetLength(0);
        var dist = new double[totalNodes, totalNodes];
        var next = new int[totalNodes, totalNodes];
        var prev = new int[totalNodes, totalNodes];

        for(int i = 0; i < totalNodes; ++i) {
           for(int j = 0; j < totalNodes; ++j) {

            if(i == j) {
                dist[i, j] = 0;           
            }
            else{
                dist[i, j] = graph[i, j];
            }

            if(graph[i, j] < inf){
                next[i, j] = j;
                prev[i, j] = i;
            }

            else {
                next[i, j] = -1;
                prev[i, j] = -1;
            }
            
         }

        }
        return Tuple.Create(dist, next, prev);

    }
    
    
    //Bottom-Up approach:
    //public static Tuple<double[,], int[,]> AllPairShortestPathBottomUp(double[,] graph)
    public static Tuple<double[,], int[,]> AllPairShortestPath(double[,] graph)
    {
        var inf = Double.PositiveInfinity;
        var totalNodes = graph.GetLength(0);
        var (dist, next) = Init(graph);
        
        for(int k = 0; k < totalNodes; ++k) {
            for(int i = 0; i < totalNodes; ++i) {
                for(int j = 0; j < totalNodes; ++j) {

                    if(dist[i, j] > dist[i, k] + dist[k, j] )
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                        next[i, j] = next[i, k];
                    }
                }
           }
        }

        return Tuple.Create(dist, next);
    }
    
    public static Tuple<double[,], int[,], int[,]> AllPairShortestPathBottomUpNextPrev(double[,] graph)
    {
        //Floyd-Warshall Algorithm, All Pairs Shortest Path

        var inf = Double.PositiveInfinity;
        var totalNodes = graph.GetLength(0);
        var (dist, next, prev) = InitNextPrev(graph);
        
        for(int k = 0; k < totalNodes; ++k) {
            for(int i = 0; i < totalNodes; ++i) {
                for(int j = 0; j < totalNodes; ++j) {

                    if(dist[i, j] > dist[i, k] + dist[k, j] )
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                        next[i, j] = next[i, k];
                        prev[i, j] = prev[k, j];
                    }
                }
           }
        }

        return Tuple.Create(dist, next, prev);
    }
    
    
    //using Recursive shortestPath without memoization: (time complexity = O(|V|^2 * 3^|V|)
    public static Tuple<double[,], int[,]> AllPairShortestPathRec(double[,] graph)
    {
        var inf = Double.PositiveInfinity;
        var totalNodes = graph.GetLength(0);
        var (dist, next) = Init(graph);
        for(int i = 0; i < totalNodes; ++i) {
            for(int j = 0; j < totalNodes; ++j) {
                    //using NON memoized recursive shortestPath (exponential complexity O(3^|V|))
                    var distNext = shortestPath(Tuple.Create(dist, next), i, j, graph.GetLength(0) - 1);
                    dist[i, j] = distNext.Item1;
                    next[i, j] = distNext.Item2;
            }
        }
        return Tuple.Create(dist, next);
    }


    public static Tuple<double[,], int[,]> AllPairShortestPathTopDown(double[,] graph)
    {

        var inf = Double.PositiveInfinity;
        var totalNodes = graph.GetLength(0);
        var (dist, next) = Init(graph);

        //--Memo initialization--
        var defaultDist = new double[totalNodes + 1, totalNodes, totalNodes];
        var defaultNext = new int[totalNodes + 1, totalNodes, totalNodes];
        for (int k = 0; k <= totalNodes; ++k){
            for(int i = 0; i < totalNodes; ++i) {
                for(int j = 0; j < totalNodes; ++j) {
                    if(k == 0){
                        defaultDist[k, i, j] = dist[i, j];
                        defaultNext[k, i, j] = next[i, j];
                    }
                    else{
                        defaultDist[k, i, j] = Double.NegativeInfinity;
                        defaultNext[k, i, j] = -1;
                    }

                }
            }
        }
        //-----------------------

        for(int i = 0; i < totalNodes; ++i) {
            for(int j = 0; j < totalNodes; ++j) {
                    //using memoized (top-down) shortestPath
                    var distNext = shortestPath(Tuple.Create(defaultDist, defaultNext), i, j, graph.GetLength(0) - 1);
                    //using NON memoized recursive shortestPath (exponential complexity)
                    //var distNext = shortestPath(Tuple.Create(dist, next), i, j, graph.GetLength(0) - 1);
                    dist[i, j] = distNext.Item1;
                    next[i, j] = distNext.Item2;
            }
        }
        return Tuple.Create(dist, next);
    }
    
    //Recursive without memoization (exponential complexity):
    public static Tuple<double, int> shortestPath(Tuple<double[, ], int[, ]> distNext, int i, int j, int k){

        Utils.ShowCallStack(false);

        var (dist, next) = distNext;

        if(k == -1)
            return Tuple.Create(dist[i, j], next[i, j]);

        // Recursive case: check if the path through vertex k is shorter
        var oldDist = shortestPath(distNext, i, j, k - 1).Item1;
        var newDist = shortestPath(distNext, i, k, k - 1).Item1 + shortestPath(distNext, k, j, k - 1).Item1;
        if(oldDist > newDist) {
           dist[i, j] = newDist;
           next[i, j] = next[i, k];
        }
        return Tuple.Create(dist[i, j], next[i, j]);
    }
    
    //Recursive with memoization (top-down) => double[k, i, j]
    public static Tuple<double, int> shortestPath(Tuple<double[, , ], int[, , ]> distNext, int i, int j, int k){

        Utils.ShowCallStack(false);

        var (dist, next) = distNext;

        if(dist[k + 1, i, j] != Double.NegativeInfinity) //memo can be used
            return Tuple.Create(dist[k + 1, i, j], next[k + 1, i, j]);
        
        else{
            // Recursive case: check if the path through vertex k is shorter
            var oldDist = shortestPath(distNext, i, j, k - 1).Item1;
            var newDist = shortestPath(distNext, i, k, k - 1).Item1 + shortestPath(distNext, k, j, k - 1).Item1;
            if(oldDist > newDist) {
                dist[k + 1, i, j] = newDist;
                next[k + 1, i, j] = next[k, i, k];
            }
            else{
                dist[k + 1, i, j] = oldDist;
                next[k + 1, i, j] = next[k, i, j];
            }
            
            return Tuple.Create(dist[k + 1, i, j], next[k + 1, i, j]);
        }
    }

}

