using Igloo.History;
using Igloo.Logger;
using Igloo.Settings;
using IndieGoat.Net.Tcp;
using IndieGoat.Net.Updater;
using System.Net;

namespace Igloo.Connection
{

    public static class ManagedResources
    {

        #region Resource Vars

        public static UniversalClient Client = new UniversalClient();
        public static UniversalServiceUpdater Updater;

        public enum PortServers { IglooBrowser, VSLoginServer }

        #endregion

        #region Resource Methods

        public static int GetServerPort(PortServers i)
        {
            if (i == PortServers.IglooBrowser) return 6431;
            if (i == PortServers.VSLoginServer) return 6432;
            return 6431;
        }

        public static string GetExternalIP()
        {
            return new WebClient().DownloadString("http://icanhazip.com");
        }

        public static string GetIndiegoatIP()
        {
            return Dns.GetHostEntry("indiegoat.us").AddressList[0].ToString();
        }

        public static bool CheckConnectionStatus()
        {
            return Client.Client.Connected;
        }

        #endregion

    }

    public static class ServerConnections
    {

        #region Initializing Connections

        public static void InitializeServerConnections()
        {
            ILogger.AddToLog("INFO", "Authorized request to validate and initialize server connections.");
            string ServerIp = "indiegoat.us";
            if (ManagedResources.GetExternalIP() == ManagedResources.GetIndiegoatIP()) { ServerIp = "192.168.0.16"; ILogger.AddToLog("WARN", "Changed to a IndieGoat localized IP! This may accure if you are connected locally to the IndieGoat Universal Server. Network issues may be apparent."); }
            int UniversalServerPort = 0;
            if (SettingsManager.UniversalUsername != null) { UniversalServerPort = ManagedResources.GetServerPort(ManagedResources.PortServers.VSLoginServer); }
            else { UniversalServerPort = ManagedResources.GetServerPort(ManagedResources.PortServers.IglooBrowser); }
            ILogger.AddToLog("INFO", "Set the Universal Server IP to : " + UniversalServerPort);

            ManagedResources.Client.ConnectToRemoteServer(new UniversalConnectionObject(ServerIp, UniversalServerPort));
            if (ManagedResources.Client.Client.Connected) { ILogger.AddToLog("INFO", "Sucessfully connected to UniversalServer."); }
            else { ILogger.AddToLog("WARN", "Failed to connect to UniversalServer"); }

            ManagedResources.Updater = new UniversalServiceUpdater(); ILogger.AddToLog("INFO", "Initialized Service Updater, entering update loop");
            while (true)
            {
                if (ManagedResources.Updater.CheckUniversalAPI()) break;
            }
            ILogger.AddToLog("INFO", "Finished checking UniversalAPI install");
            ManagedResources.Updater.CheckUpdate(ServerIp, ManagedResources.GetServerPort(ManagedResources.PortServers.VSLoginServer));
            ILogger.AddToLog("INFO", "Finished checking for update.");
            ILogger.AddToLog("INFO", "Done initializing network connections.");
        }

        #endregion

        #region History

        public static void SyncHistory()
        {
            string ServerHistory = null;

            IHistory.LoadFromString(ServerHistory);
        }

        #endregion

    }
}
