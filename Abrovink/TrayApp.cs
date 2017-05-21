using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Reflection;
using System.Drawing;

namespace Abrovink
{
    class TrayApp : IDisposable
    {
        private NotifyIcon icon;
        private IKeyboardMouseEvents hook;
        private GlobalCursor globalCursor = new GlobalCursor();
        private bool activeTool = false;

        public TrayApp()
        {
            icon = new NotifyIcon();
        }

        public void Start()
        {
            hook = Hook.GlobalEvents();
            hook.KeyDown += Hook_KeyDown;

            icon.Icon = Properties.Resources.TempIcon;
            icon.Visible = true;

            icon.ContextMenu = SetupContextMenu();
        }

        private ContextMenu SetupContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem item = new MenuItem("Exit", menu_Click);
            item.Tag = 0;
            menu.MenuItems.Add(item);

            return menu;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            int id = int.Parse(mi.Tag.ToString());

            switch(id)
            {
                // Exit
                case 0:
                    Application.Exit();
                    break;

                default:
                    break;
            }
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!activeTool)
                {
                    if ((Control.ModifierKeys == Keys.Control) && e.KeyCode == Keys.D1)
                    {
                        activeTool = true;
                        globalCursor.Change(@"%systemroot%\Cursors\cross_i.cur");
                        EyeDropper.Widget widget = new EyeDropper.Widget();
                        widget.Show();
                        ((IAbrovinkWidget)widget).isClosing += CleanUpWidget;
                    }
                    else if ((Control.ModifierKeys == Keys.Control) && e.KeyCode == Keys.D2)
                    {
                        activeTool = true;
                        Ruler.Widget widget = new Ruler.Widget();
                        widget.isClosing += CleanUpWidget;
                    }
                }
            }
            catch
            {
                globalCursor.ResetToDefault();
            }
        }

        private void CleanUpWidget()
        {
            activeTool = false;
            globalCursor.ResetToDefault();
        }

        public void Dispose()
        {
            icon.Visible = false;
            icon.Dispose();
        }
    }
}
