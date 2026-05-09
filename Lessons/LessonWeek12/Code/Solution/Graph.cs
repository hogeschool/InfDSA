
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

    //Breadth First Traversal less optimal (but similar steps as in DFT)
    public string BFT(int root)
    {
        string result = "";
        // create empty queue and enqueue the root
        var nodeQueue = new Queue<int>();
        nodeQueue.Enqueue(root);

        // create array of booleans to keep track of visited nodes
        bool[] visited = new bool[AdjacencyMatrix.GetLength(0)];

        while (nodeQueue.Count > 0) // queue is not empty
        {
            // dequeue a node
            int current = nodeQueue.Dequeue();

            if (!visited[current]) {
                // add the current node (followed by a space) to the string
                result += current + " ";

                visited[current] = true; //set current node to visited

                // find neighbors of current
                List<int> neighbors = Neighbors(current);

                // enqueue all neighbors which are not visited yet
                for (int i = 0; i < neighbors.Count; i++)
                {
                    if (!visited[neighbors[i]])
                    {
                        nodeQueue.Enqueue(neighbors[i]);
                    }
                }
            }
        }

        return result;
    }

    //Breadth First Traversal optimized
    public string BFT_(int root)
    {
        string result = "";
        // create empty queue and enqueue the root
        var nodeQueue = new Queue<int>();
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
        for (int i = AdjacencyMatrix.GetLength(0) - 1; i >= 0; --i)
        {
            if (AdjacencyMatrix[node, i] < Double.PositiveInfinity)
                neighbors.Add(i);
        }
        //neighbors.Reverse(); not necessary as already implemented reversing the order in the for loop
        return neighbors;
    }
    
    //Depth First Traveral
    public string DFT(int root)
    {
        string result = "";

        // create empty stack and push the root into it
        var nodeStack = new Stack<int>();
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

                // push all neighbors if not already visited
                for (int i = 0; i < reversedNeighbors.Count; i++) {
                    if (!visited[reversedNeighbors[i]]) 
                    {
                       nodeStack.Push(reversedNeighbors[i]);
                    }
                }
            }
        }
        return result;
    }

}
