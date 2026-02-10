namespace Model
{
    public class DijkstraPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Dijkstra;
        public PathFinderType algType { get => _algType; set {} }

        public void FindPath(Maze maze, int[] start, Queue<int[]> visitedPositions)
        {
            if (maze.MazeArray == null || maze.MazeArray.Length == 0 || maze.MazeArray[0].Length == 0 ||
                start == null || start.Length != 2 ||
                !maze.IsValidMove(start[0], start[1]) ||
                visitedPositions.Any(_ => _[0] == start[0] && _[1] == start[1]) ||
                visitedPositions.Any(_ => _[0] == maze.End[0] && _[1] == maze.End[1]))
            {
                return;
            }

            // Distance map (Dijkstra’s dist[] array)
            var dist = new Dictionary<string, double>();
            var cameFrom = new Dictionary<string, int[]>();
            var visited = new HashSet<string>();
            var openSet = new List<int[]>(); // no priority queue

            string Key(int[] p) => $"{p[0]},{p[1]}";

            dist[Key(start)] = 0;
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                // Pick node with smallest distance
                int[] current = null;
                double minDist = double.MaxValue;
                foreach (var node in openSet)
                {
                    var key = Key(node);
                    if (dist.ContainsKey(key) && dist[key] < minDist)
                    {
                        minDist = dist[key];
                        current = node;
                    }
                }

                openSet.Remove(current);
                var currentKey = Key(current);

                // Goal check
                if (current[0] == maze.End[0] && current[1] == maze.End[1])
                {
                    // Reconstruct path
                    var tmpPath = new Queue<int[]>();
                    tmpPath.Enqueue(current);

                    while (cameFrom.ContainsKey(currentKey))
                    {
                        current = cameFrom[currentKey];
                        currentKey = Key(current);
                        tmpPath.Enqueue(current);
                    }

                    tmpPath = new Queue<int[]>(tmpPath.Reverse());

                    foreach (var p in tmpPath)
                        visitedPositions.Enqueue(p);

                    return;
                }

                visited.Add(currentKey);

                // Explore neighbors
                foreach (var move in maze.moves)
                {
                    var neighbor = new int[] { current[0] + move[0], current[1] + move[1] };
                    var neighborKey = Key(neighbor);

                    if (!maze.IsValidMove(neighbor[0], neighbor[1]) || visited.Contains(neighborKey))
                        continue;

                    double tentative = dist[currentKey] + 1; // weight = 1 per move

                    if (!dist.ContainsKey(neighborKey) || tentative < dist[neighborKey])
                    {
                        dist[neighborKey] = tentative;
                        cameFrom[neighborKey] = current;

                        if (!openSet.Any(n => n[0] == neighbor[0] && n[1] == neighbor[1]))
                            openSet.Add(neighbor);
                    }
                }
            }
        }
    }
}
