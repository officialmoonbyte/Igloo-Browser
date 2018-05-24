using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Igloo.Control.Browser
{
    public class VPNToggleButton : UserControl
    {

        #region Properties

        bool isVPN;
        public bool IsVPNEnabled
        {
            get { return isVPN; }
            set { isVPN = value; this.Invalidate(); }
        }

        #endregion

        #region Required

        /// <summary>
        /// Sets the size and buffered of the control
        /// </summary>
        public VPNToggleButton()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(32, 32);
        }

        #endregion

        #region Paint

        Color CheckColor = Color.FromArgb(255, 80, 180, 80);

        /// <summary>
        /// Required event to paint the button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //Setting ANTIALIS mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (IsVPNEnabled)
            {
                Pen linepen = new Pen(new SolidBrush(CheckColor), 2);
                linepen.StartCap = LineCap.Round;
                linepen.EndCap = LineCap.Round;
            
                g.DrawLine(linepen, new Point(5, 16), new Point(14, 24));
                g.DrawLine(linepen, new Point(14, 24), new Point(24, 10));
            }
            else
            {
                int moveInt = 12; int yMove = 0; int xMove = 0;

                Bitmap shieldImage = new Bitmap(Image.FromFile(Application.StartupPath + @"\protection.ico"));
                e.Graphics.DrawImage(shieldImage, new Rectangle(this.ClientRectangle.X + (moveInt / 2) - xMove, this.ClientRectangle.Y + (moveInt / 2) - yMove,
                this.ClientRectangle.Width - moveInt, this.ClientRectangle.Height - moveInt));
            }

            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
        }

        #endregion
    }
}
