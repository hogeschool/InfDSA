
using Solution;

var inf = double.PositiveInfinity;

//Dijkstra's algorithm SingleSourceShortestPath 

var g_Dijkstra = new Graph(
    new double[,]
        {
            { inf,  9 ,  7 , inf,  2 , inf,  inf },
            { inf, inf,  1 , 12 ,  8 , inf,   4  },
            { 41 , inf, inf,  5 ,  2 ,   4,  inf },
            { inf,  7 ,  1 , inf,  5 ,   3,   6  },
            {  1 , inf, inf,  5 , inf,  10,  inf },
            { inf,  4 ,  2 , inf,  7 , inf,   2  },
            {  3 ,  2 ,  9 , inf,  4 ,  13,  inf },
        }
    );


Console.WriteLine("\n----------Adjacency  matrix:----------\n");

PrintAdjacencyMatrix(g_Dijkstra.AdjacencyMatrix);    

var source = 0;
var goal = 6;

System.Console.WriteLine("\n\n ---------Dijkstra:---------");
var path = g_Dijkstra.SingleSourceShortestPath(0);
var nodes = string.Join("---", Enumerable.Range((int)'A', g_Dijkstra.AdjacencyMatrix.GetLength(0)).Select(c => (char) c).ToArray());
var prevs = string.Join("-->", path.Item2.Select(x => (char) (x < 0? '*' : 'A' + x)).ToArray());
Console.WriteLine($"  Graph nodes:  {nodes}");
Console.WriteLine($"Dijkstra dist:  {string.Join("-->", path.Item1)}");
Console.WriteLine($"Dijkstra prev:  {prevs}");
System.Console.WriteLine($"{(char)(source + 'A')} => {(char)(goal + 'A')}");
System.Console.WriteLine(string.Join("-->", g_Dijkstra.GetPath(source, goal, path.Item2).Select(c => (char) (c + 'A')).ToArray()));
System.Console.WriteLine();

System.Console.WriteLine("\n\n ---------Astar:---------");
var path_ = g_Dijkstra.SingleSourceShortestPath_Heuristic(source, goal, Heuristic);
var prevs_ = string.Join("-->", path_.Item2.Select(x => (char) (x < 0? '*' : 'A' + x)).ToArray());
System.Console.WriteLine($"{(char)(source + 'A')} => {(char)(goal + 'A')}");
Console.WriteLine($"Astar dist:  {string.Join("-->", path_.Item1)}");
Console.WriteLine($"Astar path:  {prevs_}");
System.Console.WriteLine();


static int Heuristic(int node, int goal) {
    return (int)Math.Pow(Math.Abs(node - goal), 1); 
}

static void PrintAdjacencyMatrix(double[,] res){
        var inf = double.PositiveInfinity;
        Console.Write("   ||  ");
        for(var col = 0; col < res.GetLength(1); col++) {
            var nodeCol = (char) ('A' + col);  
            Console.Write($"{nodeCol}  ||  ");
        } 

        Console.WriteLine("");
        Console.Write("----");
        for(var col = 0; col < res.GetLength(1); col++) {
            Console.Write("--------");
        }

        Console.WriteLine("");
        
        for(var row = 0; row < res.GetLength(0); row++) {
            var nodeRow = (char) ('A' + row);  
            Console.Write(nodeRow + "  ||  ");
            for(var col = 0; col < res.GetLength(1); col++) {
                if(res[row, col] == inf || res[row, col] % 10 == res[row, col])
                    Console.Write($"{res[row, col]}  ||  ");
                else
                    Console.Write($"{res[row, col]} ||  ");
            } 
            Console.WriteLine();         
        }
}






