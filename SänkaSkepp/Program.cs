using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StartGame();
            PlayGame();
            EndGame();





        }

        private static void EndGame()
        {
            throw new NotImplementedException();
        }

        private static void PlayGame()
        {
            PrintField();
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

        private static void PrintField()
        {
            throw new NotImplementedException();
        }

        private static void StartGame()
        {
            AskPlayerName();
            SetGridSize();

            PlaceShips();
            throw new NotImplementedException();

        }

        private static void SetGridSize()
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(); // <-- färdig att använda

            
            throw new NotImplementedException();
        }

        

        private static void PlaceShips()
        {
            PrintField();
            throw new NotImplementedException();
        }

        private static void AskPlayerName()
        {
            throw new NotImplementedException();
        }
    }
}
