using System.Net;

namespace Igloo.DNS
{
    class CheckInternet
    {
        /// <summary>
        /// Used to check if there is a stable internet connection.
        /// </summary>
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
