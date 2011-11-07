using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Game gameWin = new Game())
            {
                gameWin.Run(200.0, 200.0);
            }
        }
    }
}
