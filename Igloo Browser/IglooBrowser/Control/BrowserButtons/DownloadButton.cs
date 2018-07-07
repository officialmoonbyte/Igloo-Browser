using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.Browser
{
    public class DownloadButton : UserControl
    {

        #region Required

        /// <summary>
        /// Sets the size of the button and double buffer(s) it
        /// </summary>
        public DownloadButton()
        {
            this.Size = new System.Drawing.Size(32, 32);
            this.DoubleBuffered = true;
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the background, font and icon of the button.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Line Color of the lines
            Color lineColor = Color.FromArgb(178, 178, 178);

            //Initializes the new graphics object
            Graphics g = e.Graphics;

            //Draws the background of the button
            g.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Pen backDrawPen = new Pen(new SolidBrush(lineColor), 2);
            backDrawPen.StartCap = LineCap.Round;
            backDrawPen.EndCap = LineCap.Round;

            int PlusY = 3;

            g.DrawLine(backDrawPen, new Point((this.Width / 2), 4 + PlusY), new Point(this.Width / 2, 18 + PlusY)); //Draws center line
            g.DrawLine(backDrawPen, new Point(this.Width / 2, 18 + PlusY), new Point(this.Width / 3 - 1, 10 + PlusY)); //Draws left arrow
            g.DrawLine(backDrawPen, new Point(this.Width / 2, 18 + PlusY), new Point((this.Width / 2) + (this.Width / 3) - 3, 10 + PlusY)); //Draws right arrow
            g.DrawLine(backDrawPen, new Point(8, 22 + PlusY), new Point(32 - 8, 22 + PlusY));

            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
        }

        #endregion
    }
}
