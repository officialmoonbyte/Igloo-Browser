using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.Browser
{
    public class HomeButton : UserControl
    {
        #region Required

        /// <summary>
        /// Used to change to default var's
        /// </summary>
        public HomeButton()
        {
            this.Size = new System.Drawing.Size(32, 32);
            this.DoubleBuffered = true;
        }

        #endregion

        #region Override Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Initializing new graphics
            Graphics g = e.Graphics;

            //Initializing colors
            Color _BackColor = Color.White;

            //Redrawing background to white
            g.Clear(_BackColor);

            //How thick the elilipse is
            int elipSize = 2;

            //Marj size used to center the ellipse farther from the edges of the control
            int MarjSize = 10;

            //The rectangle of the Ellipse
            Rectangle elipRectangle = new Rectangle(this.ClientRectangle.X + elipSize + (MarjSize / 2),
                this.ClientRectangle.Y + elipSize + (MarjSize / 2) + 6,
                this.ClientRectangle.Width - (elipSize * 2) - MarjSize,
                this.ClientRectangle.Height - (elipSize * 2) - MarjSize - 5);

            Brush RectBrush = new SolidBrush(Color.FromArgb(78, 78, 78));

            //Set the local var for the wals
            int WallSize = 2;

            //Setting ANTIALIS mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Draw the walls of the house
            ControlPaint.DrawBorder(g, elipRectangle, Color.FromArgb(78, 78, 78), WallSize, ButtonBorderStyle.Solid,
                Color.White, WallSize, ButtonBorderStyle.Solid,
                Color.Black, WallSize, ButtonBorderStyle.Solid,
                Color.Black, WallSize, ButtonBorderStyle.Solid);

            //Draw the roof of the house
            g.DrawLine(new Pen(RectBrush, 2),
                new Point(elipRectangle.X, elipRectangle.Y + 1),
                new Point(this.Width / 2, this.Height / 6));
            g.DrawLine(new Pen(RectBrush, 2),
                new Point(elipRectangle.X + elipRectangle.Width - 1, elipRectangle.Y + 1),
                new Point(this.Width / 2, this.Height / 6));

            //Draw the ending lines of the roof
            g.DrawLine(new Pen(RectBrush, 2),
                new Point(elipRectangle.X, elipRectangle.Y + 1),
                new Point(this.Width / 4 - 4, this.Height / 2 + 1));
            g.DrawLine(new Pen(RectBrush, 2),
                new Point(elipRectangle.X + elipRectangle.Width - 1, elipRectangle.Y + 1),
                new Point(elipRectangle.X + elipRectangle.Width - 1 + 3, this.Height / 2 + 1));

            //Setting ANTIALIS mode
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;

            int _doorWidth = 6;
            int _doorHeight = 8;

            //Getting the rectangle of the door
            Rectangle doorRect = new Rectangle((this.Width / 2) - (_doorWidth / 2), (elipRectangle.Y + elipRectangle.Height) - _doorHeight - 2, _doorWidth, _doorHeight);

            //Draw the door
            g.FillRectangle(RectBrush, doorRect);

        }

        #endregion
    }
}
