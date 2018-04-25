using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.BrowserButtons
{
    public class BackButton : UserControl
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
        public BackButton()
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

            //Default colors for the line and the ellipse.
            Color lineColor = Color.FromArgb(78, 78, 78);
            Color EllipseColor = Color.FromArgb(178, 178, 178);

            //Sets the line color to something else if the button
            //is disabled
            if (this.Enabled == false) lineColor = Color.FromArgb(178, 178, 178);

            //Paints the default control
            base.OnPaint(e);

            //Settings the vars
            Graphics g = e.Graphics;

            //Color Vars
            Color backGroundColor = Color.White;

            //Drawing the background
            g.Clear(backGroundColor);

            Pen p = new Pen(lineColor, 2);

            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;

            //Setting ANTIALIS mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Draw the line for the arrow
            g.DrawLine(p, new Point((this.Width / 3), (this.Height / 2)),
                new Point(this.Width - (this.Width / 4), (this.Height / 2)));

            //Drawing the eclipse
            g.DrawEllipse(new Pen(new SolidBrush(EllipseColor), 1), new Rectangle(this.ClientRectangle.X + 2,
                this.ClientRectangle.Y + 2,
                this.ClientRectangle.Width - 4,
                this.ClientRectangle.Height - 4));

            //Draw the right side of the arrow
            g.DrawLine(p, new Point((this.Width / 3), (this.Width / 2)),
                new Point((this.Width / 2), (this.Height / 2) - (this.Height / 5)));

            //Draw the left side of the arrow
            g.DrawLine(p, new Point((this.Width / 3), (this.Height / 2)),
                new Point((this.Width / 2), (this.Height / 2) + (this.Height / 5)));

            //Set smoothing mode and IM mode to default
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;

        }

        #endregion 
    }
}
