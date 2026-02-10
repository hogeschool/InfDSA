namespace Model
{
    public class DijkstraPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Dijkstra;
        public PathFinderType algType { get => _algType; set {} }
        
        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            //ToDo implement this method
            visitedPositions.Enqueue(pos); //remove this line
        }
   }
}
