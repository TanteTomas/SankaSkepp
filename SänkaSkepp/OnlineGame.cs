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
        public Dictionary<string,string> onlineFiles = new Dictionary<string, string> {
            { "shot" , "shot.txt" },
            { "shipSizes" , "shipSizes.txt" } };

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

        public void SetUpOnlineGame()
        {
            PlayOnline = AskIfPlayOnline();
            if (PlayOnline)
            {
                IsHost = IsHoster();

                string dropboxPath = SetUpDropBoxPath();
                dropboxPath += "\\Battleships";

                AppendOnlineFiles(dropboxPath);
                DeleteFiles(onlineFiles);

                if (!Directory.Exists(dropboxPath))
                    Directory.CreateDirectory(dropboxPath);
                Console.WriteLine($"Please set directory {dropboxPath} as shared with your opponent, then press any key");
                Console.ReadKey();

                OnlineFilePath = $"{dropboxPath}\\shot.txt";
                if (IsHost)
                {
                    if (File.Exists(OnlineFilePath))
                        File.Delete(OnlineFilePath);
                    File.Create(OnlineFilePath);
                }
            }
        }

        private void AppendOnlineFiles(string dropboxPath)
        {
            List<string> keys = new List<string>(onlineFiles.Keys);
            foreach (string key in keys)
            {
                onlineFiles[key] = $"{dropboxPath}\\{onlineFiles[key]}";
            }

        }

        private void DeleteFiles(Dictionary<string,string> onlineFiles)
        {
            foreach (var item in onlineFiles)
            {
                if (File.Exists(item.Value))
                    File.Delete(item.Value);
            }
        }

        private static string SetUpDropBoxPath()
        {
            while (true)
            {
                Console.Write("Write path to your dropbox directory: ");
                string dropboxPathRaw = Console.ReadLine();
                
                if (Directory.Exists(dropboxPathRaw))
                {
                    return dropboxPathRaw;
                }
                else if (Directory.Exists($"{dropboxPathRaw}\\Dropbox"))
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
