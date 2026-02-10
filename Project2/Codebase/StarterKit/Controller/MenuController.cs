
using Model;
using View;

namespace Controller
{
    public class MenuController
    {
        private Maze _maze;
        private MazeView _view;
        private int _timeInterval;
        private int _progressBarBlocks;

        public MenuController(Maze maze, MazeView view, int timeInterval)
        {
            _maze = maze;
            _view = view;
            _timeInterval = timeInterval;
        }
        
        public bool Run(ConsoleKey key)
        {
            if(key == ConsoleKey.Q) {
                MenuView.PrintEndMessage();
                return false;
            }
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            MenuView.DisplayMenu(key);

            IPathFinder pathFinder = new ManualPathFinder();
            bool chosen = true;
            switch(key){
                case ConsoleKey.D1:
                    pathFinder = new RecursivePathFinder();
                    break;
                case ConsoleKey.D2:
                    pathFinder = new StackPathFinder();
                    break;
                case ConsoleKey.D3:
                    pathFinder = new AStarPathFinder();
                    break;
                case ConsoleKey.D4:
                    pathFinder = new DijkstraPathFinder();
                    break;   
                case ConsoleKey.P:
                    pathFinder = new ManualPathFinder();
                    break;     
                default:
                    chosen = false;
                    break;     
            }
            
            if(chosen) {
                MazeController controller = new MazeController(_maze, _view, _timeInterval, pathFinder);
                controller.Run();
                Thread.Sleep(2*_timeInterval);
                return true;
            }
            
            return true;

        }
        
    }
}