using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Reflection;
using System.Drawing;
using System.Collections.Specialized;
using Abrovink.Widgets;
using System.Configuration;

namespace Abrovink
{
    class TrayApp : IDisposable
    {
        private NotifyIcon icon;
        private IKeyboardMouseEvents hook;
        private GlobalCursor globalCursor = new GlobalCursor();

        private bool activeWidget = false;

        private System.Windows.Input.KeyGestureConverter kgConverter = new System.Windows.Input.KeyGestureConverter();
        private Dictionary<WidgetType, System.Windows.Input.KeyGesture> hotkeys = new Dictionary<WidgetType, System.Windows.Input.KeyGesture>();

        public TrayApp()
        {
            icon = new NotifyIcon();
        }

        public void Start()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                Properties.Settings.Default.Reset();

            LoadHotkeys();

            hook = Hook.GlobalEvents();
            hook.KeyDown += Hook_KeyDown;

            SetTrayIcon(null);
            icon.Visible = true;

            icon.ContextMenu = SetupContextMenu();
        }

        private ContextMenu SetupContextMenu()
        {
            var menu = new ContextMenu();

            MenuItem item;

            item = new MenuItem("Options", menu_Click);
            menu.MenuItems.Add(item);

            item = new MenuItem("-");
            menu.MenuItems.Add(item);

            item = new MenuItem("Exit", menu_Click);
            menu.MenuItems.Add(item);

            return menu;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;

            switch(mi.Text)
            {
                case "Options":
                    if (Application.OpenForms["Options"] == null)
                    {
                        var frm = new Options();
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadHotkeys();
                        }
                    }
                    break;

                case "Exit":
                    Application.Exit();
                    break;

                default:
                    break;
            }
        }

        public void LoadHotkeys()
        {
            Properties.Settings settings = Properties.Settings.Default;
            hotkeys = new Dictionary<WidgetType, System.Windows.Input.KeyGesture>();

            foreach (WidgetType type in Enum.GetValues(typeof(WidgetType)))
            {
                var strOptions = settings["Options_" + (int)type].ToString();
                var strHotkey = "";

                if (string.IsNullOrEmpty(strOptions))
                {
                    // Default hotkeys
                    strHotkey = "Ctrl+Alt+" + ((int)type + 1);
                }
                else
                {
                    // Custom hotkeys
                    var data = settings["Options_" + (int)type].ToString();
                    if(!string.IsNullOrEmpty(data))
                    {
                        strHotkey = OptionsData.LoadDataString(data, "hotkey");
                    }
                }

                var gesture = kgConverter.ConvertFromString(strHotkey) as System.Windows.Input.KeyGesture;
                if (gesture != null)
                {
                    hotkeys.Add(type, gesture);
                }
            }
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!activeWidget)
                {
                    var wpfKey = System.Windows.Input.KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
                    var wpfArgs = new System.Windows.Input.KeyEventArgs(System.Windows.Input.Keyboard.PrimaryDevice,
                                                                        new System.Windows.Interop.HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero),
                                                                        0,
                                                                        wpfKey);

                    foreach (KeyValuePair<WidgetType, System.Windows.Input.KeyGesture> hotkey in hotkeys)
                    {
                        if (hotkey.Value.Matches(null, wpfArgs))
                        {
                            LoadWidget(hotkey.Key);
                        }
                    }
                }
            }
            catch
            {
                CleanUpWidget();
            }
        }

        private void LoadWidget(WidgetType type)
        {
            if (Application.OpenForms["Options"] == null)
            {
                activeWidget = true;

                SetTrayIcon(type);

                switch (type)
                {
                    case WidgetType.EyeDropper:
                        globalCursor.Change(@"%systemroot%\Cursors\cross_i.cur");
                        var eyedropper = new EyeDropper();
                        eyedropper.Show();
                        ((IAbrovinkWidget)eyedropper).isClosing += CleanUpWidget;
                        break;

                    case WidgetType.Ruler:
                        globalCursor.Change(@"%systemroot%\Cursors\cross_i.cur");
                        var ruler = new Ruler();
                        ruler.isClosing += CleanUpWidget;
                        break;

                    default:
                        break;
                }
            }
        }

        private void SetTrayIcon(WidgetType? widget)
        {
            if (widget == null)
            {
                icon.Icon = Properties.Resources.Icon_Toolbox;
                return;
            }

            switch (widget)
            {
                case WidgetType.EyeDropper:
                    icon.Icon = Properties.Resources.Icon_Eyedropper;
                    break;

                case WidgetType.Ruler:
                    icon.Icon = Properties.Resources.Icon_Ruler;
                    break;

                default:
                    icon.Icon = Properties.Resources.Icon_Toolbox;
                    break;
            }
        }

        private void CleanUpWidget()
        {
            activeWidget = false;
            SetTrayIcon(null);
            globalCursor.ResetToDefault();
        }

        public void Dispose()
        {
            icon.Visible = false;
            icon.Dispose();
        }
    }
}
