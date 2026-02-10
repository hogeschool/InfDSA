
namespace Model
{
    public enum PathFinderType{Recursive, Stack, Astar, Dijkstra, Manual};

    public interface IPathFinder
    {
        PathFinderType algType{get; set;}
        void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions);
    }
}
