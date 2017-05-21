using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abrovink
{
    public class FormDoubleBuffered : Form
    {
        public FormDoubleBuffered() : base()
        {
            this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            //                        ControlStyles.UserPaint |
            //                        ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }

    public class PanelDoubleBuffered : Panel
    {
        public PanelDoubleBuffered() : base()
        {
            this.DoubleBuffered = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }
    }

    public class Utils
    {
        public static bool DefaultScreenOrder()
        {
            Screen primaryScreen = Screen.AllScreens[0];
            Screen otherScreen = Screen.AllScreens[1];

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary)
                {
                    primaryScreen = screen;
                }
                else
                {
                    otherScreen = screen;
                }
            }

            if (primaryScreen.Bounds.X > otherScreen.Bounds.X)
            {
                return false;
            }

            return true;
        }

        public static Color DifferenceColor(Color c)
        {
            return (((c.R + c.B + c.G) / 3) > 128) ? Color.Black : Color.White;
        }

        public static string ColorToHexString(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static string ColorToRGBString(Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        public static Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = Win32.GetDC(IntPtr.Zero);
            uint pixel = Win32.GetPixel(hdc, x, y);
            Win32.ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        public static Image CaptureWindow()
        {
            try
            {
                Bitmap screen = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height, PixelFormat.Format32bppArgb);

                using (Graphics screenGraph = Graphics.FromImage(screen))
                {
                    screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X, SystemInformation.VirtualScreen.Y, 0, 0, SystemInformation.VirtualScreen.Size, CopyPixelOperation.SourceCopy);
                }

                return screen;
            }
            catch
            {
                return new Bitmap(1, 1);
            }
        }

        public static void EnableCaptureForm(ref Form f, string name)
        {
            f = new FormDoubleBuffered();
            f.Name = name;
            f.BackColor = Color.Red;
            f.FormBorderStyle = FormBorderStyle.None;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = Screen.AllScreens[1].WorkingArea.Location;
            f.TransparencyKey = Color.Red;

            if (Utils.DefaultScreenOrder())
            {
                f.Bounds = new Rectangle(0, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            }
            else
            {
                f.Bounds = new Rectangle(-Screen.AllScreens[0].Bounds.Width, 0, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            }

            f.Capture = true;
            f.ShowInTaskbar = false;
            f.TopMost = true;
            f.Show();
        }

        public static void DisableCaptureForm(string name)
        {
            Form f = Application.OpenForms[name];
            if (f != null)
            {
                f.Close();
                f = null;
            }
        }

        public static Point TranslateDisplayPoint(Point dp)
        {
            // TODO: Refactoring needed. Better way to deal with multiple screens and translated mouse coordinates and screen positions.

            Point p = dp;

            if (Utils.DefaultScreenOrder())
            {
                p = dp;
            }
            else
            {
                p = new Point(dp.X + Screen.AllScreens[0].Bounds.Width, dp.Y);
            }

            return p;
        }

        public static double DistanceBetweenPoints(Point p1, Point p2)
        {
            return Math.Abs(Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
        }

        public static Point[] StraightenLine(Point p1, Point p2)
        {
            int diffY = Math.Abs(p1.Y - p2.Y);
            int diffX = Math.Abs(p1.X - p2.X);

            if (diffX > diffY)
            {
                // X variation is greater
                p1 = new Point(p1.X, p1.Y);
                p2 = new Point(p2.X, p1.Y);
            }
            else
            {
                // Y variation is greater
                p1 = new Point(p1.X, p1.Y);
                p2 = new Point(p1.X, p2.Y);
            }

            return new Point[] { p1, p2 };
        }
    }
}
