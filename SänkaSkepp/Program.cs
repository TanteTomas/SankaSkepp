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
            
            bool playOnline = AskIfPlayOnline();
            string onlineFilePath;
            if (playOnline)
            {
                onlineFilePath = SetUpOnlineGame();
            }
            
            while (true) //while(not all hit)
            {
                PrintField(players.player2.grid , false);
                Console.WriteLine(players.player1.Name+"'s turn.");
                players.player1.DropBomb(players.player2.grid);
                PrintField(players.player2.grid, false);
                Console.WriteLine("Press enter for next turn");
                Console.ReadLine();

                PrintField(players.player1.grid , false);
                Console.WriteLine(players.player2.Name + "'s turn.");
                players.player2.DropBomb(players.player1.grid);
                PrintField(players.player1.grid, false);
                Console.WriteLine("Press enter for next turn");
                Console.ReadLine();
            }
        }

        private static string SetUpOnlineGame()
        {
            string dropboxPath = SetUpDropBoxPath();
            string onlineFilePath = $"{dropboxPath}\\shot.txt");
            dropboxPath += "\\Battleships";
            if (!File.Exists(dropboxPath))
                File.Create(dropboxPath);
            if (File.Exists(onlineFilePath))
                File.Delete(onlineFilePath);
            File.Create(onlineFilePath);

            return onlineFilePath;
        }

        private static string SetUpDropBoxPath()
        {
            while (true)
            {
                Console.Write("Write path to your dropbox directory: ");
                string dropboxPathRaw = Console.ReadLine();
                if (File.Exists(dropboxPathRaw))
                {
                    return dropboxPathRaw;
                }
                else if (File.Exists($"{dropboxPathRaw}\\Dropbox"))
                {
                    return $"{dropboxPathRaw}\\Dropbox";
                }
                else
                {
                    Console.WriteLine("Invalid path!");
                }
            }
        }

        private static bool AskIfPlayOnline()
        {
            Console.Write("Do you want to play on different computers? ([y]/n)");
            string input = Console.ReadLine().ToUpper();
            return (input == "" || input == "Y")
            
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

        private static void PrintField(Grid grid, bool displayShips)
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
                while (Convert.ToInt32(Convert.ToString(square.Key[1])) != column)
                    column++;

                WriteASquare(ref bgcolor , square.Value, displayShips, writeToScreen, rowToIndex);
            }

            PrintAll(writeToScreen , bgcolor);
        }

        

        private static void PrintAll(List<string> writeToScreen , string[] bgcolor)
        {
            Dictionary<char, ConsoleColor> bgcolors = BGColorDictionary();
            int colorCounter;
            int row = 0;

            foreach (string line in writeToScreen)
            {
                if (line == "")
                    continue;
                colorCounter = 0;
                foreach (char character in line)
                {
                    
                    Console.BackgroundColor = bgcolors[bgcolor[row/3][colorCounter/5]];
                    Console.Write(character);
                    colorCounter++;
                }
                Console.Write(Environment.NewLine);
                row++;

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
                    if (square.isSunk)
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
            LetUserPlaceShips(shipSizes , players.player1);
            LetUserPlaceShips(shipSizes, players.player2);
        }

        private static void LetUserPlaceShips(List<int> shipSizes, Player player)
        {
            Console.Clear();
            Console.WriteLine($"{player.Name} get ready to place ships. Hit enter when ready!");
            Console.ReadLine();
            
            Console.WriteLine("Chose what grid the upper left corner of the ship should be in (on the form A1)");
            int shipLength = 2;
            foreach (int numberOfShipsOfThisSize in shipSizes)
            {
                
                for (int i = 0; i < numberOfShipsOfThisSize; i++)
                {
                    PrintField(player.grid, true);
                    while (true)
                    {
                        string position = GetInputFromUser.GetString($"Position of boat of length {shipLength}: ");
                        if (!player.grid.squares.ContainsKey(position))
                        {
                            Console.WriteLine("This grid doesn't exist!");
                            continue;
                        }
                        if (PlaceThisShip(player , position , shipLength))
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
            PrintField(player.grid, true);
            System.Threading.Thread.Sleep(1000);
        }

        private static bool PlaceThisShip(Player player , string position , int length)
        {
            // todo: add in Square instance to what ship a square belongs to
            List<string> shipCoords = GetShipCoords(position, length);
            foreach (string coord in shipCoords)
            {
                if (!player.grid.squares.ContainsKey(coord))
                    return false;
                if (player.grid.squares[coord].isShip)
                    return false;
            }

            foreach (string coord in shipCoords)
            {
                player.grid.squares[coord].isShip = true;
                player.grid.squares[coord].belongsToShip = shipCoords;

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
