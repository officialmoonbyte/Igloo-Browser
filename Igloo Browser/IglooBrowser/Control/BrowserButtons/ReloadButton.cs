using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.BrowserButtons
{
    public class ReloadButton : UserControl
    {
        #region Vars

        public enum LoadingStatus
        {
            Loading, NotLoading
        }

        private LoadingStatus _status = LoadingStatus.NotLoading;

        #endregion

        #region Properties

        /// <summary>
        /// Used to invalidate the control on the StatusChangeEvent
        /// </summary>
        public LoadingStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Required

        /// <summary>
        /// Initialize the Back Button
        /// </summary>
        public ReloadButton()
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

            if (_status == LoadingStatus.NotLoading)
            {
                //Paints the default control
                base.OnPaint(e);

                //The color of the Ellipse and the lines
                Color LineColor = Color.FromArgb(78, 78, 78);

                //Change the line color if the control is not enabled
                if (!this.Enabled) LineColor = Color.FromArgb(178, 178, 178);

                //Settings the vars
                Graphics g = e.Graphics;

                //Color Vars
                Color backGroundColor = Color.White;

                //Drawing the background
                g.Clear(backGroundColor);

                //Setting ANTIALIS mode
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //How thick the elilipse is
                int elipSize = 2;

                //How thick the Dev line is
                int devSize = 6;

                //Marj size used to center the ellipse farther from the edges of the control
                int MarjSize = 10;

                //Pen used to draw the Ellipse
                Pen elpsPen = new Pen(LineColor, elipSize);

                //Pen used to draw the devis line
                Pen DevPen = new Pen(Color.White, devSize);

                //The rectangle of the Ellipse
                Rectangle elipRectangle = new Rectangle(this.ClientRectangle.X + elipSize + (MarjSize / 2),
                    this.ClientRectangle.Y + elipSize + (MarjSize / 2),
                    this.ClientRectangle.Width - (elipSize * 2) - MarjSize,
                    this.ClientRectangle.Height - (elipSize * 2) - MarjSize);

                //Draw the ellips of the reload button
                g.DrawEllipse(elpsPen, elipRectangle);

                //Draw the devis line
                g.DrawLine(DevPen, new Point(this.Width - 2, this.Width - (this.Height / 3) - 3),
                    new Point(this.Width / 2, this.Width - (this.Height / 3) - 4));

                //Draw the left arrow
                g.DrawLine(elpsPen, new Point(this.Width - 7, this.Width - (this.Height / 3) - 18),
                    new Point(this.Width - 7, this.Height / 2));

                //Draw the right arrow
                g.DrawLine(elpsPen, new Point(this.Width / 2 - 1, this.Height / 2 - 1),
                    new Point(this.Width - 8, this.Height / 2 - 1));
            }
            else
            {
                //How thick the elilipse is
                int elipSize = 2;

                //Marj size used to center the ellipse farther from the edges of the control
                int MarjSize = 10;

                //The rectangle of the Ellipse
                Rectangle elipRectangle = new Rectangle(this.ClientRectangle.X + elipSize + (MarjSize / 2) - 2,
                    this.ClientRectangle.Y + elipSize + (MarjSize / 2),
                    this.ClientRectangle.Width - (elipSize * 2) - MarjSize,
                    this.ClientRectangle.Height - (elipSize * 2) - MarjSize);

                //Initializing the graphics
                Graphics g = e.Graphics;

                Color PrimaryColor = Color.FromArgb(75, 75, 75);

                //Initializing the StringFormat for drawing the string
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;

                //Initializing the Font of the string
                Font buttonFont = new Font("Segoe UI", 18);

                //Draw the close button
                g.DrawString("X",
                    buttonFont,
                    new SolidBrush(PrimaryColor),
                    elipRectangle,
                    stringFormat);

            }
        }

        #endregion 
    }
}
