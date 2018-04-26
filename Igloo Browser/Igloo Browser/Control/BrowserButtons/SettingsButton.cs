using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Igloo.Control.BrowserButtons
{
    public class SettingsButton : UserControl
    {

        #region Requried

        //Initialize the Button
        public SettingsButton()
        {
            this.Size = new Size(32, 32);
            this.DoubleBuffered = true;
        }

        #endregion

        #region Override Paint

        /// <summary>
        /// Override the paint of the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            //Default color
            Color drawLineColor = Color.FromArgb(78, 78, 78);

            //Paint the default control
            base.OnPaint(e);

            //Getting the local var for graphics
            Graphics g = e.Graphics;

            //Draw the background
            g.Clear(Color.White);

            //Initialize a new pen for the line
            Pen p = new Pen(drawLineColor, 2);

            //Set ANTIALIS mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int Modif = 4;

            //Draw the image
            g.DrawImage(Properties.Resources.settingslogo.ToBitmap(), new Rectangle(ClientRectangle.X + Modif,
                ClientRectangle.Y + Modif,
                ClientRectangle.Width - (Modif * 2),
                ClientRectangle.Height - (Modif * 2)));
        }

        #endregion 
    }
}
