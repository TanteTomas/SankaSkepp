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
            Grid grid = SetGridSize();
            
        }

        public void DropBomb(int coordX, int coordY)
        {
            Console.Write("Enter coords where to fire: ");
            string input = Console.ReadLine();
            Square bombDrop = new Square("Z0"); // behövs bara som Fallback
            foreach (var sq in grid.squares)
                if (sq.Key == input)
                    bombDrop = sq.Value;
            Program.CheckHit(bombDrop);
            if (Program.CheckAllHit(grid)) Program.EndGame(this.Name);
        }

    }
}
