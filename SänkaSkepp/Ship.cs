using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Ship
    {
        public List<string> coordinates;
        public bool isSunk;

        public Ship(List<string> _coordinates)
        {
            coordinates = _coordinates;
        }
    }
}
