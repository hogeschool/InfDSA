namespace Solution;

public class Graph
{
    public double[,] AdjacencyMatrix { get; set; }
    public int Count { get { return AdjacencyMatrix.GetLength(0); } }

    public Graph(double[,] matrix)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
            throw new System.ArgumentException("The adjacency matrix must be a square matrix");
        AdjacencyMatrix = matrix;
    }

    //Breadth First Traversal
    public string BFT(int root)
    {
        string result = "";
        // create empty queue and enqueue the root
        var nodeQueue = new System.Collections.Generic.Queue<int>();
        nodeQueue.Enqueue(root);

        // create array of booleans to keep track of visited nodes and set the root flag to true
        bool[] visited = new bool[AdjacencyMatrix.GetLength(0)];
        visited[root] = true;

        while (nodeQueue.Count > 0) // queue is not empty
        {
            // dequeue a node
            int current = nodeQueue.Dequeue();

            // add the current node (followed by a space) to the string
            result += current + " ";

            // find neighbors of current
            List<int> neighbors = Neighbors(current);

            // enqueue all neighbors which are not visited yet and set them to visited
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (!visited[neighbors[i]])
                {
                    visited[neighbors[i]] = true;
                    nodeQueue.Enqueue(neighbors[i]);
                }
            }
        }

        return result;
    }

    //Nodes adjacent to a given node
    public List<int> Neighbors(int node)
    {
        List<int> neighbors = new List<int>();
        for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
        {
            if (AdjacencyMatrix[node, i] < Double.PositiveInfinity)
                neighbors.Add(i);
        }
        return neighbors;
    }

    //Nodes (adjacent to a given node) to be visited in reversed order
    public List<int> NeighborsReversed(int node) 
    {
        List<int> neighbors = new List<int>();
        for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
        {
            if (AdjacencyMatrix[node, i] < Double.PositiveInfinity)
                neighbors.Add(i);
        }
        neighbors.Reverse();
        return neighbors;
    }
    
    //Depth First Traveral
    public string DFT(int root)
    {
        string result = "";

        // create empty stack and push the root into it
        var nodeStack = new System.Collections.Generic.Stack<int>();
        nodeStack.Push(root);

        // create array of booleans to keep track of visited nodes
        bool[] visited = new bool[AdjacencyMatrix.GetLength(0)];

        while (nodeStack.Count > 0) // stack is not empty
        {
            // pop a node from the stack 
            int current = nodeStack.Pop();

            if (!visited[current])
            { // current node is not visited yet
              // add current node to the string (followed by a space) and set it to visited
                result += current + " ";
                visited[current] = true;

                // find neighbors (in reversed order) of current  
                List<int> reversedNeighbors = NeighborsReversed(current);

                // push all neighbors 
                for (int i = 0; i < reversedNeighbors.Count; i++)
                    nodeStack.Push(reversedNeighbors[i]);
            }
        }
        return result;
    }

    //Dijkstra's algorithm SingleSourceShortestPath, considering unvisited nodes
    public Tuple<double[], int[]> SingleSourceShortestPath(int source) //distance and prev arrays
    {
        double[] distance = new double[Count];
        int[] prev = new int[Count];
        Dictionary<int, int> unvisitedNodes = new Dictionary<int, int> (Count);
        // initialization of distance, prev and unvisitedNodes
        unvisitedNodes.Add(source, source);
        for (int i = 0; i < Count; i++)
        {
            distance[i] = double.PositiveInfinity;
            prev[i] = -1;
            if(i != source)
                unvisitedNodes.Add(i, i);
        }
        // set distance of source
        distance[source] = 0;
        while (unvisitedNodes.Count > 0) // unvisitedNodes is not empty
        {
            int firstUnvisited = unvisitedNodes.First().Key;
            double min = distance[firstUnvisited];
            int minIndex = firstUnvisited;
            //for (int i = 0; i < unvisitedNodes.Count; i++)
            foreach(var i in unvisitedNodes.Keys)
            { // find closest node in unvisitedNodes
                if (distance[unvisitedNodes[i]] < min)
                {
                    minIndex = unvisitedNodes[i];
                    min = distance[unvisitedNodes[i]];
                }
            }
            // remove the closest node from unvisitedNodes
            unvisitedNodes.Remove(minIndex);
            List<int> neighbors = Neighbors(minIndex);

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (unvisitedNodes.ContainsKey(neighbors[i]))
                { // calculate distance and update if smaller
                    double alt = distance[minIndex] + AdjacencyMatrix[minIndex, neighbors[i]];
                    if (alt < distance[neighbors[i]])
                    {
                        distance[neighbors[i]] = alt;
                        prev[neighbors[i]] = minIndex;
                    }
                }
            }
        }

        return new Tuple<double[], int[]>(distance, prev);
    }

    //Dijkstra's algorithm SingleSourceShortestPath, considering visited nodes 
    public Tuple<double[], int[]> SingleSourceShortestPath_(int source) //distance and prev arrays
    {
        double[] distance = new double[Count];
        int[] prev = new int[Count];
        Dictionary<int, int> visitedNodes = new Dictionary<int, int>();
        // initialization of distance, prev
        for (int i = 0; i < Count; i++)
        {
            distance[i] = double.PositiveInfinity;
            prev[i] = -1;
        }
        // set distance of source
        distance[source] = 0;
        while (visitedNodes.Count < Count)
        {
            //find closest node to source, NOT already visited
            double min = double.PositiveInfinity;
            int minIndex = -1;
            for (int i = 0; i < Count; i++) { 
                if (!visitedNodes.ContainsKey(i) && distance[i] < min) //NOT already visited
                {
                    minIndex = i;
                    min = distance[i];
                } 
            }              

            // add the closest node to visitedNodes
            visitedNodes.Add(minIndex, minIndex);
            List<int> neighbors = Neighbors(minIndex);

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (!visitedNodes.ContainsKey(neighbors[i]))
                { // calculate distance and update if smaller
                    double alt = distance[minIndex] + AdjacencyMatrix[minIndex, neighbors[i]];
                    if (alt < distance[neighbors[i]])
                    {
                        distance[neighbors[i]] = alt;
                        prev[neighbors[i]] = minIndex;
                    }
                }
            }
        }

        return new Tuple<double[], int[]>(distance, prev);
    }


    //Astar algorithm SingleSourceShortestPath 
    public Tuple<double, List<int>> SingleSourceShortestPath_Heuristic(int source, int goal, Func<int,int,int> h) //distance and prev arrays
    {
        double[] g_score = new double[Count];
        double[] f_score = new double[Count];
        int[] prev = new int[Count];
        Dictionary<int, int> visitedNodes = new Dictionary<int, int>();
        // initialization of g_score, f_score, prev
        for (int i = 0; i < Count; i++)
        {
            g_score[i] = double.PositiveInfinity;
            f_score[i] = double.PositiveInfinity;
            prev[i] = -1;
        }
        // set g_score (distance from source) of source
        g_score[source] = 0;
        // set f_score -> heuristic(source, goal) //distance from source to goal
        f_score[source] = h(source, goal);
        while (visitedNodes.Count < Count)
        {
            //find closest node to source, NOT already visited
            double min = double.PositiveInfinity;
            int minIndex = -1;

            for (int i = 0; i < Count; i++) { 
                if(!visitedNodes.ContainsKey(i) && f_score[i] < min)   //NOT already visited
                {
                    minIndex = i;
                    min = f_score[i];
                } 
                             
            }

            // add the closest node to visitedNodes
            visitedNodes.Add(minIndex, minIndex);
            
            if(minIndex == goal)
                return Tuple.Create(g_score[goal], GetPath(source, goal, prev));

            List<int> neighbors = Neighbors(minIndex);

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (!visitedNodes.ContainsKey(neighbors[i]))
                { // calculate g_score (distance to source) and update if smaller
                    double alt = g_score[minIndex] + AdjacencyMatrix[minIndex, neighbors[i]];
                    if (alt < g_score[neighbors[i]])
                    {
                        g_score[neighbors[i]] = alt;
                        f_score[neighbors[i]] = alt + h(neighbors[i], goal);
                        prev[neighbors[i]] = minIndex;
                    }
                }
            }
        }

        return default;
    }
    
    public List<int> GetPath(int source, int goal, int[] prev)
    {
        if (prev[goal] == -1 && source != goal)
        {
            return new List<int>(); // Return empty list
        }

        List<int> path = new List<int>();
        int current = goal;

        // Start at the target and move backwards to the source
        path.Add(goal);

        while (current != source)
        {
            current = prev[current];

            if (current == -1) return new List<int>();

            path.Add(current);
        }

        // Because we moved from target to source, we must reverse the list
        path.Reverse();

        return path;
    }

}
