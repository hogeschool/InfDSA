
using Solution;

//Traversal

var inf = double.PositiveInfinity;
Graph g = new Graph(
    new double[,]
        {
            { inf,  3 ,  3 , inf },
            { inf, inf, inf,  2  },
            {  4 , inf, inf, inf },
            { inf, inf,   5, inf },
        }
    );


//Depth First Traveral
var dft = String.Join(" -> ", g.DFT(0).
                                ToArray().
                                Select(c => c != ' ' ? (char) (Convert.ToInt32(c + "") + 'A') : ' ').
                                Where(c => c != ' ')); 
//Breadth First Traversal
var bft = String.Join(" -> ", g.BFT(0).
                                ToArray().
                                Select(c => c != ' ' ? (char) (Convert.ToInt32(c + "") + 'A') : ' ').
                                Where(c => c != ' ')); 

Console.WriteLine("\n---------Adjacency matrix:----------\n");

PrintAdjacencyMatrix(g.AdjacencyMatrix);

System.Console.WriteLine("\n\n ---------Traversal:---------");
Console.WriteLine($"Depth First Traversal {dft}");
Console.WriteLine($"Breadth First Traversal {bft}");

System.Console.WriteLine();

static void PrintAdjacencyMatrix(double[,] res){
        
        Console.Write("  || ");
        for(var col = 0; col < res.GetLength(1); col++) {
            var nodeCol = (char) ('A' + col);  
            Console.Write($"{nodeCol} || ");
        } 

        Console.WriteLine("");
        Console.Write("----");
        for(var col = 0; col < res.GetLength(1); col++) {
            Console.Write("-----");
        }

        Console.WriteLine("");
        
        for(var row = 0; row < res.GetLength(0); row++) {
            var nodeRow = (char) ('A' + row);  
            Console.Write(nodeRow + " || ");
            for(var col = 0; col < res.GetLength(1); col++) {
                Console.Write($"{res[row, col]} || ");
            } 
            Console.WriteLine();         
        }
    }