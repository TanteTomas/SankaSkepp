using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Player
    {
        public string Name { get; private set; }
        public int Score { get; set; }
        public bool IsTurn { get; set; }
        public Grid grid;
        private string onlineFilePath;
        private bool hasReadFromFile = false;
        private string nextShot;

        public static int NumberOfPlayers = 0;

        public Player(string name , int[] gridSize)
        {
            Name = name;
            Score = 0;
            IsTurn = (NumberOfPlayers == 0);
            NumberOfPlayers++;
            grid = SetGridSize(gridSize);
            
        }


        private static Grid SetGridSize(int[] gridSize)
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(gridSize[0], gridSize[1]); // <-- färdig att använda

            return grid;
        }




        public string DropBomb(Grid grid, OnlineGame onlineGame, bool willEnterManually)
        {
            string input;
            bool enterManually = ((willEnterManually && onlineGame.PlayOnline) || !onlineGame.PlayOnline);
            while (true)
            {
                if (enterManually)
                {
                    input = GetInputFromUser.GetString("Enter coords where to fire: ").ToUpper();
                    
                    if (onlineGame.PlayOnline)
                    {
                        WriteToFile(input);
                    }

                }
            
                else
                {
                    onlineFilePath = onlineGame.OnlineFilePath;
                    GetCoordinateFromFile();
                    input = nextShot;
                }
                if (!grid.squares.ContainsKey(input))
                {
                    Console.WriteLine("This grid does not exist!");
                    continue;
                }
                else if (grid.squares[input].isHit)
                {
                    Console.WriteLine("You have already shot here!");
                    continue;
                }

                bool isHit = grid.squares[input].isShip;
                if (isHit)
                    return ReturnHit(grid, input);
                else
                    return Miss(grid, input);
                
                
            }
            /*
            Square bombDrop = new Square("Z0"); // behövs bara som Fallback
            foreach (var sq in grid.squares)
                if (sq.Key == input)
                    bombDrop = sq.Value;
            Program.CheckHit(bombDrop);
            if (Program.CheckAllHit(grid)) Program.EndGame(this.Name);
            */
        }

        private string Miss(Grid grid, string input)
        {
            grid.squares[input].isHit = true;
            return "Miss!";
        }

        private string ReturnHit(Grid grid, string input)
        {

            grid.squares[input].isHit = true;
            if (IsTheShipSunk(grid, grid.squares[input]))
            {
                SinkTheShip(grid, grid.squares[input]);
                return "Good job, you sunk the ship!";
            }
            return "Hit!";
            

        }

        private void WriteToFile(string input)
        {
            File.WriteAllText(onlineFilePath, input);
        }

        private void GetCoordinateFromFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = onlineFilePath,
                Filter = "shipSizes.txt"
            };
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Waiting for opponent to shoot");
            while (!hasReadFromFile)
            {
               
            }
            hasReadFromFile = false;
            
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            nextShot = File.ReadAllLines(onlineFilePath)[0];
            hasReadFromFile = true;
        }

        private void SinkTheShip(Grid grid, Square thisSquare)
        {
            foreach (string coord in thisSquare.belongsToShip)
            {
                grid.squares[coord].isSunk = true;
            }

            // todo: play sinking sound!
            
        }

        private bool IsTheShipSunk(Grid grid, Square thisSquare)
        {
            
            foreach (string coord in thisSquare.belongsToShip)
            {
                if (!grid.squares[coord].isHit)
                {
                    return false;
                }
            }
            return true;
            
        }
    }
}
