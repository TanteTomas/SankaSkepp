using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp 
{
    /*MÅnga korta och koncisa metoder, dock blir koden lite otydlig i och med att ni har så många olika metoder som är lite spretigt sorterade.
     * Många metoder är dessutom ganska snarlika, både till namn och syfte.
     * Bra felhantering, snygga lösningar för input-hantering etc!
     Bra förbättring av själva spelet! Nu är det lätt att förstå och följa hur spelet går till.
     Ev förbättring är att förtydliga kring antalet båtar som man anger i början, samt generellt "pimpa" ytterligare
     Ambitiöst och avancerat, inte så konstigt att det inte är helt buggfritt ännu!*/
    class Player
    {
        public string Name { get; private set; }
        public int Score { get; set; }
        public bool IsTurn { get; set; }
        public Grid grid;
        private string onlineFilePath;
        private bool hasReadFromFile = false;
        private string nextShot;
        public List<Ship> ships = new List<Ship>(); //Sätt att "negativa" skepp inte är tillåtna (ex -1)

        public static int NumberOfPlayers = 0;

        public Player(string name , int[] gridSize)
        {
            Name = name;
            Score = 0;
            IsTurn = (NumberOfPlayers == 0);
            NumberOfPlayers++;
            grid = SetGridSize(gridSize);
            
        }


        private static Grid SetGridSize(int[] gridSize)
        {
            // skapa en instans av klassen grid
            Grid grid = new Grid(gridSize[0], gridSize[1]); 

            return grid;
        }

        public void PlaceShips(List<int> shipSizes)
        {
            Console.Clear();
            Console.WriteLine($"{this.Name} get ready to place ships. Hit enter when ready!");
            Console.ReadLine();

            Console.WriteLine("Chose what grid the upper left corner of the ship should be in (on the form A1)");
            int shipLength = 2;
            foreach (int numberOfShipsOfThisSize in shipSizes)
            {

                for (int i = 0; i < numberOfShipsOfThisSize; i++)
                {
                    Program.PrintField(this.grid, true);
                    while (true)
                    {
                        
                        string position = GetInputFromUser.GetString($"Position of boat of length {shipLength}: ").ToUpper();
                        if (!this.grid.squares.ContainsKey(position))
                        {
                            Console.WriteLine("This grid doesn't exist!");
                            continue;
                        }
                        if (PlaceThisShip(position, shipLength))
                            break;
                        else
                            Console.WriteLine("The ship cannot be here!");
                    }

                }
                shipLength++;
            }
            Program.PrintField(this.grid, true);
            System.Threading.Thread.Sleep(1000);
        }

        private bool PlaceThisShip(string position, int length)
        {
            // todo: add in Square instance to what ship a square belongs to
            List<string> shipCoords = GetShipCoords(position, length);
            foreach (string coord in shipCoords)
            {
                if (!this.grid.squares.ContainsKey(coord))
                    return false;
                if (this.grid.squares[coord].isShip)
                    return false;
            }

            
            CreateShip(grid , shipCoords);
             
            return true;
        }

        private void CreateShip(Grid grid , List<string> shipCoords)
        {
            this.ships.Add(new Ship(shipCoords));
            foreach (string partOfTheShip in shipCoords)
            {
                this.grid.squares[partOfTheShip].isShip = true;
                this.grid.squares[partOfTheShip].belongsToShip = this.ships[ships.Count - 1];

            }
        }

        private List<string> GetShipCoords(string position, int length)
        {
            List<string> shipCoords = new List<string>();
            char orientation = GetInputFromUser.GetChar("Orientation ([h]orisontal/diagonal [d]own right/diagonal [u]p right/[v]ertical): ");
            char row = position[0];
            int column = Convert.ToInt32(Convert.ToString(position[1]));

            for (int i = 0; i < length; i++)
            {
                shipCoords.Add(row + Convert.ToString(column));
                NextPartOfTheShip(orientation, ref row, ref column);
            }
            return shipCoords;
        }

        private void NextPartOfTheShip(char orientation, ref char row, ref int column)
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
                case 'u':
                    row--;
                    column++;
                    break;
                default:
                    break;
            }
        }


        public Message DropBomb(Grid grid, OnlineGame onlineGame, bool willEnterManually)
        {
            string input;
            bool enterManually = ((willEnterManually && onlineGame.PlayOnline) || !onlineGame.PlayOnline);
            while (true)
            {
                if (enterManually)
                {
                    input = GetInputFromUser.GetString("Enter coords where to fire: ").ToUpper();
                    
                    if (onlineGame.PlayOnline)
                    {
                        WriteToFile(input);
                    }

                }
            
                else
                {
                    onlineFilePath = onlineGame.OnlineFilePath;
                    GetCoordinateFromFile();
                    input = nextShot;
                }
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

                bool isHit = grid.squares[input].isShip;
                if (isHit)
                    
                    return ReturnHit(grid, input);
                else
                    return Miss(grid, input);
                
                
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

        private Message Miss(Grid grid, string input)
        {
            grid.squares[input].isHit = true;
            return new Message("Miss!" , false);
        }

        private Message ReturnHit(Grid grid, string input)
        {
            Message message = new Message("Hit!", true);
            grid.squares[input].isHit = true;
            if (IsTheShipSunk(grid, grid.squares[input]))
            {
                SinkTheShip(grid, grid.squares[input]);
                return new Message("Good job, you sunk the ship!", true);
            }
            return new Message("Hit!" , true);
            

        }

        private void WriteToFile(string input)
        {
            File.WriteAllText(onlineFilePath, input);
        }

        private void GetCoordinateFromFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = onlineFilePath,
                Filter = "shipSizes.txt"
            };
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Waiting for opponent to shoot");
            while (!hasReadFromFile)
            {
               
            }
            hasReadFromFile = false;
            
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            nextShot = File.ReadAllLines(onlineFilePath)[0];
            hasReadFromFile = true;
        }

        private void SinkTheShip(Grid grid, Square thisSquare)
        {
            thisSquare.belongsToShip.isSunk = true;
            

            // todo: play sinking sound!
            
        }

        private bool IsTheShipSunk(Grid grid, Square thisSquare)
        {
            
            foreach (string coord in thisSquare.belongsToShip.coordinates)
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
