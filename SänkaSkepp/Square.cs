using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Square
    {
        public bool isHit;
        public bool isShip;
        public string coords;
        public List<string> belongsToShip;
        public bool isSunk;

        public Square(string _coords)
        {
            coords = _coords;
            isHit = false;
            isShip = false;
            isSunk = false;
        }

        public Square(string _coords, bool _isHit, bool _isShip)
        {
            coords = _coords;
            isHit = _isHit;
            isShip = _isShip;
        }
    }
}
