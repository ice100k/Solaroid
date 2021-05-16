//Solaroid
//By ice100k and thecrisperson
using System;
using System.IO;

namespace Solaroid {

    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine("                  [SOLAROID]");
            Console.WriteLine("         The universe is in your hands");
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Solaroid Instller v0.0.1-ALPHA");
            Console.WriteLine("Made by: ice100k, thecrisperson and literaly_no1");
            Console.WriteLine("");
            Console.WriteLine("Press A to continue or press Esc to quit ");

            ConsoleKeyInfo KeyInfo;
            while (true) {
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.A)
                    Environment.Exit(0);
                if (KeyInfo.Key == ConsoleKey.A)
                    break;
            }

            string extractPath = (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            System.IO.Compression.ZipFile.ExtractToDirectory("Bin.zip", extractPath);

        }
    }
}