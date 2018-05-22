using GlobalSettingsFramework;
using GlobalSettingsFramework.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
        public List<DownloadObject> DownloadList = new List<DownloadObject>();

        //Setting name
        const string DownloadSettingHeader = "downloads";

        /// <summary>
        /// Used when the browser processes in the DownloadHandler
        /// </summary>
        public void AddDownloadItem(string URL, string FileName)
        {
            //Creating a new instance of the IDownloadControl and then incerting it into the download list.
            DownloadObject download = new DownloadObject(FileName, URL, true);
            DownloadList.Insert(0, download);

            //Invoking NewDownladItem event for display processing.
            NewDownloadItem?.Invoke(new object(), new EventArgs());

            ILogger.AddToLog("DownloadItem", "Downlaod object was invoke the event and started a new IDownloadControl");
        }

        /// <summary>
        /// Creates download objects out of the download list
        /// </summary>
        public void InitializeDownloadItems()
        {
            //Seperator for the download
            string[] Seperator = new string[] { "%20%|downloadSep|%20%" };
            string DownloadDirectory = Application.StartupPath + @"\Download";
            string DownloadFile = DownloadDirectory + @"\downloadfiles.dat";

            //Creates the directory and file if it does not exist
            if (!Directory.Exists(DownloadDirectory)) Directory.CreateDirectory(DownloadDirectory);

            //Sets a new instance of GFS
            GFS gfs = new GFS();
            gfs.SettingsDirectory = DownloadFile;

            //Check if the setting exist
            if (!gfs.CheckSetting(DownloadSettingHeader)) { gfs.EditSetting(DownloadSettingHeader, Seperator[0]); }

            //Gets the string and split it into an array
            string rawDownloadString = gfs.ReadSetting(DownloadSettingHeader);
            string[] formatDownloadString = rawDownloadString.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);

            //Adds the download object
            foreach(string str in formatDownloadString)
            {
                string[] fileSeperator = new string[] { "%20%|downloadFileSep|%20%" };
                string[] fileInfo = str.Split(fileSeperator, StringSplitOptions.None);
                DownloadList.Add(new DownloadObject(fileInfo[0], fileInfo[1], false));
            }
        }

        public void SaveDownloadItems()
        {
            //Seperator for the download
            string[] Seperator = new string[] { "%20%|downloadSep|%20%" };
            string DownloadDirectory = Application.StartupPath + @"\Download";
            string DownloadFile = DownloadDirectory + @"\downloadfiles.dat";

            //Creates the directory and file if it does not exist
            if (!Directory.Exists(DownloadDirectory)) Directory.CreateDirectory(DownloadDirectory);

            //Sets a new instance of GFS
            GFS gfs = new GFS();
            gfs.SettingsDirectory = DownloadFile;

            List<string> DownloadinString = new List<string>();
            string RawDownloadString = null;

            //Formating for each DownloadInString list and RawDownloadString
            foreach(DownloadObject download in DownloadList)
            {
                string[] fileSeperator = new string[] { "%20%|downloadFileSep|%20%" };
                DownloadinString.Add(download.FileName + fileSeperator[0] + download.FileURL);
            }
            foreach (string str in DownloadinString)
            {
                RawDownloadString += str + Seperator[0];
            }

            //Editing the setting
            try
            {
                gfs.EditSetting(DownloadSettingHeader, RawDownloadString);
            } catch { ILogger.AddToLog("Download", "Failed to write download histroy."); }
        }
    }
}
