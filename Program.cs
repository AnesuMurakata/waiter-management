using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using INF2011S_Workshop8_WaS7_PIISOL.PresentationLayer;

namespace INF2011S_Workshop8_WaS7_PIISOL
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
            Application.Run(new EmployeesMDIParent());
        }
    }
}
