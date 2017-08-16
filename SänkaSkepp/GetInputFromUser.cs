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

        public string GetString(string question, int minChars , int maxChars)
        {
            return GetStringMethod(question, minChars, maxChars);
        }

        public string GetString(string question)
        {
            return GetStringMethod(question, 0, 1000000000);
        }

        public string GetStringMethod(string question , int minChars , int maxChars)
        {
            while (true)
            {
                Console.Write(question);
                string input = Console.ReadLine();
                if ((input.Length>=minChars) && (input.Length <= maxChars))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine($"Your text has to have at least {minChars} characters and at most {maxChars} characters. Current length is {input.Length} characters");
                }
            }
        }

        public char GetChar(string question)
        {
            Console.Write(question);
            char c = char.Parse(Console.ReadLine());
            return c;

        }
    }
}
