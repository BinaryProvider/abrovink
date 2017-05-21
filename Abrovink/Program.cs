using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abrovink
{
    static class Program
    {
        static TrayApp app;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            using (app = new TrayApp())
            {
                app.Start();
                Application.Run();
            }
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            app.Dispose();
        }

    }
}
