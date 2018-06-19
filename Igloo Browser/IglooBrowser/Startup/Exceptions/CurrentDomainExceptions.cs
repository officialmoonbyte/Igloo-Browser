using Igloo.Logger;
using System;

namespace IglooBrowser.Startup.Exceptions
{
    public class CurrentDomainExceptions
    {
        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ILogger.AddToLog("Current Domain Error", "Error with App Domain");
            //MessageBox.Show("[Current Domain Error] : Error with App Domain. Please view LOG in the directory of the browser.");

            Exception ex = (Exception)e.ExceptionObject;

            ILogger.AddToLog("Current Domain", "Message : " + ex.Message);
            ILogger.AddToLog("Current Domain Error", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("Current Domain Error", "Source : " + ex.Source);

            ClosingEvents.RunClosingEvents();
        }
    }
}
