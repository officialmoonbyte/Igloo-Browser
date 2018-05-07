﻿using IndieGoat.InideClient.Default;
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

        public static IndieClient serverConnection = new IndieClient();

        public static int GTTS = 2000; //GLOBAL THREAD TICK SPEED
    }
}