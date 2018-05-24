using Igloo.Control.BrowserButtons;
using IndieGoat.MaterialFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Igloo.Control.Browser
{
    /// <summary>
    /// Used to seperate the IBrowserTab and the IBrowserHeader
    /// </summary>
    public class IBrowserHeader : UserControl
    {
        #region Var's

        //All header button controls
        BackButton sendBack = new BackButton();
        ForwardButton sendForward = new ForwardButton();
        HomeButton homeButton = new HomeButton();
        ReloadButton reloadButton = new ReloadButton();
        SettingsButton settingsButton = new SettingsButton();
        FlatButton VPNButton = new FlatButton();
        DownloadButton downloadButton = new DownloadButton();

        //Text for Online Searching
        MaterialTextBox text_Search = new MaterialTextBox();

        #endregion

        #region Event's

        public event EventHandler NewTabClicked;
        public event EventHandler SendBackClick;
        public event EventHandler SendForwardClick;
        public event EventHandler SendReloadClick;
        public event EventHandler SendHomeClick;
        public event EventHandler SettingsClicked;
        public event EventHandler VPNButtonClicked;
        public event EventHandler TextSearch_EnterPressed;

        #endregion 

        /// <summary>
        /// Used to initialize the IBrowserHeader
        /// </summary>
        /// <param name="StartWidth">Starting width of the browser</param>
        public IBrowserHeader(int StartWidth)
        {
            // IBrowserHeader //
            this.Width = StartWidth;
            this.Height = 33;
            this.BackColor = Color.Transparent;
            this.BackColor = Color.White;

            // Web Back Button //
            sendBack.Location = new System.Drawing.Point(0, 0);
            sendBack.Click += ((obj, args) =>
            {
                SendBackClick?.Invoke(this, new EventArgs());
            });

            this.Controls.Add(sendBack);

            // Web Forward Button //
            sendForward.Location = new System.Drawing.Point(32 * 1, 0);

            sendForward.Click += ((obj, args) =>
            {
                SendForwardClick.Invoke(this, new EventArgs());
            });

            this.Controls.Add(sendForward);

            // Reload Button //
            reloadButton.Location = new System.Drawing.Point(32 * 2, 0);
            reloadButton.Enabled = true;

            reloadButton.Click += ((obj, args) =>
            {
                SendReloadClick?.Invoke(this, new EventArgs());
            });

            this.Controls.Add(reloadButton);

            // Settings Button //
            settingsButton.Location = new Point(this.Width - 32, 0);

            settingsButton.Click += ((obj, args) =>
            {
                SettingsClicked?.Invoke(this, new EventArgs());
            });

            this.Controls.Add(settingsButton);

            downloadButton.Location = new Point(this.Width - 64, 0);
            downloadButton.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

            downloadButton.Click += (obj, args) =>
            {

            };
            this.Controls.Add(downloadButton);

            // Home Button //
            homeButton.Location = new Point(32 * 3, 0);

            homeButton.Click += ((obj, args) =>
            {
                SendHomeClick?.Invoke(this, new EventArgs());
            });

            this.Controls.Add(homeButton);

            // Text Search
            text_Search.Location = new Point(32 * 4 + 3, this.Height / 2 - (text_Search.Height / 2));
            text_Search.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
            text_Search.Font = new Font("Segoe UI", 12f);
            text_Search.Text = "";
            text_Search.Width = this.Width - (32 * 4) - 64;

            text_Search.BackColor = Color.White;
            text_Search.BorderColor = Color.White;
            text_Search.BottomBorderColor = Color.FromArgb(236, 236, 236);

            text_Search.baseControl.KeyDown += TextSearch_KeyDown;
            this.Controls.Add(text_Search);
        }

        /// <summary>
        /// Changes the loading button look depending if the browser is loading
        /// </summary>
        /// <param name="LoadState">IsLoading bool from ChromiumWebBrowser</param>
        public void ChangeReloadStatus(bool LoadState)
        {
            //Changes the look of the reload button
            if (LoadState) { reloadButton.Status = ReloadButton.LoadingStatus.Loading; }
            else
            { reloadButton.Status = ReloadButton.LoadingStatus.NotLoading; }
        }

        /// <summary>
        /// Used to invalidate control positions
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            //Use the Base Resize Event
            base.OnResize(e);

            // Settings Button //
            settingsButton.Location = new Point(this.Width - 32, 0);

            // VPN Button //
            VPNButton.Location = new Point(this.Width - 156, 0);
            VPNButton.Size = new Size(128, 28);
            VPNButton.Name = "vpn";

            // Text Search //
            text_Search.Size = new Size(this.Width - 32, 24);
        }

        /// <summary>
        /// Used to change the VPN text
        /// </summary>
        /// <param name="Text">Value to set the text of the VPN</param>
        public void ChangeVPNText(string Text)
        {
            //Changes the text of the VPN button
            VPNButton.Text = Text;
        }

        /// <summary>
        /// Get the Text of the Search Bar
        /// </summary>
        public string GetTXTSearchValue()
        {
            return text_Search.Text;
        }

        /// <summary>
        /// Changes the Search Bar text
        /// </summary>
        /// <param name="text">Value to change the Search Bar into</param>
        public void ChangeTXTSearchValue(string text)
        {
            text_Search.Text = text;
        }

        /// <summary>
        /// Triggers event
        /// </summary>
        private void TextSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextSearch_EnterPressed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Used to paint the border
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Border color
            Color bottomBorderColor = Color.FromArgb(230, 230, 230);
            Color NormalColor = Color.White;

            //Paint the botton border
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, NormalColor, 1,
                ButtonBorderStyle.Solid, NormalColor, 1, ButtonBorderStyle.Solid,
                NormalColor, 1, ButtonBorderStyle.Solid,
                bottomBorderColor, 1, ButtonBorderStyle.Solid);
        }

        public void CanGoBackBool(bool CanGoBack)
        {
            sendBack.Enabled = CanGoBack;
        }

        public void CanGoForwardBool(bool CanGoForward)
        {
            sendForward.Enabled = CanGoForward;
        }
    }
}
