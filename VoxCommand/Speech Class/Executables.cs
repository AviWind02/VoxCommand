using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxCommand.Speech_Class
{
    internal class Executables
    {
        public string[] executables = { "open", "close", "kill" };
        public static void ExecuteApplicationAction(string action, string appName, string executablePath)
        {
            Console.WriteLine($"Executing action '{action}' on '{appName}'");

            // Find the process by its name (without .exe extension)
            var processes = Process.GetProcessesByName(appName.Replace(".exe", ""));
            //string executableNameWithExtension = Path.GetFileName(executablePath); // This gives "Spotify.exe"
            //var processes = Process.GetProcessesByName(GetProcessNameFromExecutablePath(executablePath).Replace(".exe", "")); // This gives "Spotify"
            Console.WriteLine($"Found {processes.Length} process(es) with the name '{appName}'.");

            switch (action)
            {
                case "open":
                    Console.WriteLine($"Opening application '{appName}'.");
                    Process.Start(executablePath); //Open the application
                 break;

                case "close":
                     processes = Process.GetProcessesByName(GetProcessNameFromExecutablePath(executablePath).Replace(".exe", "")); // This gives "Spotify"
                    Console.WriteLine($"Found {processes.Length} process(es) with the name '{appName}'.");
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"Closing application '{appName}' (process ID: {process.Id}).");
                        process.CloseMainWindow(); //Close the application gracefully
                    }
                    break;

                case "kill":
                     processes = Process.GetProcessesByName(GetProcessNameFromExecutablePath(executablePath).Replace(".exe", "")); // This gives "Spotify"
                    Console.WriteLine($"Found {processes.Length} process(es) with the name '{appName}'.");
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"Killing application '{appName}' (process ID: {process.Id}).");
                        process.Kill(); //Forcefully close the application
                    }
                    break;

                default:
                    Console.WriteLine("Unrecognized action.");
                    break;
            }
        }

        private static string GetProcessNameFromExecutablePath(string executablePath)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainModule.FileName == executablePath)
                    {
                        Console.WriteLine($"Found {process.ProcessName} with path {executablePath}.");
                        return process.ProcessName;
                    }
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // This can happen if you try to access a process that is running with higher privileges.
                    Console.WriteLine($"Access denied to process: {process.ProcessName} - {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle or log other kinds of exceptions
                    Console.WriteLine($"Error accessing process: {process.ProcessName} - {ex.Message}");
                }
            }
            return executablePath;// Just return this if it fails
        }

    }
}
