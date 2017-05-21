using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        }

        private void Hook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            captureForm.Invalidate();

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
            redrawRectangle.Inflate(50, 50);

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
                e.Graphics.DrawLine(linePen, p1, p2);
                //e.Graphics.DrawLine(shadowPen, s1, s2);

                // TODO: Implement ruler lines
            }
        }

        public void Close()
        {
            hook.Dispose();
            Utils.DisableCaptureForm(FORM_NAME);
            isClosing();
        }

    }
}
