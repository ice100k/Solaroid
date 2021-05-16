//Solaroid
//By ice100k and thecrisperson
using System;
using System.IO;
using System.Net;

namespace Solaroid {

    public class ConsoleSpiner {
        private int counter;

        public ConsoleSpiner() {
            counter = 0;
        }

        public void Turn() {
            counter++;
            switch (counter % 4) {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }

    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine("                  [SOLAROID]");
            Console.WriteLine("         The universe is in your hands");
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Solaroid Instller v0.0.1-ALPHA");
            Console.WriteLine("Made by: ice100k, thecrisperson and literaly_no1");
            Console.WriteLine("");
            Console.WriteLine("All Solaroid files will be downloaded here:");
            Console.WriteLine((Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString()) + "\\Solaroid");
            Console.WriteLine("");
            Console.WriteLine("Press A to continue or press Esc to quit ");

            ConsoleKeyInfo KeyInfo;
            while (true) {
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                if (KeyInfo.Key == ConsoleKey.A)
                    break;
            }

            Console.WriteLine("");
            Console.WriteLine("Download Solaroid Core? (y/n) ");

            while (true) {
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.Y) {
                    string extractPath = ((Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString()) + "\\Solaroid");

                    ConsoleSpiner spin = new ConsoleSpiner();
                    Console.WriteLine("");
                    Console.WriteLine("Working.... ");

                    using (var Client = new WebClient()) {

                        try {
                            Client.DownloadFileAsync(

                            new System.Uri("https://github.com/ice100k/Solaroid/raw/main/Solaroid.zip"),

                            extractPath
                        );
                        } catch (UnauthorizedAccessException) {
                            Console.Write("UnAuthorizedAccessException: Unable to access file. ");
                        }

                        System.IO.Compression.ZipFile.ExtractToDirectory(extractPath + "\\Solaroid.zip", extractPath);

                        Console.WriteLine("Done!");
                    }

                    if (KeyInfo.Key == ConsoleKey.N)
                        break;
                }

            }
        }
    }
}