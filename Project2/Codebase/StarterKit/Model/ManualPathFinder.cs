
namespace Model
{
    public class ManualPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Manual;
        public PathFinderType algType { get => _algType; set {} }

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            if (maze.MazeArray == null || maze.MazeArray.Length == 0 || maze.MazeArray[0].Length == 0 ||
                pos == null || pos.Length != 2 ||
                !maze.IsValidMove(pos[0], pos[1]) ||
                visitedPositions.Any(_ => _[0] == pos[0] && _[1] == pos[1]) ||
                visitedPositions.Any(_ => _[0] == maze.End[0] && _[1] == maze.End[1])
               )
            {
                return;
            }

            visitedPositions.Enqueue(pos);
            
            return;

        }

    }
}

            

