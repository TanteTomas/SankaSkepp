using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class Players
    {
        public static Player AddPlayer() //otydligt Player vs Players - Varför ha två olika? Döp om för tydlighetens skull?
        {
            Console.WriteLine("Enter Player Name: ");
            string playerName = Console.ReadLine();
            return new Player(playerName, gridSize);
        }
       
	

	
        static int[] gridSize = GetInputFromUser.GetTwoInts("Set grid size (as rows,columns) [8,8]", ',', 25 ,new int[] { 8, 8 }); //Hör denna till denna klassen?
    
        public Player player1 = AddPlayer();
        public Player player2 = AddPlayer();
    }
}
