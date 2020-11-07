using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LOGISTIC.UI.Import;

namespace LOGISTIC.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogIn());
        }
    }
}
