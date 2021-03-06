﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Grid //Lägg till min och maxstorlek på Grid - annars kraschar spelet
    {

        public Dictionary<string, Square> squares = new Dictionary<string, Square>();
        public int rows;
        public int columns;

        public Grid(int _rows , int _columns)
        {
            rows = _rows;
            columns = _columns;
            SetGrid();
        }

        private void SetGrid()
        {
            List<string> coords = SetupGridCoords();

            foreach (string coord in coords)
            {
                /* Användbara i framtiden
                int row = (int)coord[0] - 65; //Character A becomes int 0
                int column = Convert.ToInt32(coord[1]);
                */

                squares.Add(coord , new Square(coord));
            }
        }

       
        private List<string> SetupGridCoords() //returns coordinates of new grid
        {
            //Starting with rectangular grid
            //int rows = 4;
            //int columns = 4;
            char letter = 'A';
            List<string> coords = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    coords.Add($"{letter}{j+1}");
                }
                letter++; //Smart användning av bit-nummer! Finns det någon risk med detta?
            }
            return coords;
        }


    }
}
