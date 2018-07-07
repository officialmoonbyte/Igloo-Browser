using IndieGoat.Net.Tcp;
using System;

namespace Igloo.Resources.lib
{
    public static class ResourceInformation
    {
        public static string Username;
        public static string Password;
        public static DateTime ExpirationDate;
        public static string ApplicationName = "Igloo Browser";

        public static bool IsSubscribed = false;
        public static bool LoggedIn = false;

        public static bool VPNEnabled = false;

        public static UniversalClient serverConnection = new UniversalClient();

        public static int GTTS = 2000; //GLOBAL THREAD TICK SPEED
    }
}
