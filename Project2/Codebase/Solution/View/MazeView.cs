

using Model;

namespace View
{
    public class MazeView
    {
        static void Shift<T>(T[] array, int k)
        {
            var res = new T[array.Length];
            Array.Copy(array, res, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                array[(i + k) % array.Length] = res[i];
            }
        }

        //a b c d e      [a]     
        //e a b c d      [e a]
        //d e a b c      [d e a]

        //View
    
        public void DisplayMaze(Maze maze)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            //Console.Clear();
            var array = maze.MazeArray;
           
            Console.WriteLine("\n");

            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:
                            Console.Write("🏠");   //begin 
                            break;
                        case 2:
                            Console.Write("🍦");   //end
                            break;
                        case 0:
                            Console.Write("  ");   //not visited
                            break;
                        case 10:
                            Console.Write("🏅");   //completed
                            break;
                        case 4:
                            Console.Write("⚽️");   //visited
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            Console.WriteLine("\n");
        }

        public void DisplayMaze(Maze maze, int[] currPos)
        {
            var array = maze.MazeArray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            var rand = new Random();
            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:                    //begin 
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                                Console.Write("🏠");
                            break;
                        case 2:
                            Console.Write("🍦");    //end
                            break;
                        case 0:                     //not visited
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                                Console.Write("  ");
                            break;
                        case 10:
                            Console.Write("🏅");   //completed
                            break;
                        case 4:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                            {
                                if (rand.NextDouble() < 0.3)
                                    Console.Write("🦖");
                                else if (rand.NextDouble() >= 0.3 && rand.NextDouble() < 0.6)
                                    Console.Write("🦕");
                                else
                                    Console.Write("🐈");
                            }
                            //Console.Write("⚽️");   //visited
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            Console.WriteLine();
        }

        public void DisplayMaze(Maze maze, int[] currPos, string[] symbolsArr, Queue<int[]> visitedPositions, PathFinderType algType = PathFinderType.Manual)
        {
            var array = maze.MazeArray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            
            if(algType == PathFinderType.Manual){
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n\n{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3) )}{"  " + algType + "  "}{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3))}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
            }

            System.Console.WriteLine();

            if (visitedPositions != null && visitedPositions.Count > 0 && 
                maze.IsValidMove(currPos[0], currPos[1], true) &&
                !visitedPositions.Any(_ => _[0] == currPos[0] && _[1] == currPos[1]))//(array[currPos[0]][currPos[1]] != 4 && array[currPos[0]][currPos[1]] != 10)
                Shift(symbolsArr, 1);

            var positions = visitedPositions.ToArray();

            var visitedPoints = new Dictionary<string, string>();
            for (int idx = 0; idx < positions.Length; idx++)
            {
                visitedPoints.Add($"{positions[idx][0]}:{positions[idx][1]}", symbolsArr[idx]);
            }

            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:                    //begin 
                            //if (currPos[0] == rowIdx && currPos[1] == colIdx)
                            //    Console.Write("⚽️");
                            //else
                                Console.Write("🏠");
                            break;
                        case 2:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("🏅");    //completed
                            else
                                Console.Write("🍦");    //end     
                            break;
                        case 0:                     //not visited
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else if (visitedPositions.Any(_ => _[0] == rowIdx && _[1] == colIdx))
                                Console.Write(visitedPoints[$"{rowIdx}:{colIdx}"]);    
                            else
                                Console.Write("  ");
                            break;
                        //Marking strategy:
                        case 10:
                            Console.Write("🏅");    //completed
                            break;
                        case 4:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx) {
                                Console.Write("⚽️");
                            }
                            else
                            {
                                Console.Write(visitedPoints[$"{rowIdx}:{colIdx}"]);
                            }
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            
            if(algType == PathFinderType.Manual){
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\n👉 👉 👉        Press S to go start again.    👈 👈 👈");
                Console.WriteLine("👉 👉 👉 Press M or ⬅️  to go back to the Menu. 👈 👈 👈\n");
            }
            
            if (!maze.IsValidMove(currPos[0], currPos[1], true))
            {
                PrintWrongMove(currPos);
            }

            if (currPos[0] == maze.End[0] && currPos[1] ==maze.End[1]) //completed
            {
                //Reset Maze
                visitedPositions = new Queue<int[]>(); 
                Console.WriteLine("\n");
                Console.WriteLine("👍 DONE!!! AMAZING!!! 👍");
                Thread.Sleep(300);
                return;
                
            }
       
        }

        public void DisplayMaze(Maze maze, string[] symbolsArr, int timeInterval, Queue<int[]> visitedPositions)
        {

            var array = maze.MazeMDArray;

            var toBeShownPositions = new Queue<int[]>(visitedPositions);
            var shownPositions = new Queue<int[]>();

            while (toBeShownPositions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();

                Shift(symbolsArr, 1);

                var positions = shownPositions.ToArray();
                var visitedPoints = new Dictionary<string, string>();
                for (int idx = 0; idx < positions.Length; idx++)
                {
                    visitedPoints.Add($"{positions[idx][0]}:{positions[idx][1]}", symbolsArr[idx]);
                }

                var currPos = toBeShownPositions.Dequeue();
                shownPositions.Enqueue(currPos);

                //Marking strategy:

                // if (array[currPos[0], currPos[1]] == 2)
                //     array[currPos[0], currPos[1]] = 10;
                // else
                //     array[currPos[0], currPos[1]] = 4;


                // Loop over the elements of the maze array
                // and display as characters.
                for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
                {
                    for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                    {
                        switch (array[rowIdx, colIdx])
                        {
                            case -1:
                                Console.Write("🟦");   //walls
                                break;
                            case 1:                    //begin 
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else
                                    Console.Write("🏠");
                                break;
                            case 2:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("🏅");    //completed
                                else
                                    Console.Write("🍦");    //end                           
                                break;
                            case 0:                     //not visited or visited in a not marked array
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else if (shownPositions.Any(_ => _[0] == rowIdx && _[1] == colIdx))
                                {
                                    Console.Write("🏃");//visitedPoints[$"{rowIdx}:{colIdx}"]);
                                }
                                else
                                    Console.Write("  ");
                                break;
                            //Marking strategy 
                            case 10:
                                Console.Write("🏅");    //completed
                                break;
                            case 4:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else
                                {
                                    Console.Write("🏃");//visitedPoints[$"{rowIdx}:{colIdx}"]);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("🟦");
                }

                for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                    Console.Write("🟦");
                Console.WriteLine();

                Thread.Sleep(timeInterval);
                //Console.Clear();
            }
        }


        public void DisplayMaze(Maze maze, string[] symbolsArr, int timeInterval, Queue<int[]> visitedPositions, Func<int, int, Queue<int[]>, string[], string[,]> PrintFunc)
        {
            
            var array = maze.MazeMDArray;

            var toBeShownPositions = new Queue<int[]>(visitedPositions);
            var shownPositions = new Queue<int[]>();
            
            while (toBeShownPositions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                    
                var visitedPoints = PrintFunc(array.GetLength(0), array.GetLength(1), shownPositions, symbolsArr);
                
                var currPos = toBeShownPositions.Dequeue();

                shownPositions.Enqueue(currPos);
                //Marking strategy (drawback?)
                // if (array[currPos[0], currPos[1]] == 2)
                //     array[currPos[0], currPos[1]] = 10;
                // else 
                //     array[currPos[0], currPos[1]] = 4;

                // Loop over the elements of the maze array
                // and display as characters.
              
                for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
                {
                    for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                    {
                        switch (array[rowIdx, colIdx])
                        {
                            case -1:
                                Console.Write("🟦");   //walls
                                break;
                            case 1:                    //begin 
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else
                                    Console.Write("🏠");
                                break;
                            case 2:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("🏅");    //completed
                                else
                                    Console.Write("🍦");    //end before completion
                                break;
                            case 0:                     //not visited
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else if (shownPositions.Any(_ => _[0] == rowIdx && _[1] == colIdx))
                                    Console.Write(visitedPoints[rowIdx, colIdx]);
                                else
                                    Console.Write("  ");
                                break;
                            //Marking strategy    
                            case 10:
                                Console.Write("🏅");    //completed
                                break;
                            case 4:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else
                                {
                                Console.Write(visitedPoints[rowIdx, colIdx]);
                                }

                                //Console.Write("⚽️");   //visited
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("🟦");
                }

                for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                    Console.Write("🟦");
                Console.WriteLine();
                
                Thread.Sleep(timeInterval);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;

        }

        public void DisplayMaze(Maze maze, string[] symbolsArr, int timeInterval, Queue<int[]> visitedPositions, Func<int, int, Queue<int[]>, string[], string[,]> PrintFunc, PathFinderType algType)
        {
            
            var array = maze.MazeMDArray;

            var toBeShownPositions = new Queue<int[]>(visitedPositions);
            var shownPositions = new Queue<int[]>();
            
            while (toBeShownPositions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                    
                var visitedPoints = PrintFunc(array.GetLength(0), array.GetLength(1), shownPositions, symbolsArr);
                
                var currPos = toBeShownPositions.Dequeue();

                shownPositions.Enqueue(currPos);
                //Marking strategy (drawback?)
                // if (array[currPos[0], currPos[1]] == 2)
                //     array[currPos[0], currPos[1]] = 10;
                // else 
                //     array[currPos[0], currPos[1]] = 4;

                // Loop over the elements of the maze array
                // and display as characters.
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n{String.Concat(Enumerable.Repeat("🟨", array.GetLength(1)/2 - algType.ToString().Length/3) )}{"  " + algType + "  "}{String.Concat(Enumerable.Repeat("🟨", array.GetLength(1)/2 - algType.ToString().Length/3))}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
                for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
                {
                    for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                    {
                        switch (array[rowIdx, colIdx])
                        {
                            case -1:
                                Console.Write("🟦");   //walls
                                break;
                            case 1:                    //begin 
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else
                                    Console.Write("🏠");
                                break;
                            case 2:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("🏅");    //completed
                                else
                                    Console.Write("🍦");    //end before completion
                                break;
                            case 0:                     //not visited
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else if (shownPositions.Any(_ => _[0] == rowIdx && _[1] == colIdx))
                                    Console.Write(visitedPoints[rowIdx, colIdx]);
                                else
                                    Console.Write("  ");
                                break;
                            //Marking strategy    
                            case 10:
                                Console.Write("🏅");    //completed
                                break;
                            case 4:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else
                                {
                                Console.Write(visitedPoints[rowIdx, colIdx]);
                                }

                                //Console.Write("⚽️");   //visited
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("🟦");
                }

                for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                    Console.Write("🟦");
                Console.WriteLine();
                
                Thread.Sleep(timeInterval);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;

        }

   

        public Dictionary<string, string> PrintFunc_(Queue<int[]> shownPositions, string[] symbolsArr)
        {
            Shift(symbolsArr, 1);

            var positions = shownPositions.ToList();
            var visitedPoints = new Dictionary<string, string>();
            // var i = 0;
            // shownPositions.ToList().ForEach(_ => visitedPoints.Add($"{_[0]}:{_[1]}", symbolsArr[i++])); 
            for (int idx = 0; idx < positions.Count; idx++)
            {
                visitedPoints.Add($"{positions[idx][0]}:{positions[idx][1]}", symbolsArr[idx]);
            }

            return visitedPoints;
        }
    
        public string[,] PrintFunc(int rows, int cols, Queue<int[]> shownPositions, string[] symbolsArr)
        {

            //Shift(symbolsArr, 1);

            var positions = shownPositions.Reverse().ToList();//shownPositions.ToList();
            var visitedPoints = new string[rows, cols];
            // var i = 0;
            // shownPositions.ToList().ForEach(_ => visitedPoints.Add($"{_[0]}:{_[1]}", symbolsArr[i++])); 
            for (int idx = 0; idx < positions.Count; idx++)
            {
                visitedPoints[positions[idx][0], positions[idx][1]] = symbolsArr[idx];
            }

            return visitedPoints;
        }


        public Dictionary<string, string> PrintSyms(Queue<int[]> shownPositions, string[] symbolsArr)
        {
            var visitedPoints = new Dictionary<string, string>();
            var i = 0;
            shownPositions.ToList().ForEach(_ => visitedPoints.Add($"{_[0]}:{_[1]}", symbolsArr[i++]));
            return visitedPoints;
        }

        public string[] generateSymbols(int spaces)
        {
            var rnd = new Random(); 
            var symbols = new string[2*spaces];
            for (int i = 0; i < 2 * spaces; i++)
            {
            /*
                if(i < 10)
                    symbols[i] = ":" + i;
                else
                    symbols[i] = (i % 100) < 10 ? ":" + (i % 100) : (i % 100) + "";

            */
                if (rnd.NextDouble() < 0.3)
                {
                    symbols[i] = "🦖";
                }

                else if (rnd.NextDouble() < 0.7)
                {
                    symbols[i] = "🐈";
                }

                else
                {
                    symbols[i] = "🦕";
                }
            }

            return symbols;
        }

        private void PrintWrongMove(int[] tmppos)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Wrong Direction -> {tmppos[0]}, {tmppos[1]}");

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.White;
        
        }

    }
}
