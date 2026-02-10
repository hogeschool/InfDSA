
namespace Model
{
    public class RecursivePathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Recursive;
        public PathFinderType algType { get => _algType; set {} }

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {  
            if(maze.MazeArray != null && maze.MazeArray.Length > 0 && maze.MazeArray[0].Length > 0 &&
               pos != null && pos.Length == 2 &&
               maze.IsValidMove(pos[0], pos[1]) &&
               !visitedPositions.Any(_ => _[0] == pos[0] && _[1] == pos[1]) &&
               !visitedPositions.Any(_ => _[0] == maze.End[0] && _[1] == maze.End[1])
              ) 
               
            {
            /* 
            * - BASE CASE end of the maze
            * - RECURSIVE STEP -> consider a new position
            *                     using the four possible directions              
            */

                var arrayValue = maze.MazeArray[pos[0]][pos[1]];
                visitedPositions.Enqueue(pos);
                
                if(arrayValue == 2) {
                    return;
                }

                if(arrayValue == 1 || arrayValue == 0) {
 
                    var rnd = new Random();
                    var moves = maze.moves.OrderBy(_ => rnd.Next()).ToArray();
                    foreach(var move in moves){
                        var newPos = new int[]{pos[0] + move[0],
                                               pos[1] + move[1]};
                        if(maze.IsValidMove(newPos[0], newPos[1]) && !visitedPositions.Any(_ => _[0] == newPos[0] && _[1] == newPos[1])){
                           FindPath(maze, newPos, visitedPositions);
                        }
                    }
                }
            }
        }

    }
}
