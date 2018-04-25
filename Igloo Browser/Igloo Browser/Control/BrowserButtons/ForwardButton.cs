using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.BrowserButtons
{
    public class ForwardButton : UserControl
    {
        #region Required

        /// <summary>
        /// Initialize the Back Button
        /// </summary>
        public ForwardButton()
        {
            this.Size = new Size(32, 32);
            this.DoubleBuffered = true;
            this.Enabled = false;
        }

        #endregion

        #region Override Paint

        /// <summary>
        /// Paints the control
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Paints the default control
            base.OnPaint(e);

            //Default colors for the line and the ellipse.
            Color lineColor = Color.FromArgb(78, 78, 78);

            //Sets the line color to something else if the button
            //is disabled
            if (this.Enabled == false) lineColor = Color.FromArgb(178, 178, 178);

            //Settings the vars
            Graphics g = e.Graphics;

            //Color Vars
            Color backGroundColor = Color.White;

            //Drawing the background
            g.Clear(backGroundColor);

            //Draw the line for the arrow
            //Rectangle arrowLine = new Rectangle(new Point(4, 15), new Size(26, 3));
            //g.FillRectangle(new SolidBrush(Color.Black), arrowLine);

            //Setting ANTIALIS mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Pen r = new Pen(Color.Red);
            Pen p = new Pen(lineColor, 2);

            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;

            //Draw the line for the arrow
            g.DrawLine(p, new Point((this.Width / 2) - 8, (this.Height / 2)), new Point((this.Width / 2) + 9, (this.Height / 2)));

            //Draw the right side of the arrow
            g.DrawLine(p, new Point((this.Width / 2) + 9, (this.Height / 2)), new Point((this.Width / 2),
                (this.Height / 2) - 8));

            //Draw the left side of the arrow
            g.DrawLine(p, new Point((this.Width / 2) + 9, (this.Height / 2)), new Point((this.Width / 2),
                (this.Height / 2) + 8));
        }

        protected GraphicsPath GetRoundedLine(PointF[] points, float cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            PointF previousEndPoint = PointF.Empty;
            for (int i = 1; i < points.Length; i++)
            {
                PointF startPoint = points[i - 1];
                PointF endPoint = points[i];

                if (i > 1)
                {
                    // shorten start point and add bezier curve for all but the first line segment:
                    PointF cornerPoint = startPoint;
                    LengthenLine(endPoint, ref startPoint, -cornerRadius);
                    PointF controlPoint1 = cornerPoint;
                    PointF controlPoint2 = cornerPoint;
                    LengthenLine(previousEndPoint, ref controlPoint1, -cornerRadius / 2);
                    LengthenLine(startPoint, ref controlPoint2, -cornerRadius / 2);
                    path.AddBezier(previousEndPoint, controlPoint1, controlPoint2, startPoint);
                }
                if (i + 1 < points.Length) // shorten end point of all but the last line segment.
                    LengthenLine(startPoint, ref endPoint, -cornerRadius);

                path.AddLine(startPoint, endPoint);
                previousEndPoint = endPoint;
            }
            return path;
        }

        public void LengthenLine(PointF startPoint, ref PointF endPoint, float pixelCount)
        {
            if (startPoint.Equals(endPoint))
                return; // not a line

            double dx = endPoint.X - startPoint.X;
            double dy = endPoint.Y - startPoint.Y;
            if (dx == 0)
            {
                // vertical line:
                if (endPoint.Y < startPoint.Y)
                    endPoint.Y -= pixelCount;
                else
                    endPoint.Y += pixelCount;
            }
            else if (dy == 0)
            {
                // horizontal line:
                if (endPoint.X < startPoint.X)
                    endPoint.X -= pixelCount;
                else
                    endPoint.X += pixelCount;
            }
            else
            {
                // non-horizontal, non-vertical line:
                double length = Math.Sqrt(dx * dx + dy * dy);
                double scale = (length + pixelCount) / length;
                dx *= scale;
                dy *= scale;
                endPoint.X = startPoint.X + Convert.ToSingle(dx);
                endPoint.Y = startPoint.Y + Convert.ToSingle(dy);
            }
        }

        #endregion 
    }
}
