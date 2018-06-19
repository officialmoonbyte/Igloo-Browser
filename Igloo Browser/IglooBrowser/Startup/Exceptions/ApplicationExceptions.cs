using Igloo.Logger;
using Igloo.Resources.lib;
using System;
using System.Threading;

namespace IglooBrowser.Startup.Exceptions
{
    public class ApplicationExceptions
    {
        public static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ILogger.AddToLog("Application Thread Error", "Error with Application Thread");
            //MessageBox.Show("[Application Thread Error] : Error with Application Thread Please view LOG in the directory of the browser.");

            Exception ex = e.Exception;

            ILogger.AddToLog("Application Thread Error", "Message : " + ex.Message);
            ILogger.AddToLog("Application Thread Error", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("Application Thread Error", "Source : " + ex.Source);

            ClosingEvents.RunClosingEvents();
        }

        public static void ApplicationExit(object sender, EventArgs e)
        {
            ILogger.AddToLog(ResourceInformation.ApplicationName, "Closing browser through application exit."); Console.WriteLine("Closing application");
            ClosingEvents.RunClosingEvents();
        }
    }
}
