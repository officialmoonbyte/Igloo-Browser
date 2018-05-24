using System.Drawing;
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

        /// <summary>
        /// Required event to paint the button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #endregion
    }
}
