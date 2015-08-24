using System;
using System.Windows.Forms;

namespace VoxVisio
{
    static class Program
    {
        private static MainEngine _mainEngine;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            _mainEngine = new MainEngine();


            Application.Run(new VoxVisio());
            
        }

        
    }
}
