using Igloo.Logger;
using Igloo.Resources.lib;
using IglooBrowser.Startup.Exceptions;
using System;
using System.Windows.Forms;

namespace IglooBrowser
{
    static class Program
    {

        #region Vars

        //Used to invoke on UI thread
        public static Form InvokeOnUI;

        #endregion

        #region EntryPoint

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Starting visual and text styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            SetApplicationExceptions(); //Set for logging events when the application shuts down

            ILogger.AddToLog(ResourceInformation.ApplicationName, "Running main form now!");
        }

        #endregion

        #region Application Exceptions

        /// <summary>
        /// Sets the events of application exceptions.
        /// </summary>
        static void SetApplicationExceptions()
        {
            //App doamin and application events for logging.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainExceptions.UnhandledException;
            Application.ApplicationExit += ApplicationExceptions.ApplicationExit;
            AppDomain.CurrentDomain.ProcessExit += ApplicationExceptions.ApplicationExit;
            Application.ThreadException += ApplicationExceptions.ThreadException;

            ILogger.AddToLog("INFO", "Set handle's for all application exceptions's");

        }

        #endregion
    }
}
