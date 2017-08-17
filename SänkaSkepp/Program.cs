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

            Grid grid = SetGridSize();
            StartGame(grid);
            PlayGame(grid);
            EndGame();





        }

        private static void EndGame()
        {
            throw new NotImplementedException();
        }

        private static void PlayGame(Grid grid)
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

        private static void StartGame(Grid grid)
        {
            AskPlayerName();
            

            PlaceShips(grid);
        }

        private static Grid SetGridSize()
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(4 , 4); // <-- färdig att använda
            return grid;
            
        }

        

        private static void PlaceShips(Grid grid)
        {
            //PrintField(grid);
            List<int> shipSizes = PickShipSizes();

            LetUserPlaceShips(shipSizes , grid);
        }

        private static void LetUserPlaceShips(List<int> shipSizes , Grid grid)
        {

        }

        private static List<int> PickShipSizes()
        {
            List<int> shipSizes = new List<int>();
            Console.WriteLine("You will now pick the number and size of ships. Chose the number of ships of a given type, 0 for none. When you are satisfied, leave the number slot empty");
            for (int i = 2; i < 6; i++)
            {
                while (true)
                {
                    Console.Write($"Length {i}: ");
                    string input = Console.ReadLine();
                    
                    if (int.TryParse(input , out int outputInt))
                    {
                        shipSizes.Add(outputInt);
                        break;
                    }
                    else
                    {
                        if (input == "")
                        {
                            return shipSizes;
                        }
                    }
                    Console.WriteLine("Bad input!");

                }
            }
            return null;
        }

        private static void AskPlayerName()
        {
        }
    }
}
