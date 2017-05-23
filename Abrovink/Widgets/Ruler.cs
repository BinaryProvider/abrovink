using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abrovink.Ruler
{
    public class Widget : IAbrovinkWidget
    {
        public event WidgetClosing isClosing;

        private const string FORM_NAME = "Ruler_CaptureForm";

        private IKeyboardMouseEvents hook;

        private Form captureForm;
        private Point p1, p2, s1, s2;
        private Point prev1, prev2;

        private Timer timer;

        private Pen linePen = new Pen(Color.White, 1);
        private Pen shadowPen = new Pen(Color.Black, 1);

        private bool drawLine = false;
        private bool forceStraight = false;
        private Rectangle redrawRectangle;

        private int length = 0;

        public Widget()
        {
            Utils.EnableCaptureForm(ref captureForm, FORM_NAME);
            if (captureForm != null)
            {
                captureForm.Paint += CaptureForm_Paint;
            }

            hook = Hook.GlobalEvents();
            hook.KeyDown += Hook_KeyDown;
            hook.KeyUp += Hook_KeyUp;
            hook.MouseDownExt += Hook_MouseDownExt;
            hook.MouseMoveExt += Hook_MouseMoveExt;
            hook.MouseUpExt += Hook_MouseUpExt;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Hook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            p1 = Cursor.Position;
            p2 = Cursor.Position;

            if(!Utils.DefaultScreenOrder())
            {
                p1 = Utils.TranslateDisplayPoint(p1);
                p2 = Utils.TranslateDisplayPoint(p2);
            }

            drawLine = true;
        }

        private void Hook_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            prev1 = p1;
            prev2 = p2;

            p2 = Cursor.Position;

            if (!Utils.DefaultScreenOrder())
            {
                p2 = Utils.TranslateDisplayPoint(p2);
            }

            if (forceStraight)
            {
                Point[] points = Utils.StraightenLine(p1, p2);
                p1 = points[0];
                p2 = points[1];
            }

            s1 = p1;
            s2 = p2;

            s1.Offset(1, 1);
            s2.Offset(1, 1);

            length = (int)Utils.DistanceBetweenPoints(p1, p2);

            GraphicsPath path = new GraphicsPath();
            path.AddLine(p1, p2);
            path.AddLine(prev1, prev2);

            redrawRectangle = Rectangle.Round(path.GetBounds());
            redrawRectangle.Inflate(100, 100);

            captureForm.Invalidate(new Region(redrawRectangle), false);
        }

        private void Hook_MouseUpExt(object sender, MouseEventExtArgs e)
        {
            drawLine = false;
            hook.MouseDownExt -= Hook_MouseDownExt;
            hook.MouseMoveExt -= Hook_MouseMoveExt;
            hook.MouseUpExt -= Hook_MouseUpExt;
            Close();
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                forceStraight = true;
                Point[] points = Utils.StraightenLine(p1, p2);
                p1 = points[0];
                p2 = points[1];
            }
        }
        private void Hook_KeyUp(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                forceStraight = false;
            }
        }

        private void CaptureForm_Paint(object sender, PaintEventArgs e)
        {
            if (drawLine)
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                e.Graphics.DrawLine(linePen, p1, p2);
                e.Graphics.DrawLine(shadowPen, s1, s2);

                double angle = DegreeToRadian(AngleBetweenPoints(p1, p2));

                for(int i = 0; i < length; i++)
                {
                    int length = 0;

                    if (i % 5 == 0)
                    {
                        length = 2;
                    }

                    if (i % 10 == 0)
                    {
                        length = 6;
                    }

                    if (i % 20 == 0)
                    {
                        length = 10;
                    }

                    if (i % 100 == 0)
                    {
                        length = 15;
                    }

                    Point p = CalculatePoint(p1, p2, i);

                    int x = (int)(p.X + Math.Cos(angle - DegreeToRadian(90)) * length);
                    int y = (int)(p.Y + Math.Sin(angle - DegreeToRadian(90)) * length);
                    Point lp1 = new Point(x, y);

                    x = (int)(p.X - Math.Cos(angle - DegreeToRadian(90)) * length);
                    y = (int)(p.Y - Math.Sin(angle - DegreeToRadian(90)) * length);
                    Point lp2 = new Point(x, y);

                    x = (int)(p.X + Math.Cos(angle - DegreeToRadian(90)) * 30);
                    y = (int)(p.Y + Math.Sin(angle - DegreeToRadian(90)) * 30);
                    Point tp1 = new Point(x, y);

                    x = (int)(p.X - Math.Cos(angle - DegreeToRadian(90)) * 30);
                    y = (int)(p.Y - Math.Sin(angle - DegreeToRadian(90)) * 30);
                    Point tp2 = new Point(x, y);

                    e.Graphics.DrawLine(linePen, p, lp1);
                    e.Graphics.DrawLine(shadowPen, p, lp2);

                    //if (i % 100 == 0)
                    //{
                    //    StringFormat stringFormat = new StringFormat();
                    //    stringFormat.Alignment = StringAlignment.Center;
                    //    stringFormat.LineAlignment = StringAlignment.Center;

                    //    e.Graphics.DrawString(i.ToString(), new Font("Courier New", 10, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, tp1, stringFormat);

                    //    e.Graphics.DrawString(i.ToString(), new Font("Courier New", 10, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, tp2, stringFormat);
                    //}
               
                }

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                Point tp = p2;
                tp.Offset(40, 0);

                e.Graphics.DrawString(length.ToString(), new Font("Courier New", 20, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, tp, stringFormat);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            captureForm.Invalidate();
        }

        public void Close()
        {
            hook.Dispose();
            Utils.DisableCaptureForm(FORM_NAME);
            isClosing();
        }

        private Point CalculatePoint(Point a, Point b, int distance)
        {
            Point newPoint = new Point(0, 0);
            double magnitude = Math.Sqrt(Math.Pow((b.Y - a.Y), 2) + Math.Pow((b.X - a.X), 2));
            if (magnitude != 0)
            {
                newPoint.X = (int)(a.X + (distance * ((b.X - a.X) / magnitude)));
                newPoint.Y = (int)(a.Y + (distance * ((b.Y - a.Y) / magnitude)));
            }
            return newPoint;
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double AngleBetweenPoints(Point a, Point b)
        {
            float x = b.X - a.X;
            float y = b.Y - a.Y;
            return Math.Atan2(y, x) * 180.0 / Math.PI;
        }

    }
}
