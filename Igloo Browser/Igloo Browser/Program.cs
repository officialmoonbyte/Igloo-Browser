using CefSharp;
using Igloo.Connection;
using Igloo.DNS;
using Igloo.Engines.CefSharp.Lib;
using Igloo.Engines.GeckoFx;
using Igloo.History;
using Igloo.Logger;
using Igloo.Pages.Browser;
using Igloo.Resources.lib;
using Igloo.Server;
using Renci.SshNet.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Igloo
{
    static class Program
    {
        #region Vars

        //Used to invoke on UI thread
        public static Form InvokeOnUI;

        //Server object
        static TcpServer localServer;

        #endregion

        #region Entry Point

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Static vars
            string sshIP = "indiegoat.us";
            int sshPort = 80;

            SetApplicationExceptions(); //Set for logging events when the application shuts down

            //Checks if the application exist
            var exists = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1;
            if (exists)
            {
                ILogger.AddToLog(ResourceInformation.ApplicationName, "Detected another instance of the browser open! Sending client package and closing browser...");

                //New TCP client to send data to local server
                TcpClient client = new TcpClient(); client.Connect("localhost", 6189);
                client.Client.Send(Encoding.UTF8.GetBytes("newpage"));

                //Closing application and client
                client.Close();
                Application.Exit();
            }

            InitializeInvokeObject(); //Initialize the invoke object for the start of the application.

            //Starting visual and text styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            //Check if there is an active internet connection
            if (!CheckInternet.CheckForInternetConnection())
            {
                ILogger.AddToLog("ERROR", "Connection to the internet was not found!");
                MessageBox.Show("Connection to the internet was not found! this browser is currently closing.", ResourceInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            InitializeSSH(); //Attemps to connect to the SSH server.
            InitializeLocalServer(); //Loads the local server used to open a new form when a new application is open.

            //Initialize CefSharp
            VoidCef.InitializeCefSharp();

            //Initialize GeckoFx
            VoidGecko.InitializeGecko();

            InitializeStripe(); //Initialize the stripe API

            ILogger.AddToLog(ResourceInformation.ApplicationName, "Running main form now!");

            //Initializing History
            IHistory.LoadFromFile();

            OpenForm(); //Opens a new borwser window
            StartFormTimer(); //Starts a timer to check when no forms are open

            //Running the application loop.
            Application.Run();
        }

        #endregion

        #region Application Exceptions

        /// <summary>
        /// Sets the events of application exceptions.
        /// </summary>
        static void SetApplicationExceptions()
        {
            //App doamin and application events for logging.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.ProcessExit += Application_ApplicationExit;
            Application.ThreadException += Application_ThreadException;

            ILogger.AddToLog("INFO", "Set handle's for all application exceptions's");

        }

        #endregion

        #region Invoke Object

        /// <summary>
        /// Creates and initialize a new Invoke object used for multi threading threads.
        /// </summary>
        static void InitializeInvokeObject()
        {
            //Used to invoke on UI thread
            InvokeOnUI = new Form();
            InvokeOnUI.Size = new System.Drawing.Size(0, 0);
            InvokeOnUI.FormBorderStyle = FormBorderStyle.None;
            InvokeOnUI.Show();
            InvokeOnUI.Hide();
        }

        #endregion

        #region Networking startup task

        /// <summary>
        /// Initialize a new SSH connection
        /// </summary>
        static void InitializeSSH()
        {
            //Starts the ssh connection task
            bool ConnectToSSH = true;
            bool ConnectToDynUpdate = true;
            bool ConnectToUniversalServer = true;
            new Thread(new ThreadStart(() => { new ServerConnections(ConnectToSSH, ConnectToDynUpdate, ConnectToUniversalServer); }));
        }

        /// <summary>
        /// Starts listening on a local port to detect when the application is open -- Saves memory.
        /// </summary>
        static void InitializeLocalServer()
        {
            //Starts the local tcp server
            localServer = new TcpServer(); localServer.StartServer();
            ILogger.AddToLog(ResourceInformation.ApplicationName, "Finished initializing TCP Server.");
        }

        #endregion

        #region Stripe Initialization

        /// <summary>
        /// Initializes stripe API
        /// </summary>
        static void InitializeStripe()
        {
            //Instalization for Stripe
            Stripe.StripeConfiguration.SetApiKey("sk_live_gTIkgQoj5jZGrpGJ7vj3x4Mt");
            ILogger.AddToLog("Stripe", "Instalization complete!");

            //**Stripe is currently not being used in this application. This thead is meant for testing. And as soon as we figure
            //Out how to store the API key. We will not be using STRIPE to accept donations. Except we are moving from STRIPE to
            //Patreon. As feedback from our open reddit AMA. We do agree, keeping a private API key public is not secure.
        }

        #endregion

        #region Starting main form

        /// <summary>
        /// Starts up a timer to check if there are any forms open.
        /// If there are no forms open, closes the application
        /// </summary>
        static void StartFormTimer()
        {
            //Setting up the timer
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    //If there are no open forms, close the application.
                    if (Application.OpenForms.Count == 1)
                    {
                        //Shutdown CEF process
                        InvokeOnUI.Invoke(new Action(() =>
                        {
                            Cef.Shutdown();

                            //Logging
                            ILogger.AddToLog(ResourceInformation.ApplicationName, "Detected no forms open. Closing application.");

                            //Exit out of the application
                            Environment.Exit(1);
                        }));
                    }

                    //Waits for 5 seconds
                    Thread.Sleep(2500);
                }
            })).Start();
        }

        /// <summary>
        /// Loads a new main window.
        /// </summary>
        static void OpenForm()
        {
            //Declaring new BrowserWindow
            BrowserWindow browser = new BrowserWindow();

            //Showing the browser form.
            browser.Show();
        }

        #endregion

        #region Application Logging

        /// <summary>
        /// For logging when Application has exit.
        /// </summary>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ILogger.AddToLog(ResourceInformation.ApplicationName, "Closing browser through application exit."); Console.WriteLine("Closing application");
            ILogger.WriteLog();
            IHistory.WriteHistory();
            localServer.StopServer();
            Application.Exit();
        }

        #endregion

        #region Error Handling

        /// <summary>
        /// For logging when there is a Unhandled Exception in the AppDomain
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.IsTerminating);

            ILogger.AddToLog("Current Domain Error", "Error with App Domain");
            MessageBox.Show("[Current Domain Error] : Error with App Domain. Please view LOG in the directory of the browser.");

            Exception ex = (Exception)e.ExceptionObject;

            ILogger.AddToLog("Current Domain", "Message : " + ex.Message);
            ILogger.AddToLog("Current Domain Error", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("Current Domain Error", "Source : " + ex.Source);

            ILogger.WriteLog();
            IHistory.WriteHistory();

            localServer.StopServer();
            Application.Exit();
        }

        /// <summary>
        /// For logging when there is a ThreadException in the Application.
        /// </summary>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ILogger.AddToLog("Application Thread Error", "Error with Application Thread");
            MessageBox.Show("[Application Thread Error] : Error with Application Thread Please view LOG in the directory of the browser.");

            Exception ex = e.Exception;

            ILogger.AddToLog("Application Thread Error", "Message : " + ex.Message);
            ILogger.AddToLog("Application Thread Error", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("Application Thread Error", "Source : " + ex.Source);

            ILogger.WriteLog();
            IHistory.WriteHistory();

            localServer.StopServer();
            Application.Exit();
        }

        /// <summary>
        /// For logging when there is a Exception in SSHClient
        /// </summary>
        static void client_ErrorOccured(object sender, ExceptionEventArgs e)
        {
            ILogger.AddToLog("SSH ERROR", "Error with SSH Client.");
            MessageBox.Show("[SSH ERROR] : Error with SSH Client. Please view LOG in the directory of the browser.");

            Exception ex = e.Exception;

            ILogger.AddToLog("SSH ERROR", "Message : " + ex.Message);
            ILogger.AddToLog("SSH ERROR", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("SSH ERROR", "Source : " + ex.Source);

            ILogger.WriteLog();
            IHistory.WriteHistory();

            localServer.StopServer();
            Application.Exit();
        }

        static void port_Exception(object sender, ExceptionEventArgs e)
        {
            ILogger.AddToLog("Port Exception Error", "Error with remote tunnel.");
            MessageBox.Show("[Port Exception Error] : Error with remote tunnel. Please view LOG in the directory of the browser.");

            Exception ex = e.Exception;

            ILogger.AddToLog("Port Exception Error", "Message : " + ex.Message);
            ILogger.AddToLog("Port Exception Error", "StackTrace : " + ex.StackTrace);
            ILogger.AddToLog("Port Exception Error", "Source : " + ex.Source);

            ILogger.WriteLog();
            IHistory.WriteHistory();

            localServer.StopServer();
            Application.Exit();
        }

        #endregion
    }
}
