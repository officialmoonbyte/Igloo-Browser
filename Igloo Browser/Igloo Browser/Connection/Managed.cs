using Igloo.Logger;
using IndieGoat.Net.SSH;
using IndieGoat.Net.Updater;
using System.Net;

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

            sshService = new GlobalSSH(sshIP, sshPort, "public", "Public36", true); ILogger.AddToLog("SSH", "Created SSH Object");
            PortSSH();

            ILogger.AddToLog("SSH", "SSH service started!");
        }

        private void PortSSH()
        {
            sshService.TunnelLocalPort("192.168.0.16", "2445", true);
            sshService.TunnelLocalPort("192.168.0.16", "3389", true);
            sshService.TunnelLocalPort("192.168.0.16", "5750", true);
        }

        #endregion

        #region Universal Server

        private void UniversalServer()
        {
            //Connects to the Universal server
            Settings.Settings.UniversalConnection = new UniversalClient.UniversalClient();
            Settings.Settings.UniversalConnection.ConnectToRemoteServer("localhost", 2445);
            ILogger.AddToLog("Indie Client", "Connected to host localhost:2445");
        }

        #endregion Universal Server

        #region DYN

        private void DYN()
        {
            //Initializing update
            UniversalServiceUpdater updater = new UniversalServiceUpdater("https://dl.dropbox.com/s/vppsempy90194q1/install.zip?dl=0");

            if (updater.CheckUniversalAPI()) ILogger.AddToLog("UniversalServiceUpdater", "Universal API is currently not installed!");
            updater.CheckUpdate("localhost", 5750);

            ILogger.AddToLog("UniversalServiceUpdater", "Initialized USU on port 5750.");
        }

        #endregion DYN

    }
}
