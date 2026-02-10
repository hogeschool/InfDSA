
using Model;
using View;
using Controller;
using System.Text.RegularExpressions;



class Program
{
    private static void StartMenu (Maze maze, MazeView view)
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.BackgroundColor = ConsoleColor.White;
      MenuView.DisplayMenu();
      view.DisplayMaze(maze); 
    }

    static void Main()
    {
        string mazeText = @"
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

        //-----------constants:------------
        const int rows = 25, cols = 2*rows;
        const int timeInterval = 400;
        //---------------------------------
        
        //Predefined maze:
        //Maze maze = new Maze(mazeText); //to use the string above;
        //OR
        //Maze maze = new Maze(MazeGrids.mazeText);
        //OR
        //Maze maze = new Maze(-1, -1);
        //OR
        //Maze maze = new Maze(false);

        Maze maze = new Maze(rows, cols);
        MazeView view = new MazeView();

        MenuController menuController = new MenuController(maze, view, timeInterval);
        ConsoleKey key;
        bool resp = true;

        //----Refresh for visualization reason----
        int i = 0;
        while (i <= 4)
        {
          StartMenu(maze, view);           
          i++;
        }
        //----------------------------------------

        while (resp)
        {
          StartMenu(maze, view); 
          key = Console.ReadKey(true).Key;
          resp = menuController.Run(key);
        }
    }
}

