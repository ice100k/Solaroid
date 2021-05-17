//Solaroid
//By ice100k and thecrisperson, Lore by Literally_no1
using System;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Security.AccessControl;

namespace Solaroid {

    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine("                  [SOLAROID]");
            Console.WriteLine("         The universe is in your hands");
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Solaroid Instller v0.0.1-ALPHA");
            Console.WriteLine("Made by: ice100k, thecrisperson and literaly_no1");
            Console.WriteLine("");
            Console.WriteLine("All Solaroid files will be downloaded here:");
            Console.WriteLine(@"C:\Solaroid");
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
            Console.WriteLine("Download Solaroid Core? (Y/N) ");

            while (true) {
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.Y) {
                    string extractPath = @"C:\Solaroid";

                    if (Directory.Exists(extractPath)) {
                        Console.WriteLine("");
                        Console.WriteLine(extractPath + " already exists. Skipping step...");
                    } else {
                        try {
                            Console.WriteLine("");
                            DirectoryInfo dInfo = Directory.CreateDirectory(extractPath);
                            Console.WriteLine("Directory {0} was created sucessfully at: ", Directory.GetCreationTime(extractPath));

                            DirectorySecurity dSecurity = dInfo.GetAccessControl();

                            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);

                        } catch (Exception e) {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                    }

                    DirectoryInfo dI = new DirectoryInfo(extractPath);

                    DirectorySecurity dS = dI.GetAccessControl();

                    dS.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dI.SetAccessControl(dS);

                    dI.Attributes &= ~FileAttributes.ReadOnly;

                    dI.Refresh();

                    Console.WriteLine("");
                    Console.WriteLine("Downloading.... ");

                    using (var Client = new WebClient()) {
                        try {
                            Client.DownloadFile(new System.Uri("https://github.com/ice100k/Solaroid/raw/main/Solaroid.zip"), extractPath);
                        } catch (Exception e) {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Unzipping Core...");

                    System.IO.Compression.ZipFile.ExtractToDirectory(extractPath + "\\Solaroid.zip", extractPath);
                    File.Delete(extractPath + "\\Solaroid.zip");

                    Console.WriteLine("");
                    Console.WriteLine("Sucsessfully setup Core!");

                    if (KeyInfo.Key == ConsoleKey.N)
                        break;
                }

            }
        }
    }
}