using System;
using System.Collections.Generic;
using System.IO;
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
            OnlineGame onlineGame = new OnlineGame(); //Otydligt i spelet hur en Dropbox Path ska skrivas (ge exempel!)
            onlineGame.SetUpOnlineGame();
            StartGame(players , onlineGame);
            PlayGame(players , onlineGame);
            





        }

        public static void EndGame(Player player)
        {
            // todo: fixa lite flashigare output
            Console.WriteLine(" * * * Winner: "+player.Name.ToUpper()+" * * *");
            Console.ReadLine();

        }

        private static void PlayGame(Players players , OnlineGame onlineGame)
        {

            while (true) //while(not all hit)
            {
                
                PlayOneTurn(players.player1 , players.player2, onlineGame);
                if (IsGameOver(players.player2))
                {
                    EndGame(players.player1);
                    break;
                }
                PlayOneTurn(players.player2, players.player1, onlineGame);
                if (IsGameOver(players.player1))
                {
                    EndGame(players.player2);
                    break;
                }
            }
        }

        private static void PlayOneTurn(Player player , Player opponent, OnlineGame onlineGame)
        {
            
            while (true)
            {
                PrintField(opponent.grid, false);
                Console.WriteLine(player.Name + "'s turn.");
                Message message = player.DropBomb(opponent.grid, onlineGame, onlineGame.IsHost);
                PrintField(opponent.grid, false);
                message.player.PlaySync();
                Console.WriteLine(message.text);
                

                if (!message.shootAgain)
                {
                    Console.WriteLine("Press enter for next turn");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    if (IsGameOver(opponent))
                    {
                        EndGame(player);
                        Environment.Exit(0);
                    }
                    
                }
                Console.WriteLine("You hit a ship! Shoot again. Press enter");
                Console.ReadLine();
            }

            
        }

        private static bool IsGameOver(Player player)
        {
            foreach (Ship ship in player.ships)
            {
                if (!ship.isSunk)
                {
                    return false;
                }
            }
            return true;
        }

        private static void PlayARound(Player player)
        {
            Console.WriteLine($"{player.Name} is up. Press enter to begin");
            Console.ReadLine();
            PrintField(player.grid , false);
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

        public static void PrintField(Grid grid, bool displayShips)
        {
            Console.Clear();
            List<string> writeToScreen = new List<string>();
            string[] bgcolor = new string[grid.rows];
            char row = 'A';
            int rowToIndex = row - 65;
            int column = 1;
            foreach (var square in grid.squares)
            {
                while (square.Key[0] != row)
                {
                    row++;
                    rowToIndex = row - 65;
                    column = 1;
                }
                while (Convert.ToInt32(Convert.ToString(square.Key.Substring(1))) != column)
                    column++;

                WriteASquare(ref bgcolor , square.Value, displayShips, writeToScreen, rowToIndex);
            }

            PrintAll(writeToScreen , bgcolor);
        }

        private static void PrintAll(List<string> writeToScreen , string[] bgcolor)
        {
            ConsoleColor frameColor = ConsoleColor.DarkGray;
            Dictionary<char, ConsoleColor> bgcolors = BGColorDictionary();
            int colorCounter;
            int row = 0;
            PrintColumns(writeToScreen , frameColor);
            foreach (string line in writeToScreen)
            {
                PrintRowFrame(frameColor,row);
                if (line == "")
                    continue;
                colorCounter = 0;
                foreach (char character in line)
                {
                    
                    Console.BackgroundColor = bgcolors[bgcolor[row/3][colorCounter/5]];
                    Console.Write(character);
                    colorCounter++;
                }
                PrintRowFrame(frameColor, row);
                Console.Write(Environment.NewLine);
                row++;

            }
            PrintColumns(writeToScreen, frameColor);
            Console.ResetColor();
        }

        private static void PrintRowFrame(ConsoleColor frameColor, int row)
        {
            Console.BackgroundColor = frameColor;
            if (row % 3 == 0 || row%3==2) // First or last line line
            {
                Console.Write("     ");
            }
            else
            {
                char letter = (char)(row / 3 + 65); // ASCII 65=A
                Console.Write($"  {Convert.ToString(letter)}  ");
            }
        }

        private static void PrintColumns(List<string> writeToScreen , ConsoleColor frameColor)
        {
            Console.BackgroundColor = frameColor;
            string empty = "     ";
            for (int i = 0; i < 3; i++)
            {
                Console.Write(empty);
                for (int j = 0; j < writeToScreen[0].Length / 5; j++)
                {
                    if ((i == 0) || (i == 2))
                        Console.Write(empty);
                    else
                    {
                        Console.Write($"  {j + 1}");
                        int lastEmptyInThisString = 3 - (Convert.ToString(j + 1)).Length;
                        for (int k = 0; k < lastEmptyInThisString; k++) { Console.Write(" "); }
                        
                    }
                }
                Console.WriteLine(empty);
            }
            Console.ResetColor();
        }

        private static Dictionary<char, ConsoleColor> BGColorDictionary()
        {
            Dictionary<char, ConsoleColor> bgcolor = new Dictionary<char, ConsoleColor>
            {
                { 'g',ConsoleColor.Green },
                {'b',ConsoleColor.Blue },
                {'r' , ConsoleColor.Red} ,
                {'y' , ConsoleColor.Yellow },
                {'d' , ConsoleColor.DarkBlue },
                {'w' , ConsoleColor.White }
            };
            return bgcolor;
        }



        private static void WriteASquare(ref string[] bgcolor , Square square , bool displayShips , List<string> writeToScreen , int row)
        {
               
            int[] rows = new int[3] { row * 3, row * 3 + 1, row * 3 + 2 };
            while (writeToScreen.Count < (row+1) * 3)
            {
                writeToScreen.Add("");
            }
            string middlePart;
            if (square.isHit)
            {
                if (square.isShip)
                {
                    if (square.belongsToShip.isSunk)
                    {
                        bgcolor[row] += "d";
                        middlePart = "xxx";
                    }
                    else
                    {
                        bgcolor[row] += "r";
                        middlePart = "xxx";
                    }
                }
                else
                {
                    bgcolor[row] += "y";
                    middlePart = "ooo";
                }
            }
            else if (square.isShip && displayShips)
            {
                bgcolor[row] += "g";
                middlePart = "+s+";
            } else
            {
                bgcolor[row] += "b";
                middlePart = "   ";
            }
            writeToScreen[rows[0]] += "+---+";
            writeToScreen[rows[1]] += $"|{middlePart}|";
            //writeToScreen[rows[2]] += $"|{middlePart}|";
            //writeToScreen[rows[3]] += $"|{middlePart}|";
            writeToScreen[rows[2]] += "+---+";
            
        }

        private static void StartGame(Players players , OnlineGame onlineGame)
        {
            PlaceShips(players , onlineGame);
        }

        private static void PlaceShips(Players players , OnlineGame onlineGame)
        {
            List<int> shipSizes;
            //PrintField(grid);
            if (onlineGame.PlayOnline)
            {
                if (onlineGame.IsHost)
                {
                    shipSizes = PickShipSizes();
                    SaveShipSizes(shipSizes, onlineGame);
                }
                else
                {
                    shipSizes = LoadShipSizes();
                }
            }
            else
            {
                shipSizes = PickShipSizes();
            }
            LetsPlaceShips(shipSizes , players); // todo: i denna metoden ska vi skapa en klass Ship inom Player, där skeppen lagras
        }

        private static void SaveShipSizes(List<int> shipSizes, OnlineGame onlineGame)
        {
            string[] shipSizesString = IntListToStringArray(shipSizes);
            File.WriteAllLines(onlineGame.onlineFiles["shipSizes"] , shipSizesString);
        }

        private static string[] IntListToStringArray(List<int> shipSizes)
        {
            string[] outString = new string[shipSizes.Count];
            for (int i = 0; i < shipSizes.Count; i++)
            {
                outString[i] = Convert.ToString(shipSizes[i]);
            }
            return outString;
        }

        private static List<int> LoadShipSizes()
        {
            List<int> shipSizes;
            // todo: fortsätt här! ladda in skeppsstorlekar från fil. Se även till att host-spelaren sparar filen
            throw new NotImplementedException();
            return shipSizes;
        }

        private static void LetsPlaceShips(List<int> shipSizes , Players players)
        {
            players.player1.PlaceShips(shipSizes);// LetUserPlaceShips(shipSizes , players.player1);
            players.player2.PlaceShips(shipSizes);
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
