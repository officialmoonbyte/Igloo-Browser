namespace Igloo.Settings
{
    public static class UserAgents
    {

        #region UserAgents

        private static string Google = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36";
        private static string FireFox = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0";
        private static string Default = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";

        //Planned to add
        private static string Edge12 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246";
        private static string ChromeOS = "Mozilla/5.0 (X11; CrOS x86_64 8172.45.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.64 Safari/537.36";
        private static string Safari601 = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9";

        #endregion

        #region GetMethod

        /// <summary>
        /// Used to get a UserAgent in string format.
        /// </summary>
        /// <returns>UserAgent</returns>
        public static string GetUserAgent()
        {

            string userAgent = null;

            //Set user agent based off of the UserAgent selected
            if (SettingsManager.UserAgent == SettingsManager.UserAgents.Default)
            { userAgent = Default; }
            if (SettingsManager.UserAgent == SettingsManager.UserAgents.Chrome)
            { userAgent = Google; }
            if (SettingsManager.UserAgent == SettingsManager.UserAgents.Firefox)
            { userAgent = FireFox; }

            //Set userAgent to the default setting if null
            if (userAgent == null) { userAgent = Default; }

            //return
            return userAgent;

        }

        #endregion

    }
}
