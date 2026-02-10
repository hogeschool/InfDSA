
namespace Model
{
    public class StackPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Stack;
        public PathFinderType algType { get => _algType; set {} }

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            if (maze.MazeArray != null && maze.MazeArray.Length > 0 && maze.MazeArray[0].Length > 0 &&
                pos != null && pos.Length == 2 &&
                maze.IsValidMove(pos[0], pos[1])
               )
            {
                var positionsToVisit = new Stack<int[]>();
                positionsToVisit.Push(pos);

                while (positionsToVisit.Count > 0)
                {
                    var currPos = positionsToVisit.Pop();
                    if (!visitedPositions.Any(_ => _[0] == currPos[0] && _[1] == currPos[1]))
                    {
                        visitedPositions.Enqueue(currPos);
                    }

                    int arrayValue = maze.MazeArray[currPos[0]][currPos[1]];

                    //End of the maze reached!
                    if (arrayValue == 2)
                    {
                        return;
                    }
                    //not yet visited or begin
                    if (arrayValue == 0 || arrayValue == 1)
                    {
                        //var rnd = new Random();
                        //moves = moves.OrderBy(_ => rnd.Next()).ToArray();
                        foreach (var move in maze.moves)
                        {
                            var newPos = new int[] {currPos[0] + move[0],
                                                    currPos[1] + move[1]};
                            if (maze.IsValidMove(newPos[0], newPos[1]) && !visitedPositions.Any(_ => _[0] == newPos[0] && _[1] == newPos[1]))
                            {
                                positionsToVisit.Push(newPos);  //Add positions to the stack positionsToVisit

                            }
                        }
                    }
                }
            }
        }
    }
}

            

