using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igloo.Download
{
    public class DownloadObject
    {
        #region Vars

        public string FileName;
        public string FileURL;
        public int DownloadPercent = 0;

        #endregion

        /// <summary>
        /// Downloads the file from the url if given.
        /// </summary>
        public DownloadObject(string fileName, string fileURL, bool Download)
        {
            FileName = fileName;
            FileURL = fileURL;

            if (Download)
            {
                IDownload download = new IDownload(FileURL, FileName, DownloadPercent);
                download.DownloadProgressChanged += (obj, args) =>
                {
                    DownloadPercent = download.Percent;
                };
                download.DownloadFinished += (obj, args) =>
                {
                    DownloadPercent = 100;
                };
            } else { DownloadPercent = 100; }
        }
    }
}
