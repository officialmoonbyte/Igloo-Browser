namespace Igloo.Download
{
    /// <summary>
    /// Used for the help of displaying a new download item.
    /// </summary>
    public class IDownloadControl : UserControl
    {
        #region Var's

        //Title label for the download
        MaterialLabel lbl_Title = new MaterialLabel();

        //Progress bar for the doInstall-Package MaterialFramework -Version 4.0.2wnload.
        MaterialProgressBar pgb_Download = new MaterialProgressBar();

        //Private Width and Height of this control
        int p_Width = 200;
        int p_Height = 49;

        #endregion

        #region Required

        /// <summary>
        /// Calling of the IDownloadControl (Initialization)
        /// </summary>
        public IDownloadControl(string FileName, string Url)
        {
            //Setting the size of the control
            this.Size = new Size(p_Width, p_Height);

            //To prevent flickering, enable DoubleBuffered
            this.DoubleBuffered = true;

            // Lbl_Title
            lbl_Title.Text = FileName;
            lbl_Title.Location = new Point(2, 2);
            lbl_Title.Font = new Font("Segoe UI", 12);
            this.Controls.Add(lbl_Title);

            // Download Progress Bar
            pgb_Download.Location = new Point(2, 24);
            pgb_Download.Width = this.Width - 4;
            pgb_Download.Value = 0;
            this.Controls.Add(pgb_Download);

            // IDownload
            IDownload download = new IDownload(Url, FileName, 0);
            download.DownloadProgressChanged += download_DownloadProgressChanged;
        }

        #endregion

        #region IDownload Event's

        /// <summary>
        /// Triggers when the DownloadProgress has changed.
        /// </summary>
        private void download_DownloadProgressChanged(object sender, EventArgs e)
        {
            //Localizing IDownload
            IDownload download = (IDownload)sender;

            //Setting progress bar value
            pgb_Download.Value = download.Percent;
        }

        #endregion
    }
}
