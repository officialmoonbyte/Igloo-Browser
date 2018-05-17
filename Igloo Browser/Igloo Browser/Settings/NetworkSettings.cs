using Igloo.Download;

namespace Igloo.Settings
{
    public static class Settings
    {
        public static UniversalClient.UniversalClient UniversalConnection;

        public static DownloadRequest downloadItem = new DownloadRequest();

        public static string RemoteIP;
        public static int RemotePort;

        public static bool TryConnection = true;
    }
}
