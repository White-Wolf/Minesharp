using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesharp
{
    static class Program
    {
        static Game gameWin;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Starting up...");
#if DEBUG
            Console.WriteLine("Skipping launcher and heading into game");
            Network.LoggedIn = true;
#endif
#if (!DEBUG)
            //load the launcher
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Launcher());
#endif
            if (Network.LoggedIn)
            {
                Console.WriteLine("Logged in, starting game");
                using (gameWin = new Game())
                {
                    //start the game and tell it to use the most FPS possible
                    gameWin.Run(200.0, 200.0);
                }
            }
            Console.WriteLine("Shutting down...");
        }

        public static void Close()
        {
            gameWin.Close();
        }
    }
}
