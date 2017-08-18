using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SänkaSkepp
{
    class OnlineGame
    {
        public bool IsHost { get; set; }
        public string OnlineFilePath { get; private set; }
        public bool PlayOnline { get; set; }

        private static bool IsHoster()
        {

            string input = GetInputFromUser.GetString("Will you host? (y/n)");
            if (input.ToUpper() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string SetUpOnlineGame()
        {
            PlayOnline = AskIfPlayOnline();
            string dropboxPath = SetUpDropBoxPath();
            dropboxPath += "\\Battleships";
            if (!File.Exists(dropboxPath))
                File.Create(dropboxPath);
            Console.WriteLine($"Please set directory {dropboxPath} as shared with your opponent, then press any key");
            Console.ReadKey();

            OnlineFilePath = $"{dropboxPath}\\shot.txt";
            if (IsHost)
            {
                if (File.Exists(OnlineFilePath))
                    File.Delete(OnlineFilePath);
                File.Create(OnlineFilePath);
            }

            return OnlineFilePath;
        }

        private static string SetUpDropBoxPath()
        {
            while (true)
            {
                Console.Write("Write path to your dropbox directory: ");
                string dropboxPathRaw = Console.ReadLine();
                if (File.Exists(dropboxPathRaw))
                {
                    return dropboxPathRaw;
                }
                else if (File.Exists($"{dropboxPathRaw}\\Dropbox"))
                {
                    return $"{dropboxPathRaw}\\Dropbox";
                }
                else
                {
                    Console.WriteLine("Invalid path!");
                }
            }
        }

        private static bool AskIfPlayOnline()
        {
            Console.Write("Do you want to play on different computers? ([y]/n)");
            string input = Console.ReadLine().ToUpper();
            return (input == "" || input == "Y");


        }
    }
}
