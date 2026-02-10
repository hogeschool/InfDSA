
using System.Data;

namespace Model
{
    public class Maze
    {
        public int[][] MazeArray { get; private set; }
        public int[,] MazeMDArray { get; private set; }
        public int[] Begin { get; private set; }
        public int[] End { get; private set; }

        public readonly int[][] moves = {           
            new int[] {  1,  0 },  //down
            new int[] { -1,  0 },  //up
            new int[] {  0, -1 },  //left
            new int[] {  0,  1 },  //right
            };
        
        public Maze() => GenerateMaze();
        public Maze(bool automatic = true) {if(automatic) GenerateMaze(); else GenerateFromText(MazeGrids.mazeText);}
        public Maze(int rows, int cols) {if(rows <= 0 && cols <= 0) GenerateFromText(MazeGrids.mazeText); else GenerateMaze(rows, cols);}
        public Maze(string lines) => GenerateFromText(lines);

        void GenerateFromText(string lines){
            MazeArray = ToMazeArray(lines);
            MazeMDArray = ToMazeMDArray(lines);
        }

        void GenerateMaze(int rows = 20, int cols = 40)
        {
            if(rows < 4 || cols < 4) {rows = 20; cols = 40;}
            if(rows % 2 != 0) {rows++;}
            if(cols % 2 != 0) {cols++;}

            int[,] mazeMD = new int[rows, cols];
            int[][] maze = new int[rows][];

            // Initialize all cells as walls
            for (int r = 0; r < rows; r++){ 
                maze[r] = new int[cols];
                for (int c = 0; c < cols; c++) {
                    mazeMD[r, c] = -1;
                    maze[r][c] = -1;
                }
            }
            // Carve passages using Binary Tree algorithm
            Random rand = new Random();
            try{
            for (int r = 1; r < rows; r+=2)
            {
                for (int c = 1; c < cols; c+=2)
                {
                    mazeMD[r, c] = 0; // empty cell
                    maze[r][c] = 0; // empty cell

                    // Decide whether to carve South or East
                    bool carveSouth = rand.NextDouble() >= 0.5;

                    if (r == rows - 1) carveSouth = false; // last row → carve east
                    if (c == cols - 1) carveSouth = true; // rightmost col → carve south

                    if (carveSouth)
                    {
                        if(r + 1 < rows) {
                            mazeMD[r + 1, c] = 0; // carve south
                            maze[r + 1][c] = 0; // carve south
                        }
                    }
                    else
                    {
                        if (c + 1 < cols){
                            mazeMD[r, c + 1] = 0; // carve east
                            maze[r][c + 1] = 0; // carve east
                        }
                    }
                }
            }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            // Place End at bottom-right carved cell
            int endRow = rows - 2;
            int endCol = cols - 2;

            // Place Start and End
            this.Begin = [1, 1];
            this.End = [endRow, endCol];
            mazeMD[Begin[0], Begin[1]] = 1; // Begin
            mazeMD[End[0], End[1]] = 2; // End
            maze[Begin[0]][Begin[1]] = 1; // Begin
            maze[End[0]][End[1]] = 2; // End
 
            this.MazeMDArray = mazeMD;
            this.MazeArray = maze;
        }

        int[][] ToMazeArray(string maze)
        {
            // substrings from the maze string
            var arrayLines = maze.Split(new char[] { '.', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            int[][] outArray = new int[arrayLines.Length][];

            for (var rowIdx = 0; rowIdx < arrayLines.Length; rowIdx++)
            {
                var line = arrayLines[rowIdx];
                // row array:
                var row = new int[line.Length];
                for (int colIdx = 0; colIdx < line.Length; colIdx++)
                {
                    //from chars to integers
                    switch (line[colIdx])
                    {
                        case 'x':
                            row[colIdx] = -1;  //walls
                            break;
                        case '1':
                            row[colIdx] = 1;   //begin
                            Begin = [rowIdx, colIdx];
                            break;
                        case '2':
                            row[colIdx] = 2;   //end 
                            End = [rowIdx, colIdx];
                            break;
                        default:
                            row[colIdx] = 0;   //not visited
                            break;
                    }
                }
                // row in the output jagged array.
                outArray[rowIdx] = row;
            }

            return outArray;
            
        }

        int[,] ToMazeMDArray(string maze)
        {
            // substrings from the maze string
            var arrayLines = maze.Split(new char[] { '.', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            var lineLength = 0;
            if (arrayLines != null && arrayLines.Length > 0)
                lineLength = arrayLines[0].Length;
            else
            throw new Exception($"Maze incorrect");
            
            for (var rowIdx = 0; arrayLines != null && rowIdx < arrayLines.Length; rowIdx++)
            {
                var line = arrayLines[rowIdx];
                if (arrayLines[rowIdx] == null || line.Length != lineLength)
                    throw new Exception($"Not same line length for rows in maze:\n at row 0: {lineLength}, at row {rowIdx}: {line.Length}");
            }
            
            int[,] outArray = new int[arrayLines.Length, lineLength];

            for (var rowIdx = 0; rowIdx < arrayLines.Length; rowIdx++)
            {
                var line = arrayLines[rowIdx];

                for (int colIdx = 0; colIdx < line.Length; colIdx++)
                {
                    //from chars to integers
                    switch (line[colIdx])
                    {
                        case 'x':
                            outArray[rowIdx, colIdx] = -1;  //walls
                            break;
                        case '1':
                            outArray[rowIdx, colIdx] = 1;   //begin
                            Begin = [rowIdx, colIdx];
                            break;
                        case '2':
                            outArray[rowIdx, colIdx] = 2;   //end 
                            End = [rowIdx, colIdx];
                            break;
                        default:
                            outArray[rowIdx, colIdx] = 0;   //not visited
                            break;
                    }
                }
            }
            return outArray;
        }

        static int CountNotVisited(int[][] maze)
        {
            int cnt = 0;
            if (maze != null && maze.Length > 0)
            {
                for (int rowIdx = 0; rowIdx < maze.Length; rowIdx++)
                {
                    for (int colIdx = 0; maze[rowIdx] != null && colIdx < maze[rowIdx].Length; colIdx++)
                    {
                        cnt = maze[rowIdx][colIdx] == 0 ? cnt + 1 : cnt;
                    }
                }
            }
            return cnt;
        }

        public int CountNotVisited() => CountNotVisited(MazeArray);

        static bool IsValidPos(int[][] array, int newRow, int newColumn)
        {
            // ... Ensure position is within the array bounds.
            /*
            if (newRow < 0) return false;
            if (newColumn < 0) return false;
            if (newRow >= array.Length) return false;
            if (newColumn >= array[newRow].Length) return false;
            return true;
            */
            return !(newRow < 0)
                    && !(newColumn < 0)
                    && !(newRow >= array.Length)
                    && !(newColumn >= array[newRow].Length);
        }
        
        // Make sure the position is within the maze array bounds.
        // no walls
        public bool IsValidMove(int newRow, int newColumn) => 
            IsValidPos(MazeArray, newRow, newColumn) &&
            !(MazeArray[newRow][newColumn] == -1); //no walls 

        //Marking strategy
        public bool IsValidMove(int newRow, int newColumn, bool notVisited = true)
        {
            // Make sure the position is within the maze array bounds.
            // no walls, not yet visited ? (flag notVisited: false)
            return notVisited ?
                    IsValidPos(MazeArray, newRow, newColumn) &&
                    !(MazeArray[newRow][newColumn] == -1)  //no walls, but already visited -> ok
                    :
                    IsValidPos(MazeArray, newRow, newColumn) &&
                    !(MazeArray[newRow][newColumn] == -1 || MazeArray[newRow][newColumn] == 4); //no walls, not yet visited 
        }
        
    }

    public static class MazeGrids
    {
      public static string mazeText = @"
xxxxxx1xxxxxxxxxxxxxxxxxxxxxxx.
 x   x   x                    .
xx2x xxx   x xxxxxxxx    x xx .
x  x xxxxxxx xxxxxxxxxxxxx xxx.
 x x xx      x                .
x  x xx xxxxx  x xxxx xxxxx  x.
xx    x xxx   xx xxx  xxx   xx.
xxx   xxx   x xxxx   xx   x xx.
xx     xx   x xxxx   xx   x xx.
xxxx    xxxxx xx xxxx xxxxx xx.
xx            xx            xx.";
    }
}
