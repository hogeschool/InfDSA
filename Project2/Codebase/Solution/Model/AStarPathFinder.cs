
namespace Model
{
    public class AStarPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Astar;
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

            var openSet = new PriorityQueue<int[], double>();
            var cameFrom = new Dictionary<string, int[]>();
            var gScore = new Dictionary<string, double>();
            var visited = new HashSet<string>();

            string Key(int[] p) => $"{p[0]},{p[1]}";
            double Heuristic(int[] a, int[] b) =>
                Math.Sqrt(Math.Pow(a[0] - b[0], 2) + Math.Pow(a[1] - b[1], 2));

            openSet.Enqueue(pos, 0);
            gScore[Key(pos)] = 0;

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                var currentKey = Key(current);

                if (current[0] == maze.End[0] && current[1] == maze.End[1])
                {
                    // Reconstruct path and store in visitedPositions
                    //array[current[0]][current[1]] = 10;
                    var tmp_visitedPoints = new Queue<int[]>();
                    tmp_visitedPoints.Enqueue(current);
                    while (cameFrom.ContainsKey(currentKey))
                    {
                        current = cameFrom[currentKey];
                        currentKey = Key(current);
                        tmp_visitedPoints.Enqueue(current);
                        //array[current[0]][current[1]] = 4;
                    }

                    tmp_visitedPoints = new Queue<int[]>(tmp_visitedPoints.Reverse());
                    
                    foreach(var p in tmp_visitedPoints)
                        visitedPositions.Enqueue(p);

                    return;
                }

                visited.Add(currentKey);

                foreach (var move in maze.moves)
                {
                    var neighbor = new int[] { current[0] + move[0], current[1] + move[1] };
                    var neighborKey = Key(neighbor);

                    if (!maze.IsValidMove(neighbor[0], neighbor[1]) || visited.Contains(neighborKey))
                        continue;

                    double tentativeG = gScore[currentKey] + 1;

                    if (!gScore.ContainsKey(neighborKey) || tentativeG < gScore[neighborKey])
                    {
                        cameFrom[neighborKey] = current;
                        gScore[neighborKey] = tentativeG;
                        double fScore = tentativeG + Heuristic(neighbor, maze.End);
                        openSet.Enqueue(neighbor, fScore);
                    }
                }
            }
        }

    }
}

            

