using System;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;

namespace Abrovink.EyeDropper
{
    public class PanelDoubleBuffered : Panel
    {
        public PanelDoubleBuffered() : base()
        {
            this.DoubleBuffered = true;
        }
    }

    public partial class Widget : Form, IAbrovinkWidget
    {
        private IKeyboardMouseEvents hook;
        private Point offset = new Point(30, -30);

        private Timer timer;

        private Bitmap previewImage = new Bitmap(58, 58);
        private int previewBorderWidth = 1;
        private Color previewBorderColor = Color.FromArgb(100, 100, 100);
        private Graphics previewGraphics;

        private Image screenshot;
        private Color color;
        private int screenWidth;

        private Point cursorLoc;
        private Point translatedCursorLoc;

        public Widget()
        {
            InitializeComponent();

            hook = Hook.GlobalEvents();
            hook.MouseMoveExt += Hook_MouseMoveExt;
            hook.MouseDownExt += Hook_MouseDownExt;

            screenWidth = Screen.FromControl(this).Bounds.Width;
            screenshot = CaptureWindow();

            previewPanel.Paint += PreviewPanel_Paint;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.ShowInTaskbar = false;
            this.FormClosing += Widget_FormClosing;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            screenshot.Dispose();
            screenshot = CaptureWindow();
        }

        private void Widget_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            previewImage.Dispose();
            screenshot.Dispose();
        }

        private void Widget_Load(object sender, EventArgs e)
        {
            Point loc = Cursor.Position;
            loc.Offset(offset.X, offset.Y);
            this.Location = loc;
            screenshot = CaptureWindow();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBorder();
        }

        private void Hook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            Clipboard.SetDataObject(previewLabel.Text, false, 5, 200);
            e.Handled = true;
            hook.MouseMoveExt -= Hook_MouseMoveExt;
            hook.MouseDownExt -= Hook_MouseDownExt;
            hook.Dispose();

            this.Close();
        }

        private void Hook_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            cursorLoc = Cursor.Position;
            cursorLoc.Offset(offset.X, offset.Y);
            this.Location = cursorLoc;
            this.TopMost = true;

            previewImage = new Bitmap(58, 58);
            using (previewGraphics = Graphics.FromImage(previewImage))
            {
                translatedCursorLoc = Cursor.Position;

                if(screenWidth != screenshot.Width)
                    translatedCursorLoc = new Point(Cursor.Position.X + screenWidth, Cursor.Position.Y);

                previewGraphics.SmoothingMode = SmoothingMode.HighQuality;
                previewGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                previewGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                previewGraphics.DrawImage(screenshot, new Rectangle(0, 0, 58, 58), new Rectangle(translatedCursorLoc.X - 3, translatedCursorLoc.Y - 3, 7, 7), GraphicsUnit.Pixel);
            }

            color = previewImage.GetPixel(previewImage.Width / 2, previewImage.Height / 2);
            previewLabel.Text = ColorToHexString(color);

            previewPanel.BackgroundImage = previewImage;
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

        private void PreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            Color crosshairColor = DifferenceColor(color);
            Point p1 = new Point(previewPanel.Width / 2, 25);
            Point p2 = new Point(previewPanel.Width / 2, previewPanel.Height - 25);
            Point p3 = new Point(25, previewPanel.Height / 2);
            Point p4 = new Point(previewPanel.Width - 25, previewPanel.Height / 2);
            e.Graphics.DrawLine(new Pen(crosshairColor, 1), p1, p2);
            e.Graphics.DrawLine(new Pen(crosshairColor, 1), p3, p4);
        }

        private Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = Win32.GetDC(IntPtr.Zero);
            uint pixel = Win32.GetPixel(hdc, x, y);
            Win32.ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private string ColorToHexString(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private string ColorToRGBString(Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        private Image CaptureWindow()
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

        private static Color DifferenceColor(Color c)
        {
            return (((c.R + c.B + c.G) / 3) > 128) ? Color.Black : Color.White;
        }
    }
}
