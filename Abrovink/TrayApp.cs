using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Abrovink
{
    class TrayApp : IDisposable
    {
        NotifyIcon icon;

        IKeyboardMouseEvents hook;

        public TrayApp()
        {
            icon = new NotifyIcon();
        }

        public void Start()
        {
            hook = Hook.GlobalEvents();
            icon.Icon = Properties.Resources.TempIcon;
            icon.Visible = true;

            EyeDropper.Widget frm = new EyeDropper.Widget();
            frm.Show();
        }

        public void Dispose()
        {
            icon.Dispose();
        }
    }
}
