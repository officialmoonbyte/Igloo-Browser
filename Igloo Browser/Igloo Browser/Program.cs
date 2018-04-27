using CefSharp;
using Igloo.DNS;
using Igloo.Engines.CefSharp.Lib;
using Igloo.Engines.GeckoFx;
using Igloo.History;
using Igloo.Logger;
using Igloo.Pages.Browser;
using Igloo.Resources.lib;
using Igloo.Server;
using IndieGoat.InideClient.Default;
using IndieGoat.Net.SSH;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Igloo
{
    static class Program
    {
        //Used to invoke on UI thread
        public static Form InvokeOnUI;

        //Server object
        static TcpServer localServer;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists(@"C:\STOPINTERNETC.cfg"))
            {
                Settings.Settings.TryConnection = false;
            }

            //Static vars
            string sshIP = "indiegoat.us";
            int sshPort = 80;

            //Starting visual and text styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            //Used to invoke on UI thread
            InvokeOnUI = new Form();
            InvokeOnUI.Size = new System.Drawing.Size(0, 0);
            InvokeOnUI.FormBorderStyle = FormBorderStyle.None;
            InvokeOnUI.Show();
            InvokeOnUI.Hide();

            //App doamin and application events for logging.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ApplicationExit += Application_ApplicationExit;
            AppDomain.CurrentDomain.ProcessExit += Application_ApplicationExit;
            Application.ThreadException += Application_ThreadException;

            ILogger.AddToLog("INFO", "Set handle's for all application exceptions's");

            //Tries to see if the process is open
            try
            {
                //Gets the list of processes with browser and then write that process in memory
                Process openBrowserProcess; Process[] processList = Process.GetProcessesByName(ResourceInformation.ApplicationName);
                openBrowserProcess = processList[0];

                //Checks if the process is equal to null
                if (openBrowserProcess != null)
                {
                    ILogger.AddToLog(ResourceInformation.ApplicationName, "Detected another instance of the browser open, connecting to tcp server.");

                    //Sets up a new client and connect to the local server
                    TcpClient client = new TcpClient();
                    client.Connect("localhost", 6189);

                    ILogger.AddToLog(ResourceInformation.ApplicationName, "Connection established");

                    //Send a command to make a new window
                    if (client.Connected) client.Client.Send(Encoding.UTF8.GetBytes("newpage"));

                    Thread.Sleep(1000);

                    //Closes the application
                    Environment.Exit(1);
                }
            }
            catch { }

            //Check if there is an active internet connection
            if (!CheckInternet.CheckForInternetConnection())
            {
                ILogger.AddToLog("ERROR", "Connection to the internet was not found!");
                MessageBox.Show("Connection to the internet was not found! thi sbrowser is currently closing.", ResourceInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //Checks if the network should connect to the ssh server
            if (Settings.Settings.TryConnection)
            {
                Thread sshThread = new Thread(new ThreadStart(() =>
                {

                    //Sets the VortexStudio ip and the External IP
                    var vortexStudioIP = Dns.GetHostAddresses(sshIP)[0];
                    string externalIP = new WebClient().DownloadString("http://icanhazip.com/"); externalIP = externalIP.Trim();

                    //Changes the IP of the SSH client, if using local internet.
                    if (vortexStudioIP.ToString() == externalIP)
                    {
                        sshIP = "192.168.0.8";
                        ILogger.AddToLog("INFO", "Detected local network use! Changing IP of the SSH server. External IP " + externalIP + ", Vortex Studio IP : " + vortexStudioIP.ToString());
                    }

                    ILogger.AddToLog("SSH", "Trying to connect to SSH server.... Please wait.");

                    //Connects to the ssh server
                    GlobalSSH sshService = new GlobalSSH(); ILogger.AddToLog("SSH", "Created SSH object");
                    sshService.StartSSHService(sshIP, sshPort.ToString(), "public", "Public36");

                    ILogger.AddToLog("SSH", "SSH service started!");

                    Thread.Sleep(1000);

                    //Forwards all local port's for the server
                    sshService.ForwardLocalPort("2445", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 2445");
                    sshService.ForwardLocalPort("3389", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 3389");
                    sshService.ForwardLocalPort("5750", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 5750");

                    //Initializing update
                    DynUpdater.Main dynUpdater = new DynUpdater.Main();
                    dynUpdater.UpdateURLLocation = "https://dl.dropboxusercontent.com/s/9z2xx5rbi1qw4vw/Install.zip?dl=0";
                    dynUpdater.CheckUpdate("127.0.0.1", 5750);

                    ILogger.AddToLog("DynUpdater", "Initialized DYNUpdater on port 5750.");

                    //Connects to the Universal server
                    Settings.Settings.UniversalConnection = new IndieClient();
                    Settings.Settings.UniversalConnection.ConnectToRemoteServer("localhost", 2445);
                    ILogger.AddToLog("Indie Client", "Connected to host localhost:2445");

                })); sshThread.Start();
            }

            //Starts the local tcp server
            localServer = new TcpServer(); localServer.StartServer();

            //Initialize CefSharp
            VoidCef.InitializeCefSharp();

            //Initialize GeckoFx
            VoidGecko.InitializeGecko();

            //Set the IP and PORT of the Proxy Server
            string ip = "localhost";
            int iport = 5567;

            //Instalization for Stripe
            Stripe.StripeConfiguration.SetApiKey("sk_live_gTIkgQoj5jZGrpGJ7vj3x4Mt");
            ILogger.AddToLog("Stripe", "Instalization complete!");

            ILogger.AddToLog(ResourceInformation.ApplicationName, "Running main form now!");

            //Declaring new BrowserWindow
            BrowserWindow browser = new BrowserWindow();

            //Initializing History
            IHistory.LoadFromFile();

            //Showing the browser form.
            browser.Show();

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

            //Running the application loop.
            Application.Run();
        }

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
