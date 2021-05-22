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
            Console.WriteLine("Welcome to the Solaroid Installer v1.0.1-BETA");
            Console.WriteLine("Made by: ice100k, thecrisperson and literaly_no1");
            Console.WriteLine("");
            Console.WriteLine("All Solaroid files will be downloaded here:");
            Console.WriteLine(@"C:\Solaroid");
            Console.WriteLine("");
            Console.WriteLine(".NET 5.0+ required");
            Console.WriteLine("Currently running version: {0}", Environment.Version.ToString());
            Console.WriteLine("");
            Console.WriteLine("If the versions dont match you can download .NET 5.0 here: https://download.visualstudio.microsoft.com/download/pr/2892493e-df43-409e-af68-8b14aa75c029/53156c889fc08f01b7ed8d7135badede/dotnet-sdk-5.0.100-win-x64.exe");
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
                            Console.WriteLine("Directory C:\\Solaroid\\ was created sucessfully at: {0}", Directory.GetCreationTime(extractPath));

                            DirectorySecurity dSecurity = dInfo.GetAccessControl();

                            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);

                            dInfo.Attributes &= ~FileAttributes.ReadOnly;
                            dInfo.Attributes &= FileAttributes.Hidden;

                        } catch (Exception e) {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                            Console.WriteLine("");
                            Console.WriteLine("Do you wish to continue (Y/N) ");

                            while (true) {
                                KeyInfo = Console.ReadKey();
                                if (KeyInfo.Key == ConsoleKey.N)
                                    Environment.Exit(0);
                                if (KeyInfo.Key == ConsoleKey.Y)
                                    break;
                            }

                        }

                    }

                    string tempPath = Path.GetTempPath() + @"\Solaroid";
                    Directory.CreateDirectory(tempPath);

                    DirectoryInfo dI = new DirectoryInfo(extractPath);

                    DirectorySecurity dS = dI.GetAccessControl();

                    dS.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dI.SetAccessControl(dS);

                    dI.Attributes &= ~FileAttributes.ReadOnly;
                    dI.Attributes &= FileAttributes.Hidden;

                    dI.Refresh();

                    Console.WriteLine("");
                    Console.WriteLine("Downloading.... ");

                    try {
                        //Client.DownloadFile(new System.Uri("https://github.com/ice100k/Solaroid/raw/main/Solaroid.zip"), tempPath);
                        downloadFile("https://github.com/ice100k/Solaroid/raw/main/Solaroid.zip", tempPath + @"\Solaroid.zip");

                    } catch (Exception e) {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                        Console.WriteLine("");
                        Console.WriteLine("Do you wish to continue (Y/N) ");

                        while (true) {
                            KeyInfo = Console.ReadKey();
                            if (KeyInfo.Key == ConsoleKey.N)
                                Environment.Exit(0);
                            if (KeyInfo.Key == ConsoleKey.Y)
                                break;
                        }
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Unzipping Core...");

                    System.IO.Compression.ZipFile.ExtractToDirectory(tempPath + "\\Solaroid.zip", extractPath);
                    File.Delete(tempPath + "\\Solaroid.zip");
                    Directory.Delete(tempPath);

                    Console.WriteLine("");
                    Console.WriteLine("Sucsessfully setup Core! Press Esc to exit!");

                    while (true) {
                        KeyInfo = Console.ReadKey();
                        if (KeyInfo.Key == ConsoleKey.Escape)
                            Environment.Exit(0);
                    }

                }

                if (KeyInfo.Key == ConsoleKey.N)
                    break;

            }

        }

        public static void downloadFile(string sourceURL, string destinationPath) {
            long fileSize = 0;
            int bufferSize = 1024;
            bufferSize *= 1000;
            long existLen = 0;

            System.IO.FileStream saveFileStream;
            if (System.IO.File.Exists(destinationPath)) {
                System.IO.FileInfo destinationFileInfo = new System.IO.FileInfo(destinationPath);
                existLen = destinationFileInfo.Length;
            }

            if (existLen > 0)
                saveFileStream = new System.IO.FileStream(destinationPath,
                                                          System.IO.FileMode.Append,
                                                          System.IO.FileAccess.Write,
                                                          System.IO.FileShare.ReadWrite);
            else
                saveFileStream = new System.IO.FileStream(destinationPath,
                                                          System.IO.FileMode.Create,
                                                          System.IO.FileAccess.Write,
                                                          System.IO.FileShare.ReadWrite);

            System.Net.HttpWebRequest httpReq;
            System.Net.HttpWebResponse httpRes;
            httpReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sourceURL);
            httpReq.AddRange((int)existLen);
            System.IO.Stream resStream;
            httpRes = (System.Net.HttpWebResponse)httpReq.GetResponse();
            resStream = httpRes.GetResponseStream();

            fileSize = httpRes.ContentLength;

            int byteSize;
            byte[] downBuffer = new byte[bufferSize];

            while ((byteSize = resStream.Read(downBuffer, 0, downBuffer.Length)) > 0) {
                saveFileStream.Write(downBuffer, 0, byteSize);
            }

            saveFileStream.Close();
        }

    }
}