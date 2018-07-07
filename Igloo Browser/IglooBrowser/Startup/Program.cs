using Igloo.History;
using Igloo.Logger;
using Igloo.Resources.lib;
using IglooBrowser.Pages;
using IglooBrowser.Startup.Exceptions;
using System;
using System.Threading;
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

            ILogger.AddToLog("INFO", "Running main form now!");

            SetFormTimer();
            
            MainForm form = new MainForm();
            form.Show();

            //Running the application loop.
            Application.Run();
        }

        #endregion

        #region Timer

        /// <summary>
        /// Sets a timer to detect if a MainForm is open
        /// </summary>
        private static void SetFormTimer()
        {
            //Setting up the timer
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    FormCollection fc = Application.OpenForms;

                    if (fc.Count == 1)
                    {
                        ILogger.AddToLog("Info", "Detected no open forms! Closing application.");
                        ClosingEvents();
                    }
                    Thread.Sleep(50);
                }
            })).Start();
        }

        #endregion

        #region ClosingEvents

        /// <summary>
        /// Runs this thread when the application is going to close.
        /// </summary>
        private static void ClosingEvents()
        {
            ILogger.WriteLog();
            IHistory.WriteHistory();
            Environment.Exit(0);
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
