using System;
using System.Collections.Generic;
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

        public static int NumberOfPlayers = 0;

        public Player(string name)
        {
            Name = name;
            Score = 0;
            IsTurn = (NumberOfPlayers == 0);
            NumberOfPlayers++;
            grid = SetGridSize();
            
        }


    private static Grid SetGridSize()
    {
        // skapa en instans av klassen grid
        Grid grid = new Grid(10, 10); // <-- färdig att använda

        return grid;
    }




    public void DropBomb(Grid grid , OnlineGame onlineGame , bool willEnterManually)
        {
            while (true)
            {
                Console.Write("Enter coords where to fire: ");
                string input = Console.ReadLine();
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
                else if (grid.squares[input].isShip)
                {
                    Console.WriteLine("Hit!");
                    grid.squares[input].isHit = true;
                    if (IsTheShipSunk(grid , grid.squares[input]))
                    {
                        SinkTheShip(grid, grid.squares[input]);
                    }
                }
                else
                {
                    Console.WriteLine("Miss!");
                    grid.squares[input].isHit = true;
                }
                break;
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
