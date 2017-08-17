using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Players
    {
        public static Player AddPlayer()
        {
            Console.WriteLine("Enter Player Name: ");
            return new Player(Console.ReadLine());
        }
        public Player player1 = AddPlayer();
        public Player player2 = AddPlayer();
    }
}
