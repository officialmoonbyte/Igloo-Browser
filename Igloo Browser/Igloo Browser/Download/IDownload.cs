using GlobalSettingsFramework;
using System;
using System.IO;
using System.Net;

namespace Igloo.Download
{
    /// <summary>
    /// A class webclient, used to process and download files.
    /// </summary>
    public class IDownload
    {
        #region Var's

        //Does this have to be commented
        public int Percent;
        public string URL;
        public string FileName;

        //Events for processing when the DownloadProgress has changed and when it has finished
        public event EventHandler DownloadProgressChanged;
        public event EventHandler DownloadFinished;

        //Webclient for downloading
        WebClient client = new WebClient();

        #endregion

        #region Required

        /// <summary>
        /// Calling of the IDownlaod (Initialization)
        /// </summary>
        public IDownload(string _Url, string _fileName, int _percent)
        {
            //Setting local var's
            URL = _Url;
            FileName = _fileName;
            Percent = _percent;

            //Initializing AUS and getting the Download Directory setting.
            GFS gFS = new GFS();
            string DownloadDirectory = gFS.ReadSetting("downloadD");

            //Setting client event's
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadDataCompleted += client_DownloadDataCompleted;

            //Create the Download Directory if it does not exist
            if (!Directory.Exists(DownloadDirectory)) Directory.CreateDirectory(DownloadDirectory);

            //Setting the FileDirectory to locate where to save the file
            string fileDirectory = DownloadDirectory + @"\" + FileName;

            //Split the FileName into the name[0] and type [1]
            string[] FileArgs = FileName.Split('.');

            #region Reinvalidating the FileName

            for (int i = 0; ; i++)
            {
                //Initialize a tmpI
                int tmpI = i - 1;

                //Set the file name to 'FileName('i+1').exce
                if (i != 0) { fileDirectory = DownloadDirectory + "\\" + FileArgs[0] + " (" + (tmpI + 1) + ")." + FileArgs[1]; }

                //Check if file exist, if false then break the for loop.
                if (!File.Exists(fileDirectory)) { break; }
            }

            //Download's the file to the FileDirectory from URL
            client.DownloadFileAsync(new Uri(_Url), fileDirectory);

            #endregion 

        }

        #endregion

        #region Client Events

        /// <summary>
        /// Triggers when the WebClient Download Progress Change happens.
        /// </summary>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Set the percent to the progress of the download.
            Percent = e.ProgressPercentage;

            //Invoke the DownloadProgressChanged event
            DownloadProgressChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Triggers when the WebClient finishes the download.
        /// </summary>
        private void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            //Set the percent to exacly 100 (Done)
            Percent = 100;

            //Invoke the DownloadFinished event.
            DownloadFinished?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
