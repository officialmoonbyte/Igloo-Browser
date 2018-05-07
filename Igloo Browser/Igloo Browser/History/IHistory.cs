using Igloo.Logger;
using Igloo.Resources.lib;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Igloo.History
{
    /// <summary>
    /// Highly based of of ILogger
    /// </summary>
    public class IHistory
    {
        //List of History Items 
        public static List<string> History = new List<string>();

        //Seperator for the string
        static string seperator = "|%991%|";

        /// <summary>
        /// Used to format and add a value to the history
        /// </summary>
        /// <param name="URL">The link for the website</param>
        /// <param name="date">Date when the website was viewed</param>
        /// <param name="Title">Title of the website</param>
        public static void AddToHistory(string URL, string date, string Title)
        {
            //Format the Value
            string value = URL + seperator + Title + seperator + date;

            //Insert the History value at index 0
            History.Insert(0, value);
            ILogger.AddToLog("IHistory", "Added a new history entry! : " + value);
        }

        /// <summary>
        /// Used to wrhite the History value to the History file
        /// </summary>
        public static void WriteHistory()
        {
            string CacheDirectory = @"C:\MoonByte\Igloo Browser Cache\History.hit";

            //Creates the cache directory if it does not exist's
            if (!Directory.Exists(@"C:\MoonByte\Igloo Browser Cache")) Directory.CreateDirectory(@"C:\MoonByte\Igloo Browser Cache");

            //If History.hit file does not exist, create the file.
            if (!File.Exists(CacheDirectory)) File.Create(CacheDirectory).Close();

            //Read all of the lines of the History.hit file.
            File.WriteAllLines(CacheDirectory, History);
            ILogger.AddToLog("IHistory", "Wrote all history values to History.hit");
        }

        /// <summary>
        /// Used to Initialize and Load the history
        /// </summary>
        public static void LoadFromFile()
        {
            string CacheDirectory = @"C:\MoonByte\" + ResourceInformation.ApplicationName + @" Cache\History.hit";

            //If History.hit file does not exist, create the file.
            if (!File.Exists(CacheDirectory)) File.Create(CacheDirectory).Close();

            //Read all of the lines of the History.hit file
            History = File.ReadAllLines(CacheDirectory).ToList();
            ILogger.AddToLog("IHistory", "Finished initializing history!");
        }
    }
}
