using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abrovink
{
    class GlobalCursor
    {
        private Dictionary<string, string> paths = new Dictionary<string, string>();

        public void Change(string cursor)
        {
            var pRegKey = Registry.CurrentUser;
            pRegKey = pRegKey.OpenSubKey(@"Control Panel\Cursors");
            paths.Clear();
            foreach (var key in pRegKey.GetValueNames())
            {
                var _key = pRegKey.GetValue(key);
                paths.Add(key, _key.ToString());
                var val = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors", key, null);
                Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors", key, cursor);
            }

            Win32.SystemParametersInfo(Win32.SPI_SETCURSORS, 0, null, Win32.SPIF_UPDATEINIFILE | Win32.SPIF_SENDCHANGE);
        }

        public void ResetToDefault()
        {
            var pRegKey = Registry.CurrentUser;
            pRegKey = pRegKey.OpenSubKey(@"Control Panel\Cursors");
            foreach (string key in paths.Keys)
            {
                var path = paths[key];
                Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors", key, path);
            }
            Win32.SystemParametersInfo(Win32.SPI_SETCURSORS, 0, null, Win32.SPIF_UPDATEINIFILE | Win32.SPIF_SENDCHANGE);
        }
    }
}
