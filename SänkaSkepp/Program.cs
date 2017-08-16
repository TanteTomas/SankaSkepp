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
            throw new NotImplementedException();
            PrintField();
            DropBomb();

        }

        private static void DropBomb()
        {
            throw new NotImplementedException();
            CheckHit();
            CheckAllHit();
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
            throw new NotImplementedException();
            AskPlayerName();
            SetGridSize();

            PlaceShips();
        }

        private static void SetGridSize()
        {
            Grid grid = new Grid();
            throw new NotImplementedException();
        }

        

        private static void PlaceShips()
        {
            PrintField();
            throw new NotImplementedException();
        }

        private static void AskPlayerName()
        {
            // skapa en instans av klassen Player
            throw new NotImplementedException();
        }
    }
}
