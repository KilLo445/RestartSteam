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
        static bool SkipConfirmation = false;
        static bool RestartConfirmed = false;
        static bool DebugMode = false;

        static string rootPath = Path.Combine(Directory.GetCurrentDirectory());

        static void Main(string[] args)
        {
            Console.Title = "Restart Steam";
            string Version = "2.0.0";

            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                regKey.CreateSubKey("KilLo");
                using (RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey("Software\\KilLo", true))
                {
                    regKey2.CreateSubKey("RestartSteam");
                    regKey2.Close();
                }
                regKey.Close();
            }

            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\KilLo\\RestartSteam", true))
            {
                regKey.SetValue("InstallPath", rootPath);
                regKey.SetValue("Version", Version);
            }

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg == "-h" || arg == "-help")
                {
                    Console.WriteLine("-help - Opens this");
                    Console.WriteLine("-github - Opens GitHub page");
                    Console.WriteLine("-restart - Restart Steam");
                    Console.WriteLine("-yes - Skip confirmation");
                    Console.WriteLine("-debug - Runs the app in debug mode");
                    Console.WriteLine("-version - Shows installed version");
                    Console.WriteLine();
                    Console.WriteLine("These also work with the first letter of each arg, -r, -y, etc.");
                }
                if (arg == "-g" || arg == "-github") { Process.Start("https://github.com/KilLo445/RestartSteam"); }
                if (arg == "-r" || arg == "-restart") { RestartSteam(); }
                if (arg == "-v" || arg == "-ver" || arg == "-version") { Console.WriteLine($"You have v{Version} installed"); }
            }

            Environment.Exit(0);
        }

        private static void RestartSteam()
        {
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg == "-y" || arg == "-yes") { SkipConfirmation = true; }
                if (arg == "-d" || arg == "-debug") { DebugMode = true; }
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
                    if (DebugMode == true) { Console.WriteLine("[Debug] Getting Steam install path..."); }

                    using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam"))
                    {
                        if (DebugMode == true)
                        {
                            Console.WriteLine("[Debug] Key: HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Valve\\Steam");
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
                    if (DebugMode == true) { Console.WriteLine("[Debug] Checking for Steam..."); }
                    if (Process.GetProcessesByName("steam").Length > 0) { steamRunning = true; }
                    else { steamRunning = false; }
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Starting Steam...");
                Process.Start(steamExe);

                Thread.Sleep(1000);

                while (steamRunning == false)
                {
                    if (DebugMode == true) { Console.WriteLine("[Debug] Checking for Steam..."); }
                    if (Process.GetProcessesByName("steam").Length > 0) { steamRunning = true; }
                    else { steamRunning = false; }
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
