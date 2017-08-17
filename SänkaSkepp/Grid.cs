using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Grid
    {
        
        public List<Square> squares = new List<Square>();

        private void SetGrid()
        {
            List<string> coords = SetupGridCoords();

            foreach (string coord in coords)
            {
                int row = (int)coord[0] - 65; //Character A becomes int 0
                int column = Convert.ToInt32(coord[1]);

                squares.Add(new Square(coord));
            }
        }

       
        private static List<string> SetupGridCoords() //returns coordinates of new grid
        {
            //Starting with rectangular grid
            int rows = 4;
            int columns = 4;
            char letter = 'A';
            List<string> coords = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    coords.Add($"{letter}{j}");
                }
                letter++;
            }
            return coords;
        }


    }
}
