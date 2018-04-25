namespace Igloo.Resources.lib
{
    public static class ResourceInformation
    {
        public static string Username;
        public static string Password;
        public static DateTime ExpirationDate;

        public static bool IsSubscribed = false;
        public static bool LoggedIn = false;

        public static bool VPNEnabled = false;

        public static IndieClient serverConnection = new IndieClient();
    }
}
