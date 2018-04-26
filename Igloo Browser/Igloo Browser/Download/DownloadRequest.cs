using GlobalSettingsFramework.Logger;
using System;
using System.Collections.Generic;

namespace Igloo.Download
{
    /// <summary>
    /// Used to process a new download item.
    /// </summary>
    public class DownloadRequest
    {
        //Event for displaying the download item on the new browser page
        public event EventHandler NewDownloadItem;

        //List to count all the download items.
        public List<IDownloadControl> DownloadList = new List<IDownloadControl>();

        /// <summary>
        /// Used when the browser processes in the DownloadHandler
        /// </summary>
        public void AddDownloadItem(string URL, string FileName)
        {
            //Creating a new instance of the IDownloadControl and then incerting it into the download list.
            IDownloadControl download = new IDownloadControl(FileName, URL);
            DownloadList.Insert(0, download);

            //Invoking NewDownladItem event for display processing.
            NewDownloadItem?.Invoke(new object(), new EventArgs());

            ILogger.AddToLog("DownloadItem", "Downlaod object was invoke the event and started a new IDownloadControl");
        }
    }
}
