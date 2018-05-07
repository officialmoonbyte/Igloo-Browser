﻿using Igloo.Logger;
using Igloo.Resources.lib;
using IndieGoat.InideClient.Default;
using IndieGoat.Net.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Igloo.Connection
{
    public class ServerConnections
    {

        #region Vars

        #region SSH

        string sshIP = "indiegoat.us";
        int sshPort = 80;
        GlobalSSH sshService;

        #endregion

        #endregion Vars

        #region Startup

        public ServerConnections(bool SSHV = true, bool Update = true, bool Universal = true)
        {
            if (SSHV == true) { SSH(); }
            if (Update == true) { DYN(); }
            if (Universal == true) { UniversalServer(); }
        }

        #endregion

        #region SSH

        private void SSH()
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

            sshService = new GlobalSSH(); ILogger.AddToLog("SSH", "Created SSH Object");
            sshService.StartSSHService(sshIP, sshPort.ToString(), "public", "Public36");
            PortSSH();

            ILogger.AddToLog("SSH", "SSH service started!");
        }

        private void PortSSH()
        {
            sshService.ForwardLocalPort("2445", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 2445");
            sshService.ForwardLocalPort("3389", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 3389");
            sshService.ForwardLocalPort("5750", "192.168.0.11"); ILogger.AddToLog("SSH", "Forward Port 5750");

        }

        #endregion

        #region Universal Server

        private void UniversalServer()
        {
            //Connects to the Universal server
            Settings.Settings.UniversalConnection = new IndieClient();
            Settings.Settings.UniversalConnection.ConnectToRemoteServer("localhost", 2445);
            ILogger.AddToLog("Indie Client", "Connected to host localhost:2445");
        }

        #endregion Universal Server

        #region DYN

        private void DYN()
        {
            //Initializing update
            DynUpdater.Main dynUpdater = new DynUpdater.Main();
            dynUpdater.UpdateURLLocation = "https://dl.dropboxusercontent.com/s/9z2xx5rbi1qw4vw/Install.zip?dl=0";
            dynUpdater.CheckUpdate("127.0.0.1", 5750);

            ILogger.AddToLog("DynUpdater", "Initialized DYNUpdater on port 5750.");
        }

        #endregion DYN

    }
}
