using System;
using System.IO;
using System.Windows.Forms;

namespace Igloo.Logger
{
    public class ILogger
    {

        //Generating a new string for the log file.
        public static string Log;

        /// <summary>
        /// Used to add a value to the log string.
        /// </summary>
        public static void AddToLog(string Header, string Value)
        {
            string value = "[" + Header.ToUpper() + "] " + Value;

            //Check if Log is null, if it is not then makes a new line.
            if (Log != null) Log = Log + "\r\n" + value;

            //Cehck if log is null, if it is then set log to value
            if (Log == null) Log = value;
            Console.WriteLine(value);
        }

        /// <summary>
        /// Used to write to the log file
        /// </summary>
        public static void WriteLog()
        {
            //Check if the Log is null
            if (Log != null)
            {
                //Delete the log file if it exist.
                if (File.Exists(Application.StartupPath + "\\Log.log")) File.Delete(Application.StartupPath + "\\Log.log");

                //Creates the log file, and then close the file stream.
                File.Create(Application.StartupPath + "\\Log.log").Close();

                //Write to the log file.
                File.WriteAllText(Application.StartupPath + "\\Log.log", Log);
            }
        }
    
        /// <summary>
        /// Used to log any detected errors
        /// </summary>
        /// <param name="e"></param>
        public static void LogException(Exception e)
        {
            ILogger.AddToLog("Exception", "Exception detected! Following details are in-listed below.");
            ILogger.AddToLog("Exception Message", e.Message);
            ILogger.AddToLog("Exception Stack", e.StackTrace);
            ILogger.AddToLog("Exception", e.HelpLink);
        }
    }
}
