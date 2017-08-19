﻿using System;
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

        public static string GetString(string question, int minChars , int maxChars)
        {
            return GetStringMethod(question, minChars, maxChars);
        }

        public static string GetString(string question)
        {
            return GetStringMethod(question, 0, 1000000000);
        }

        public static string GetStringMethod(string question , int minChars , int maxChars)
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

        public static char GetChar(string question)
        {
            Console.Write(question);
            char c = char.Parse(Console.ReadLine());
            return c;

        }

        public static int[] GetTwoInts(string question, char separator , int[] defaultOutput)
        {
            string input = GetString(question);
            if (input == "")
            {
                return defaultOutput;
            }
             return ReturnIntMethod(input , question , separator);
        }

        public static int[] GetTwoInts(string question, char separator)
        {
            string input = GetString(question);
            
            return ReturnIntMethod(input, question, separator);
        }

        private static int[] ReturnIntMethod(string input , string question , char separator)
        {
            while (true)
            {
                string[] splitInput = input.Split(separator);
                if (splitInput.Length == 2)
                {
                    int[] output = new int[2];
                    if (!(int.TryParse(splitInput[0], out output[0]) && int.TryParse(splitInput[1], out output[1])))
                    {
                        Console.WriteLine("Bad input!");
                    }
                    else
                    {
                        return output;
                    }
                }
                else
                {
                    input = GetInputFromUser.GetString(question);
                }
            }
        }

    }
}
