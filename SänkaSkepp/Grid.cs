using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Grid
    {
        
        public List<Square> squares;

        private static void SetGrid()
        {
            List<string> coords = WriteGrid();
            throw new NotImplementedException();
        }

        private static void GetGridSize()
        {
            throw new NotImplementedException();
        }

        private static List<string> WriteGrid() //returns coordinates of new grid
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
