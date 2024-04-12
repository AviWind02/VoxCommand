using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxCommand.Retriever_Class;
using VoxCommand.Speech_Class;

namespace VoxCommand
{
    internal static class Program
    {
        private static SteamGamesRetriever steamGamesRetriever;

   

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
  
            Console.WriteLine("Start");
            var speechTask = new Speech_recognition().run(); // Start the speech recognition task
            steamGamesRetriever = new SteamGamesRetriever();// Trying to get all steam games from all steams lib(Drives) on the computer
            steamGamesRetriever.run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            await speechTask; // wait for the speech task to complete after the form is closed
        
        }

        public static SteamGamesRetriever getSteamGamesRetriever()
        {

            return steamGamesRetriever;
        }
    }
}
