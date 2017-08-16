using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class GetInputFromUser
    {
        public int GetInt(string question)
        {
            while (true)
            {
                Console.Write(question);
                string input = Console.ReadLine();
                if (input.Length != 0)
                {
                    if (int.TryParse(input, out int output))
                    {
                        return output;
                    }
                }
            }

        }

        public string GetString(string question)
        {

        }

        public char GetChar(string question)
        {

        }
    }
}
