using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDPKeuze
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Start het hoofdvenster (FormRdpKeuze, staat in MainFormRdp.cs).
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormRdpKeuze());
        }
    }
}
