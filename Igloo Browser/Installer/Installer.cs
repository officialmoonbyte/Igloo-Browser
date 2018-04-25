using IndieGoat.MaterialFramework.Controls;
using Ionic.Zip;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Installer
{
    public class Installer
    {
        #region LocalVars

        public string TempDirectory = Path.GetTempPath();
        public string ZipName = "install.zip";
        public string DefaultDirectory = @"C:\MoonByte\Crash\";
        public string DownloadURL = "https://dl.dropboxusercontent.com/s/3c58k3wvhw4aejw/Install.zip?dl=0";

        public MaterialProgressBar InstallProgressBar;
        public MaterialProgressBar DownloadProgressBar;

        public event EventHandler DownloadFinished;

        public event EventHandler DownloadProgressChanged;

        public event EventHandler ExtractProgressChanged;

        public int DownloadPercent = 0;
        public int ExtractPercent = 0;

        #endregion LocalVars

        #region Initialization

        /// <summary>
        /// Initialize the installer class, setting a url if needed.
        /// </summary>
        /// <param name="Downloadurl">URL to be downloaded</param>
        public Installer(string Downloadurl = "https://dl.dropboxusercontent.com/s/3c58k3wvhw4aejw/Install.zip?dl=0")
        { DownloadURL = Downloadurl; }

        #endregion Initialization

        #region Downloading

        /// <summary>
        /// Download's the client
        /// </summary>
        public void DownloadFile()
        {
            //Initialize the web client
            WebClient downloadClient = new WebClient();

            //Webclient events
            downloadClient.DownloadFileCompleted += (obj, args) =>
            {
                DownloadFinished?.Invoke(this, new EventArgs());
            };
            downloadClient.DownloadProgressChanged += (obj, args) =>
            {
                //Get event args
                DownloadProgressChangedEventArgs DoubleArgs = (DownloadProgressChangedEventArgs)args;

                //Sets progress percentage as a int
                DownloadPercent = DoubleArgs.ProgressPercentage;

                Console.WriteLine("Local Download Percent : " + DownloadPercent);

                //Changes the download progress bar
                if (DownloadProgressBar != null) DownloadProgressBar.Value = DownloadPercent;

                //Triggers event
                DownloadProgressChanged?.Invoke(this, new EventArgs());
            };

            //Creates the directory
            Directory.CreateDirectory(TempDirectory + "\\" + "MoonByte" + "\\");

            //Download's the file.
            downloadClient.DownloadFileAsync(new Uri(DownloadURL), TempDirectory + "\\MoonByte\\" + ZipName);
        }

        #endregion Downloading

        #region Extracting

        /// <summary>
        /// Extracts the client
        /// </summary>
        private int totalFiles; private int filesExtracted; public void ExtractDownload()
        {
            //Deletes old files if exist
            if (Directory.Exists(DefaultDirectory))
            { Directory.Delete(DefaultDirectory, true); }

            Console.WriteLine("Extracting...");
            //Sets up a new ZipFile
            using (ZipFile zip = ZipFile.Read(TempDirectory + "\\MoonByte\\" + ZipName))
            {
                //Extract Progress Update
                zip.ExtractProgress += (obj, args) =>
                {
                    //Setup new arguments
                    ExtractProgressEventArgs ExArgs = (ExtractProgressEventArgs)args;

                    if (ExArgs.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry) return;

                    //Add a file extracted
                    filesExtracted++;

                    //Gets the extract percent with the amount of files extracted
                    ExtractPercent = 100 * filesExtracted / totalFiles;

                    //Triggers extract progress changed event
                    ExtractProgressChanged?.Invoke(this, new EventArgs());

                    //Changes the value of the ectract progress bar
                    if (InstallProgressBar != null)
                    { InstallProgressBar.Value = ExtractPercent; }

                    Console.WriteLine("Extract progress : " + ExtractPercent);
                };

                //Extracts the zip file in a new thread
                new Thread(new ThreadStart(() =>
                {
                    totalFiles = zip.Count;
                    filesExtracted = 0;
                    zip.ExtractAll(DefaultDirectory, ExtractExistingFileAction.OverwriteSilently);
                    Console.WriteLine("Extracting complete");
                })).Start();
            }
        }

        #endregion Extracting
    }
}