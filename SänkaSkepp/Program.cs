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

            Players players = new Players();
            StartGame(players);
            PlayGame(players);
            





        }

        public static void EndGame(string winner)
        {
            Console.WriteLine(" * * * Winner: "+winner.ToUpper()+" * * *");
            Console.ReadLine();
        }

        private static void PlayGame(Players players)
        {

            while (true) //while(not all hit)
            {
                PrintField(players.player1.grid);
                Console.WriteLine(players.player1.Name+"'s turn.");
                players.player1.DropBomb(players.player1.grid);

                PrintField(players.player2.grid);
                Console.WriteLine(players.player2.Name + "'s turn.");
                players.player2.DropBomb(players.player2.grid);
            }
        }

        private static void PlayARound(Player player)
        {
            Console.WriteLine($"{player.Name} is up. Press enter to begin");
            Console.ReadLine();
            PrintField(player.grid);
        }

        public static bool CheckAllHit(Grid grid)
        {
            bool allHit = true;
            foreach (Square square in grid.squares.Values)
                if (square.isShip && !square.isHit)
                    allHit = false;
            return allHit;
            // => continue with other player, or end game
        }

        public static void CheckHit(Square square)
        {
            if (square.isShip)
            {
                square.isHit = true;
                Console.WriteLine("You hit a ship!");
            }
            else
                Console.WriteLine("You missed...");
        }

        private static void PrintField(Grid grid)
        {
            
            foreach (var square in grid.squares)
            {

            }
            throw new NotImplementedException();
        }

        private static void StartGame(Players players)
        {

            PlaceShips(players);
        }

        
        

        private static void PlaceShips(Players players)
        {
            //PrintField(grid);
            List<int> shipSizes = PickShipSizes();

            LetsPlaceShips(shipSizes , players);
        }

        private static void LetsPlaceShips(List<int> shipSizes , Players players)
        {
            LetUserPlaceShips(shipSizes , players.player1.grid , players.player1);
            LetUserPlaceShips(shipSizes, players.player2.grid, players.player2);
        }

        private static void LetUserPlaceShips(List<int> shipSizes, Grid grid, Player player)
        {
            Console.WriteLine("Chose what grid the upper left corner of the ship should be in (on the form A1)");
            int shipLength = 2;
            foreach (int numberOfShipsOfThisSize in shipSizes)
            {
                for (int i = 0; i < numberOfShipsOfThisSize; i++)
                {
                    while (true)
                    {
                        string position = GetInputFromUser.GetString("Position: ");
                        if (!grid.squares.ContainsKey(position))
                        {
                            Console.WriteLine("This grid doesn't exist!");
                            continue;
                        }
                        if (PlaceThisShip(grid, player , position , shipLength))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("The ship cannot be here!");
                        }
                    }
                }
                shipLength++;
            }
        }

        private static bool PlaceThisShip(Grid grid, Player player , string position , int length)
        {
            List<string> shipCoords = GetShipCoords(position, length);
            foreach (string coord in shipCoords)
            {
                if (grid.squares[coord].isShip)
                {
                    return false;
                }
            }
            foreach (string coord in shipCoords)
            {
                grid.squares[coord].isShip = true;
            }
            return true;
        }

        private static List<string> GetShipCoords(string position , int length)
        {
            List<string> shipCoords = new List<string>();
            char orientation = GetInputFromUser.GetChar("Orientation (h/d/v): ");
            char row = position[0];
            int column = Convert.ToInt32(Convert.ToString(position[1]));

            for (int i = 0; i < length; i++)
            {
                shipCoords.Add(row + Convert.ToString(column));
                NextPartOfTheShip(orientation, ref row, ref column);
            }
            return shipCoords;
        }

        private static void NextPartOfTheShip(char orientation, ref char row, ref int column)
        {
            switch (orientation)
            {
                case 'h':
                    column++;
                    break;
                case 'v':
                    row++;
                    break;
                case 'd':
                    row++;
                    column++;
                    break;
                default:
                    break;
            }
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
        
    }
}
