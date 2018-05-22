using GlobalSettingsFramework;
using Igloo.Logger;
using System;
using System.IO;
using System.Windows.Forms;

namespace Igloo.Settings
{
    public class SettingsManager
    {

        #region Enums

        public enum SearchEngines { Google, Yahoo, Bing };
        public enum UserAgents { Default, Chrome, Firefox };
        public enum HistoryEngines { SyncAll, NoSync };
        public enum BuildVersions { Public, Beta, Alpha };
        public enum DonatePeriods { Daily, Monthly, Yearly };
        public enum BrowserEngines { CefSharp, GeckoUI };

        #endregion

        #region Setting Values

        public static string UniversalUsername;
        public static string UniversalPassword;

        public static SearchEngines SearchEngine;
        public static HistoryEngines HistorySettings;
        public static UserAgents UserAgent;
        public static BrowserEngines BrowserEngine;
        public static BuildVersions BuildVersion;

        public static bool SyncHistory;
        public static bool SyncBookmarks;

        public static int SyncInterval;

        #endregion

        #region SettingNames

        private static string _Username = "%User%";
        private static string _Password = "%Password%";
        private const string _BrowserEngine = "%browserEngine%";
        private static string _SearchEngine = "%SearchEngine%";
        private const string _HistoryEngine = "%historyEngine%";
        private const string _UserAgent = "%usrAgent%";
        private const string _BuildVersion = "%conBuildVersion%";
        private const string _SyncHistory = "%syncHistory%";
        private const string _SyncBookmarks = "%syncBookmarks%";
        private const string _SyncInterval = "%_SyncTime%";

        #endregion

        #region Get values

        private static GFS manager = new GFS();

        /// <summary>
        /// Calls the InvalidateSetting method to get the values.
        /// </summary>
        public static void InitializeValues()
        {
            manager = new GFS();
            string settingsDirectory = Application.StartupPath + @"\Settings\";

            if (!Directory.Exists(settingsDirectory)) Directory.CreateDirectory(settingsDirectory);

            string SettingFile = settingsDirectory + "applicationSettings.set";
            manager.SettingsDirectory = SettingFile;

            UniversalUsername = InvalidateSetting(_Username, "");
            UniversalPassword = InvalidateSetting(_Password, "");
            BrowserEngine = BrowserEngineFromString(InvalidateSetting(_BrowserEngine, _cefSharp));
            SearchEngine = SearchEngineFromString(InvalidateSetting(_SearchEngine, _google));
            HistorySettings = HistoryEngineFromString(InvalidateSetting(_HistoryEngine, _SyncAll));
            UserAgent = UserAgentFromString(InvalidateSetting(_UserAgent, _default));
            BuildVersion = BuildVersionFromString(InvalidateSetting(_BuildVersion, _Public));
            SyncHistory = bool.Parse(InvalidateSetting(_SyncHistory, true.ToString()));
            SyncBookmarks = bool.Parse(InvalidateSetting(_SyncBookmarks, true.ToString()));
            SyncInterval = int.Parse(InvalidateSetting(_SyncInterval, 10000.ToString()));
        }

        /// <summary>
        /// Used to load settings from the InitializeValues void
        /// </summary>
        /// <param name="SettingName">The name of the setting</param>
        /// <param name="DefaultValue">Baseline value</param>
        /// <returns></returns>
        private static string InvalidateSetting(string SettingName, string DefaultValue)
        {
            ILogger.AddToLog("GFS", "Checking " + SettingName + ", with datavalue " + DefaultValue + " exist.");
            //Check if the setting exist's
            if (!manager.CheckSetting(SettingName))
            {
                ILogger.AddToLog("GFS", SettingName + " exist! Returning value.");
                //Return the value of the setting
                return manager.ReadSetting(SettingName);
            }
            else
            {
                ILogger.AddToLog("GFS", SettingName + " does not exist! Creating setting with " + DefaultValue + " as a datavalue");
                //Edit the setting and then return the default value.
                manager.EditSetting(SettingName, DefaultValue);
                return DefaultValue;
            }
        }

        #endregion

        #region Edit Values

        /// <summary>
        /// Writes all settings onto the disk.
        /// </summary>
        public static void SaveValues()
        {
            manager.EditSetting(_Username, UniversalUsername);
            manager.EditSetting(_Password, UniversalPassword);
            manager.EditSetting(_SearchEngine, SearchEngineToString(SearchEngine));
            manager.EditSetting(_HistoryEngine, HistoryEngineToString(HistorySettings));
            manager.EditSetting(_UserAgent, UserAgentToString(UserAgent));
            manager.EditSetting(_BrowserEngine, BrowserEngineToString(BrowserEngine));
            manager.EditSetting(_BuildVersion, BuildVersionToString(BuildVersion));
            manager.EditSetting(_SyncHistory, SyncHistory.ToString());
            manager.EditSetting(_SyncBookmarks, SyncBookmarks.ToString());
            manager.EditSetting(_SyncInterval, SyncInterval.ToString());
        }

        #endregion

        #region Parse

        #region Build Version

        private const string _Public = "%pub%";
        private const string _Beta = "%bet%";
        private const string _Alpha = "%alph%";

        /// <summary>
        /// Get a BuildVersion from String
        /// </summary>
        /// <param name="BuildVersionString">String to convert</param>
        /// <returns>BuildVersion refers to string</returns>
        public static BuildVersions BuildVersionFromString(string BuildVersionString)
        {
            switch (BuildVersionString)
            {
                case _Public:
                    return BuildVersions.Public;
                case _Beta:
                    return BuildVersions.Beta;
                case _Alpha:
                    return BuildVersions.Alpha;
            }

            //Returns the defualt one
            return BuildVersions.Public;
        }

        /// <summary>
        /// Get a String from BuildVersion
        /// </summary>
        /// <param name="buildVersion">BuildVersion to convert</param>
        /// <returns>String refers to BuildVersion</returns>
        public static string BuildVersionToString(BuildVersions buildVersion)
        {
            switch (buildVersion)
            {
                case BuildVersions.Public:
                    return _Public;
                case BuildVersions.Beta:
                    return _Beta;
                case BuildVersions.Alpha:
                    return _Alpha;
            }

            //Returns the default one
            return _Public;
        }

        #endregion

        #region UserAgent

        private const string _default = "%DefaultU%";
        private const string _Chrome = "%ChromeUserAgent%";
        private const string _Firefox = "%FirefoxUserAgent%";

        /// <summary>
        /// Gets a UserAgent from a string
        /// </summary>
        /// <param name="UserAgentString">String to convert</param>
        /// <returns>UserAgent refers to string</returns>
        public static UserAgents UserAgentFromString(string UserAgentString)
        {
            switch (UserAgentString)
            {
                case _default:
                    return UserAgents.Default;
                case _Chrome:
                    return UserAgents.Chrome;
                case _Firefox:
                    return UserAgents.Firefox;
            }

            //Returns the default user agent
            return UserAgents.Default;
        }

        /// <summary>
        /// Gets a string from a UserAgent
        /// </summary>
        /// <param name="userAgent">UserAgent to convert</param>
        /// <returns>String refers to UserAgent</returns>
        public static string UserAgentToString(UserAgents userAgent)
        {
            switch (userAgent)
            {
                case UserAgents.Default:
                    return _default;
                case UserAgents.Chrome:
                    return _Chrome;
                case UserAgents.Firefox:
                    return _Firefox;
            }

            //Returns the default UserAgent
            return _default;
        }

        #endregion

        #region History Engine

        private const string _SyncAll = "%SA%";
        private const string _SyncNone = "%SN%";

        /// <summary>
        /// Get a HistoryEngine from a string
        /// </summary>
        /// <param name="HistoryString">A string value to convert</param>
        /// <returns>HistoryEngine refers to String</returns>
        public static HistoryEngines HistoryEngineFromString(string HistoryString)
        {
            switch (HistoryString)
            {
                case _SyncAll:
                    return HistoryEngines.SyncAll;
                case _SyncNone:
                    return HistoryEngines.NoSync;
            }

            //Returns default value
            return HistoryEngines.SyncAll;
        }

        /// <summary>
        /// Get a String from a HistoryEngine
        /// </summary>
        /// <param name="historyEngine">A HistoryEngine to convert</param>
        /// <returns>A string refers to HistoryEngine</returns>
        public static string HistoryEngineToString(HistoryEngines historyEngine)
        {
            switch (historyEngine)
            {
                case HistoryEngines.SyncAll:
                    return _SyncAll;
                case HistoryEngines.NoSync:
                    return _SyncNone;
            }

            //Returns default value
            return _SyncAll;
        }

        #endregion 

        #region Search Engine

        private const string _google = "%googl";
        private const string _yahoo = "%yah%";
        private const string _bing = "%bng%";

        /// <summary>
        /// Get a SearchEngine from a string
        /// </summary>
        /// <param name="searchString">A string value to convert</param>
        /// <returns>A SearchEngine refers to string</returns>
        public static SearchEngines SearchEngineFromString(string searchString)
        {
            switch (searchString)
            {
                case _google:
                    return SearchEngines.Google;
                case _yahoo:
                    return SearchEngines.Yahoo;
                case _bing:
                    return SearchEngines.Bing;
            }

            //If values null, return default searchengine
            return SearchEngines.Google;
        }

        /// <summary>
        /// Get a String from a Search Engine
        /// </summary>
        /// <param name="searchString">A string value to convert</param>
        /// <returns>A SearchEngine refers to String</returns>
        public static string SearchEngineToString(SearchEngines searchEng)
        {
            switch (searchEng)
            {
                case SearchEngines.Google:
                    return _google;
                case SearchEngines.Yahoo:
                    return _yahoo;
                case SearchEngines.Bing:
                    return _bing;
            }

            //if values null, return default searchengine
            return _google;
        }

        #endregion

        #region BrowserEngine

        private const string _cefSharp = "cefSharp";
        private const string _geckoUI = "geckoUI";

        /// <summary>
        /// Gets a BrowserEngine from a string
        /// </summary>
        /// <param name="browserString">A string value to convert</param>
        /// <returns>a BrowserEngine refers to string</returns>
        public static BrowserEngines BrowserEngineFromString(string browserString)
        {
            switch (browserString)
            {
                case _cefSharp:
                    return BrowserEngines.CefSharp;
                case _geckoUI:
                    return BrowserEngines.GeckoUI;
            }

            //If values are none, returns the default browser
            return BrowserEngines.CefSharp;
        }

        /// <summary>
        /// Returns a string from a BrowserEngine
        /// </summary>
        /// <param name="browserEngine">Browser engine value to convert</param>
        /// <returns>A string refers to BrowserEngine</returns>
        public static string BrowserEngineToString(BrowserEngines browserEngine)
        {
            switch (browserEngine)
            {
                case BrowserEngines.CefSharp:
                    return _cefSharp;
                case BrowserEngines.GeckoUI:
                    return _geckoUI;
            }

            //If the values are none, return the default browser
            return _cefSharp;
        }

        #endregion

        #endregion

    }
}
