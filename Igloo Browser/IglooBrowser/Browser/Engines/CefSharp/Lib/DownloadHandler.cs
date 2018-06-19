using CefSharp;
using Igloo.Logger;

namespace Igloo.Engines.CefSharp.Lib
{
    public class DownloadHandler : IDownloadHandler
    {
        /// <summary>
        /// Used when called on the method, ask for the custom DownloadHandler
        /// </summary>
        public DownloadHandler()
        {
            ILogger.AddToLog("CEF IDownloadHandler", "Initialization for the DownloadHandler is complete!");
        }

        /// <summary>
        /// Used to detect when the browser has detected a new download.
        /// </summary>
        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            //Add a item to the custom DownloadHandler
            Settings.Settings.downloadItem.AddDownloadItem(downloadItem.Url, downloadItem.SuggestedFileName);
            ILogger.AddToLog("CEF IDownloadHandler", "Downloading : " + downloadItem.Url);
        }

        /// <summary>
        /// Used to cancel the download as soon as it starts.
        /// </summary>
        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            //Cancels the download for the item.
            callback.Cancel();
        }
    }
}
