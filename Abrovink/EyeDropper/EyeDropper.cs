using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Abrovink.EyeDropper
{
    public partial class Widget : Form, IAbrovinkWidget
    {
        private IKeyboardMouseEvents hook;
        private Point offset = new Point(40, -30);

        private Timer timer = new Timer();

        private int previewBorderWidth = 1;
        private Color previewBorderColor = Color.FromArgb(100, 100, 100);

        Image screenshot;

        Color color;

        public Widget()
        {
            InitializeComponent();

            hook = Hook.GlobalEvents();
            hook.MouseMoveExt += Hook_MouseMoveExt;

            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();

            screenshot = CaptureAllWindows();
            //screenshot = CaptureWindow(Win32.GetDesktopWindow());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            color = GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            previewPanel.BackColor = color;
            previewLabel.Text = ColorToHexString(color);
            screenshot = CaptureAllWindows();
        }

        private void Hook_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            Point loc = Cursor.Position;
            loc.Offset(offset.X, offset.Y);
            this.Location = loc;
            this.TopMost = true;

            Bitmap previewPart = new Bitmap(58, 58);
            using (Graphics g = Graphics.FromImage(previewPart))
            {
                Point cursorLoc = Cursor.Position;



                cursorLoc.Offset((screenshot.Width - cursorLoc.X), (screenshot.Height - cursorLoc.Y));



                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(screenshot, new Rectangle(0, 0, 58, 58), new Rectangle(Cursor.Position.X - 6, Cursor.Position.Y - 6, 12, 12), GraphicsUnit.Pixel);
            }
            previewPanel.BackgroundImage = previewPart;
        }

        private void Widget_Load(object sender, EventArgs e)
        {
            Point loc = Cursor.Position;
            loc.Offset(offset.X, offset.Y);
            this.Location = loc;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBorder();
        }

        private void DrawBorder()
        {
            IntPtr hdc = Win32.GetWindowDC(this.Handle);
            Graphics g = Graphics.FromHdc(hdc);
            Pen p = new Pen(previewBorderColor, previewBorderWidth);
            Rectangle r = new Rectangle(0, 0, DisplayRectangle.Width - previewBorderWidth, DisplayRectangle.Height - previewBorderWidth);
            g.DrawRectangle(p, r);
            g.DrawLine(p, new Point(previewPanel.Width + previewBorderWidth, previewBorderWidth), new Point(previewPanel.Width + previewBorderWidth, previewPanel.Height));
            g.Dispose();
            Win32.ReleaseDC(this.Handle, hdc);
        }

        private Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = Win32.GetDC(IntPtr.Zero);
            uint pixel = Win32.GetPixel(hdc, x, y);
            Win32.ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private string ColorToHexString(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private string ColorToRGBString(System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        private Image CaptureAllWindows()
        {
            Bitmap screenshot = new Bitmap(
                SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height,
                PixelFormat.Format32bppArgb);

            Graphics screenGraph = Graphics.FromImage(screenshot);

            screenGraph.CopyFromScreen(
                SystemInformation.VirtualScreen.X,
                SystemInformation.VirtualScreen.Y,
                0,
                0,
                SystemInformation.VirtualScreen.Size,
                CopyPixelOperation.SourceCopy);

            return screenshot;
        }

        private Image CaptureWindow(IntPtr handle)
        { 
            IntPtr hdcSrc = Win32.GetWindowDC(handle);

            Win32.RECT windowRect = new Win32.RECT();
            Win32.GetWindowRect(handle, ref windowRect);

            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            IntPtr hdcDest = Win32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = Win32.CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr hOld = Win32.SelectObject(hdcDest, hBitmap);

            Win32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Win32.SRCCOPY);
            Win32.SelectObject(hdcDest, hOld);

            Win32.DeleteDC(hdcDest);
            Win32.ReleaseDC(handle, hdcSrc);

            Image img = Image.FromHbitmap(hBitmap);

            Win32.DeleteObject(hBitmap);

            return img;
        }
    }
}
