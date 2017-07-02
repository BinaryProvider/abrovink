using System;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;

namespace Abrovink.Widgets
{
    public partial class EyeDropper : Form, IAbrovinkWidget
    {
        private WidgetType type = WidgetType.EyeDropper;

        public WidgetType Type
        {
            get { return type; }
            set { }
        }

        public event WidgetClosing isClosing;

        private const string FORM_NAME = "EyeDropper_CaptureForm";

        private IKeyboardMouseEvents hook;
        private Point offset = new Point(30, -30);

        private Timer timer;

        private Bitmap previewImage = new Bitmap(58, 58);
        private int previewBorderWidth = 1;
        private Color previewBorderColor = Color.FromArgb(100, 100, 100);
        private Graphics previewGraphics;
        private Size previewResolution = new Size(3, 3);

        private Image screenshot;
        private Color color;
        private int screenWidth;

        private Point cursorLoc;
        private Point translatedCursorLoc;

        private Form captureForm = null;

        public EyeDropper()
        {
            InitializeComponent();

            var resolution = OptionsData.LoadDataInt(Properties.Settings.Default["Options_" + (int)type].ToString(), "resolution");
            if(resolution != -1)
            {
                previewResolution = new Size(resolution, resolution);
            }

            hook = Hook.GlobalEvents();
            hook.MouseMoveExt += Hook_MouseMoveExt;
            hook.MouseDownExt += Hook_MouseDownExt;

            screenWidth = Screen.FromControl(this).Bounds.Width;
            screenshot = Utils.CaptureWindow();

            previewPanel.Paint += PreviewPanel_Paint;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            Utils.EnableCaptureForm(ref captureForm, FORM_NAME);

            this.ShowInTaskbar = false;
            this.FormClosing += Widget_FormClosing;
        }

        private void Widget_Load(object sender, EventArgs e)
        {
            var loc = Cursor.Position;
            loc.Offset(offset.X, offset.Y);
            this.Location = loc;
            screenshot = Utils.CaptureWindow();
        }

        private void Widget_FormClosing(object sender, FormClosingEventArgs e)
        {
            hook.Dispose();
            timer.Stop();
            previewImage.Dispose();
            screenshot.Dispose();
            isClosing();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBorder();
        }

        private void PreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            var crosshairColor = Utils.DifferenceColor(color);
            var p1 = new Point(previewPanel.Width / 2, 25);
            var p2 = new Point(previewPanel.Width / 2, previewPanel.Height - 25);
            var p3 = new Point(25, previewPanel.Height / 2);
            var p4 = new Point(previewPanel.Width - 25, previewPanel.Height / 2);
            e.Graphics.DrawLine(new Pen(crosshairColor, 1), p1, p2);
            e.Graphics.DrawLine(new Pen(crosshairColor, 1), p3, p4);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            screenshot.Dispose();
            screenshot = Utils.CaptureWindow();
        }

        private void Hook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            Clipboard.SetDataObject(previewLabel.Text, false, 5, 200);
            e.Handled = true;
            hook.MouseMoveExt -= Hook_MouseMoveExt;
            hook.MouseDownExt -= Hook_MouseDownExt;
            hook.Dispose();

            Utils.DisableCaptureForm(FORM_NAME);

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

                if (Screen.AllScreens.Length > 1)
                {
                    translatedCursorLoc = Utils.TranslateDisplayPoint(Cursor.Position);
                    //translatedCursorLoc = new Point(Cursor.Position.X + screenWidth, Cursor.Position.Y);
                }

                previewGraphics.SmoothingMode = SmoothingMode.HighQuality;
                previewGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                previewGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                previewGraphics.DrawImage(screenshot, new Rectangle(0, 0, 58, 58), new Rectangle(translatedCursorLoc.X - (previewResolution.Width / 2), translatedCursorLoc.Y - (previewResolution.Height / 2), previewResolution.Width, previewResolution.Height), GraphicsUnit.Pixel);
            }

            color = previewImage.GetPixel(previewImage.Width / 2, previewImage.Height / 2);
            previewLabel.Text = Utils.ColorToHexString(color);

            previewPanel.BackgroundImage = previewImage;
        }

        private void DrawBorder()
        {
            var hdc = Win32.GetWindowDC(this.Handle);
            var g = Graphics.FromHdc(hdc);
            var p = new Pen(previewBorderColor, previewBorderWidth);

            var r = new Rectangle(0, 0, DisplayRectangle.Width - previewBorderWidth, DisplayRectangle.Height - previewBorderWidth);
            g.DrawRectangle(p, r);

            g.DrawLine(p, new Point(previewPanel.Width + previewBorderWidth, previewBorderWidth), new Point(previewPanel.Width + previewBorderWidth, previewPanel.Height));

            g.Dispose();
            Win32.ReleaseDC(this.Handle, hdc);
        }

    }
}
