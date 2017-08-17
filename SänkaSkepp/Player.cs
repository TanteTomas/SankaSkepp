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

        private static Grid SetGridSize()
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(4, 4); // <-- färdig att använda

            return grid;

        }


        public void DropBomb(int coordX, int coordY)
        {
            Program.CheckHit(1,1);
            Program.CheckAllHit();
            // IsTurn=>false
        }

    }
}
