using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;

namespace RestartSteam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Restart Steam";
            string Version = "1.0.0";

            bool SkipConfirmation = false;
            bool RestartConfirmed = false;
            bool DebugMode = false;

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg == "-help")
                {
                    Console.WriteLine("-help - Opens this");
                    Console.WriteLine("-github - Opens GitHub page");
                    Console.WriteLine("-yes - Skip confirmation");
                    Console.WriteLine("-y");
                    Console.WriteLine("-debug - Runs the app in debug mode");
                    Console.WriteLine("-version - Shows installed version");
                    Console.WriteLine("-ver");
                    Environment.Exit(0);
                }
                if (arg == "-y" || arg == "-yes")
                {
                    SkipConfirmation = true;
                }
                if (arg == "-github")
                {
                    Process.Start("https://github.com/KilLo445/RestartSteam");
                    Environment.Exit(0);
                }
                if (arg == "-debug")
                {
                    DebugMode = true;
                    Console.Title = "Restart Steam [Debug Mode]";
                }
                if (arg == "-ver" || arg == "-version")
                {
                    Console.WriteLine($"You have v{Version} installed");
                    Environment.Exit(0);
                }
            }

            if (SkipConfirmation == false)
            {
                RestartConfirmed = false;
                string Key;
                
                while (RestartConfirmed == false)
                {
                    Console.WriteLine("Are you sure you want to restart steam? [Y/N]");
                    Key = Console.ReadLine();

                    if (Key == "Y" || Key == "y" || Key == "N" || Key == "n")
                    {
                        if (Key == "Y" || Key == "y")
                        {
                            RestartConfirmed = true;
                        }
                        if (Key == "N" || Key == "n")
                        {
                            RestartConfirmed = false;
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");
                    }
                }
            }

            string steamPath = "SteamPath";
            string steamExe = "SteamExe";

            bool steamRunning = false;

            if (Process.GetProcessesByName("steam").Length > 0)
            {
                steamRunning = true;
                
                try
                {
                    if (DebugMode == true)
                    {
                        Console.WriteLine("[Debug] Getting Steam install path...");
                    }

                    using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam"))
                    {
                        if (DebugMode == true)
                        {
                            Console.WriteLine("[Debug] Path: Computer\\HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Valve\\Steam");
                        }

                        if (regKey != null)
                        {
                            Object obSteamPath = regKey.GetValue("InstallPath");
                            if (obSteamPath != null)
                            {
                                steamPath = (obSteamPath as String);
                                steamExe = Path.Combine(steamPath, "steam.exe");

                                if (DebugMode == true)
                                {
                                    Console.WriteLine("[Debug] Name: InstallPath");
                                    Console.WriteLine($"[Debug] Value: {steamPath}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemSounds.Exclamation.Play();
                    Console.WriteLine($"{ex}");
                }

                Console.WriteLine("Shutting down Steam...");
                Process.Start(steamExe, "-shutdown");

                Thread.Sleep(1000);

                while (steamRunning == true)
                {
                    if (DebugMode == true)
                    {
                        Console.WriteLine("[Debug] Checking for Steam...");
                    }
                    
                    if (Process.GetProcessesByName("steam").Length > 0)
                    {
                        steamRunning = true;
                    }
                    else
                    {
                        steamRunning = false;
                    }

                    Thread.Sleep(1000);
                }

                Console.WriteLine("Starting Steam...");
                Process.Start(steamExe);

                Thread.Sleep(1000);

                while (steamRunning == false)
                {
                    if (DebugMode == true)
                    {
                        Console.WriteLine("[Debug] Checking for Steam...");
                    }

                    if (Process.GetProcessesByName("steam").Length > 0)
                    {
                        steamRunning = true;
                    }
                    else
                    {
                        steamRunning = false;
                    }

                    Thread.Sleep(1000);
                }

                Thread.Sleep(3000);

                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Steam does not seem to be running.");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
