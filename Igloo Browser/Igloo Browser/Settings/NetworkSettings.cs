using Igloo.Download;
using IndieGoat.InideClient.Default;

namespace Igloo.Settings
{
    public static class Settings
    {
        public static IndieClient UniversalConnection;

        public static DownloadRequest downloadItem = new DownloadRequest();

        public static string RemoteIP;
        public static int RemotePort;

        public static bool TryConnection = true;
    }
}
