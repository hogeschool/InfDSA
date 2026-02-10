
using System.IO.Pipes;
using Model;
using View;

namespace Controller
{
    public class MazeController
    {
        private Maze _maze;
        private MazeView _view;
        private int _timeInterval;
        private IPathFinder _pathFinder;
        private PathFinderType _algType;

        public MazeController(Maze maze, MazeView view, int timeInterval, IPathFinder pathFinder)
        {
            _maze = maze;
            _view = view;
            _pathFinder = pathFinder;
            _timeInterval = timeInterval;
        }

        public void Run()
        {
            var symbols = _view.generateSymbols(_maze.CountNotVisited());
            if(_pathFinder.algType == PathFinderType.Manual){
                _timeInterval = 0;
                var visitedPositions = new Queue<int[]>();

                var pos = _maze.Begin;

                ConsoleKey consoleKey = ConsoleKey.None;
                visitedPositions.Enqueue(pos);
                
                while (consoleKey != ConsoleKey.Q)
                { 
                    /* 
                    //moves:
                    {           
                        new int[] { 1,  0 },  //down
                        new int[] { -1, 0 },  //up
                        new int[] { 0, -1 },  //left
                        new int[] { 0,  1 },  //right
                    };
                    */

                    //_view.DisplayMaze(_maze, symbols, _timeInterval, visitedPositions, _view.PrintFunc, _algType);
                    _view.DisplayMaze(_maze, pos, symbols, visitedPositions);
                    if (pos[0] == _maze.End[0] && pos[1] ==_maze.End[1]) //completed
                    {
                        return;
                    }
                    _pathFinder.FindPath(_maze, pos, visitedPositions); 

                    consoleKey = Console.ReadKey(true).Key;
                    int[] tmppos = pos;
                    
                    switch (consoleKey)
                    {
                        case ConsoleKey.L or ConsoleKey.LeftArrow:
                            tmppos = new int[] { pos[0] + _maze.moves[2][0], pos[1] + _maze.moves[2][1] };
                            break;
                        case ConsoleKey.R or ConsoleKey.RightArrow:
                            tmppos = new int[] { pos[0] + _maze.moves[3][0], pos[1] + _maze.moves[3][1] };
                            break;
                        case ConsoleKey.U or ConsoleKey.UpArrow:
                            tmppos = new int[] { pos[0] + _maze.moves[1][0], pos[1] + _maze.moves[1][1] };
                            break;
                        case ConsoleKey.D or ConsoleKey.DownArrow:
                            tmppos = new int[] { pos[0] + _maze.moves[0][0], pos[1] + _maze.moves[0][1] };
                            break;
                        case ConsoleKey.S:  //Reset (Start again)
                            //Reset Maze
                            visitedPositions = new Queue<int[]>();
                            tmppos = _maze.Begin;
                            break;
                        case ConsoleKey.M or ConsoleKey.Backspace:  //Back to Menu
                            //Reset positions
                            visitedPositions = new Queue<int[]>();
                            return;
                        default:
                            break;
                    }

                    if (_maze.IsValidMove(tmppos[0], tmppos[1], true)){
                        pos = tmppos;
                    }
                    else {
                        _view.DisplayMaze(_maze, tmppos, symbols, visitedPositions);
                    }

                }
            }
            //Algorithms part of Controller:
            else{
                _view.DisplayMaze(_maze);
                var visitedPositions = new Queue<int[]>();
                _pathFinder.FindPath(_maze, _maze.Begin, visitedPositions);
                bool success = visitedPositions.ToList().Last()[0] == _maze.End[0] && visitedPositions.ToList().Last()[1] == _maze.End[1];
                string msg = $"\n\n{String.Join("", Enumerable.Repeat(" ", _maze.MazeMDArray.GetLength(1)/6))}";
                //_view.DisplayMaze(_maze, symbols, _timeInterval, visitedPositions);
                _view.DisplayMaze(_maze, symbols, _timeInterval, visitedPositions, _pathFinder.algType);
                _view.DisplaySuccess(success, msg, _timeInterval);
                
            }
        }

    }


}
