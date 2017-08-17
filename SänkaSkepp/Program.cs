using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Program
    {
        void Main(string[] args)
        {

            Grid grid = SetGridSize();
            StartGame(grid);
            PlayGame(grid);
            EndGame();





        }

        private static void EndGame()
        {
            throw new NotImplementedException();
        }

        private void PlayGame(Grid grid)
        {
            PrintField(grid);
            DropBomb();
            throw new NotImplementedException();

        }

        private static void DropBomb()
        {
            CheckHit();
            CheckAllHit();
            throw new NotImplementedException();

        }

        private static void CheckAllHit()
        {
            throw new NotImplementedException();
        }

        private static void CheckHit()
        {
            throw new NotImplementedException();
        }

        private static void PrintField(Grid grid)
        {
            
            foreach (var square in grid.squares)
            {

            }
            throw new NotImplementedException();
        }

        private void StartGame(Grid grid)
        {

            PlaceShips(grid);
        }

        private Grid SetGridSize()
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(4 , 4); // <-- färdig att använda

            
            throw new NotImplementedException();
        }

        

        private void PlaceShips(Grid grid)
        {
            PrintField(grid);
            throw new NotImplementedException();
        }
        
    }
}
