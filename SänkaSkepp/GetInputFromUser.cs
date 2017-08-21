using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class GetInputFromUser
    {
        public static int GetInt(string question)
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

        public static string GetString(string question, int minChars, int maxChars)
        {
            return GetStringMethod(question, minChars, maxChars);
        }

        public static string GetString(string question)
        {
            return GetStringMethod(question, 0, 1000000000); //Behövs så långa strängar? Det är en övre gräns! c.f. överlagring
        }

        public static string GetStringMethod(string question, int minChars, int maxChars)
        {
            while (true)
            {
                Console.Write(question);
                string input = Console.ReadLine();
                if ((input.Length >= minChars) && (input.Length <= maxChars))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine($"Your text has to have at least {minChars} characters and at most {maxChars} characters. Current length is {input.Length} characters");
                }
            }
        }

        public static char GetChar(string question)
        {
            Console.Write(question);
            char c = char.Parse(Console.ReadLine());
            return c;

        }

        public static int[] GetTwoInts(string question, char separator, int max , int[] defaultOutput)
        {
            while (true)
            {
                string input = GetString(question);
                if (input == "")
                {
                    return defaultOutput;
                }
                int[] output = ReturnIntMethod(input, question, separator);
                if (((output[0] > 0) && (output[1] > 0)) && ((output[0] <= max) && (output[1] <= max) ))
                    
                    return output;
                else if ((output[0] > max) || (output[1] > max))
                {
                    Console.WriteLine($"The grid is too large! Maximum size is {max}x{max}");
                }
            }
        }


        public static int[] GetTwoInts(string question, char separator) //Onödigt att bryta ut denna från ReturnIntMethod
        {
            string input = GetString(question);

            return ReturnIntMethod(input, question, separator);
        }

        private static int[] ReturnIntMethod(string input, string question, char separator)
        {

            string[] splitInput = input.Split(separator);
            if (splitInput.Length == 2)
            {
                int[] output = new int[2];
                if (!(int.TryParse(splitInput[0], out output[0]) && int.TryParse(splitInput[1], out output[1])))
                {
                    Console.WriteLine("Bad input!");
                    return new int[] { 0, 0 };
                }
                else
                {
                    return output;
                }
            }
            else
            {
                return new int[] { 0, 0 };
            }

        }

    }
}
