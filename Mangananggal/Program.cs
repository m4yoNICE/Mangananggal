using Mangananggal.Forms.Navigation;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace Mangananggal
{
    internal static class Program
    {
        public static Login loginForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginForm = new Login();
            Application.Run(new Login());
        }
    }
}
