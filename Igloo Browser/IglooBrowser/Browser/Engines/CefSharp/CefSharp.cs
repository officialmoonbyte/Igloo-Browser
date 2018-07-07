using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Igloo.Events;
using Igloo.History;
using Igloo.Logger;
using Igloo.Resources.lib;
using Igloo.Settings;
using IglooBrowser.Browser.Engines;
using IndieGoat.MaterialFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TheDuffman85.Tools;

namespace Igloo.Engines.CefSharp.Lib
{
    #region Startup Methods

    public class VoidCef
    {

        //Vars
        static CefSettings cfSettings;

        public static void InitializeCefSharp()
        {
            //Initialize CefSharp
            Cef.Initialize(GetCefSettings());

            ILogger.AddToLog("CEFSHARP", "Initialization Completed!");
        }

        private static CefSettings GetCefSettings()
        {
            //Getting the Cache directory
            string CacheDirectory = @"C:\MoonByte\" + ResourceInformation.ApplicationName + @" Cache\";

            // CEFSharp - Editing settings and browser Cache with CEFSHARP //
            cfSettings = new CefSettings();

            //EditCefCommandLine("renderer-process-limit", "1");
            //EditCefCommandLine("renderer-startup-dialog", "1");
            //EditCefCommandLine("enable-media-stream", "1"); //Enable WebRTC
            //EditCefCommandLine("no-proxy-server", "1"); //Don't use a proxy server, always make direct connections. Overrides any other proxy server flag that are passed.
            //EditCefCommandLine("debug-plugin-loading", "1"); //Dumps extra logging about plugin loading to the log file.
            //EditCefCommandLine("disable-plugins-discovery", "1"); //Disable discovering third-party plugins. Effectively loading only ones shipped with the browser plus third-party ones as specified by --extra-plugin-dir and --load-plugin switches
            //EditCefCommandLine("enable-system-flash", "1"); //Automatically discovered and load a system-wide installation of Pepper Flash.
            //EditCefCommandLine("allow-running-insecure-content", "1"); //By default, an https page cannot run JavaScript, CSS or plugins from http URLs. This provides an override to get the old insecure behavior. Only available in 47 and above.

            //EditCefCommandLine("enable-logging", "1"); //Enable Logging for the Renderer process (will open with a cmd prompt and output debug messages - use in conjunction with setting LogSeverity = LogSeverity.Verbose;)
            //settings.LogSeverity = LogSeverity.Verbose; // Needed for enable-logging to output messages

            //EditCefCommandLine("disable-extensions", "1"); //Extension support can be disabled
            //EditCefCommandLine("disable-pdf-extension", "1"); //The PDF extension specifically can be disabled

            //Load the pepper flash player that comes with Google Chrome - may be possible to load these values from the registry and query the dll for it's version info (Step 2 not strictly required it seems)
            EditCefCommandLine("ppapi-flash-path", @"C:\Program Files (x86)\Google\Chrome\Application\47.0.2526.106\PepperFlash\pepflashplayer.dll"); //Load a specific pepper flash version (Step 1 of 2)
            EditCefCommandLine("ppapi-flash-version", "20.0.0.228"); //Load a specific pepper flash version (Step 2 of 2)
            EditCefCommandLine("enable-npapi", "1");

            //NOTE: For OSR best performance you should run with GPU disabled:
            // `--disable-gpu --disable-gpu-compositing --enable-begin-frame-scheduling`
            // (you'll loose WebGL support but gain increased FPS and reduced CPU usage).
            // http://magpcss.org/ceforum/viewtopic.php?f=6&t=13271#p27075
            //https://bitbucket.org/chromiumembedded/cef/commits/e3c1d8632eb43c1c2793d71639f3f5695696a5e8

            //NOTE: The following function will set all three params
            //cfSettings.SetOffScreenRenderingBestPerformanceArgs();
            //EditCefCommandLine("disable-gpu", "1");
            //EditCefCommandLine("disable-gpu-compositing", "1");
            //EditCefCommandLine("enable-begin-frame-scheduling", "1");

            //EditCefCommandLine("disable-gpu-vsync", "1");

            //Disables the DirectWrite font rendering system on windows.
            //Possibly useful when experiencing blury fonts.
            //EditCefCommandLine("disable-direct-write", "1");

            cfSettings.IgnoreCertificateErrors = true;
            cfSettings.CachePath = CacheDirectory;
            cfSettings.UserAgent = UserAgents.GetUserAgent();

            Directory.CreateDirectory(CacheDirectory);

            return cfSettings;
        }

        private static void EditCefCommandLine(string key, string Value)
        {
            //Add to command line
            cfSettings.CefCommandLineArgs.Add(key, Value);

            //logging
            ILogger.AddToLog("CefSharp", "Added : " + key + " with a value of : " + Value);
        }
    }

    #endregion

    public class CefSharpHandle : BrowserEngineInterface
    {
        #region Vars

        //Parents of the browser
        MaterialTabPage mainTabPage;

        //Browser Control
        ChromiumWebBrowser browser;

        //Local var to check if the vpn is enabled
        bool VPNEnabled = false;
        bool isFirstLoad = false;

        #endregion

        #region Event's

        public event EventHandler<DocumentTitleChange> OnTitleChange;
        public event EventHandler<DocumentURLChange> OnDocumentURLChange;
        public event EventHandler<EventArgs> LoadingStateChanged;
        public event EventHandler<EventArgs> FirstLoad;

        #endregion

        #region ChromiumWebBrowser

        /// <summary>
        /// Initialize the browser
        /// </summary>
        public void CreateBrowserHandle(string URL, MaterialTabPage tabPage)
        {
            //Initializing the new browser
            browser = new ChromiumWebBrowser(URL);

            //Setting all of the events of the browser
            browser.TitleChanged += browser_TitleChanged;
            browser.AddressChanged += browser_AddressChanged;
            browser.LoadingStateChanged += browser_LoadingStateChanged;
            browser.FrameLoadEnd += (sender, args) =>
            {
                browser.InvokeOnUiThreadIfRequired(() =>
                {
                    //Runs the first load event args
                    if (!isFirstLoad) { isFirstLoad = true; FirstLoad?.Invoke(this, new EventArgs()); }
                });
            };

            //Setting the main tab page
            mainTabPage = tabPage;
            mainTabPage.BackColor = Color.Black;

            //Setting the browser objects
            browser.DownloadHandler = new DownloadHandler();
            browser.DisplayHandler = new DisplayHandler(this);
            browser.LifeSpanHandler = new LifespanHandler(tabPage);
            browser.MenuHandler = new ContextMenuHandler(tabPage);

        }

        /// <summary>
        /// Returns the browser object
        /// </summary>
        /// <returns>The browser object</returns>
        public System.Windows.Forms.Control GetBrowser()
        {
            return browser;
        }

        public MaterialTabPage getTabPage()
        {
            return mainTabPage;
        }

        #endregion

        #region Document Method's

        /// <summary>
        /// Changes the Doucment or Title of the Browser Tab Page
        /// </summary>
        /// <param name="URL">The URL of the browser</param>
        /// <param name="Title">The Title of the Document</param>
        public void ChangeDocumentURL(string URL) { OnDocumentURLChange?.Invoke(browser, new DocumentURLChange { DocumentURL = URL }); }
        public void ChangeTitle(string Title) { OnTitleChange?.Invoke(browser, new DocumentTitleChange { DocumentTitle = Title }); }

        #endregion

        #region TabIcon

        /// <summary>
        /// Changes the local TabIcon for the TabBrowser
        /// </summary>
        /// <param name="icon">A image of the browser icon</param>
        public void ChangeTabIcon(Image ico)
        {
            mainTabPage.ChangeTabIcon(ico);
        }

        #endregion

        #region Browser Event's

        /// <summary>
        /// Used to set the search bar text to the URI of the website.
        /// </summary>
        private void browser_AddressChanged(object sender, AddressChangedEventArgs args)
        {

            //Invokes on UI thread
            mainTabPage.InvokeOnUiThreadIfRequired(() =>
            {
                //Sets the search bar to the address of the web browser.
                OnDocumentURLChange?.Invoke(browser, new DocumentURLChange { DocumentURL = args.Address });

                //Activating the History Command
                AddHistoryEntry(args.Address, HistoryValueType.URL);
            });

            Image iconIco = Favicon.GetFromUrl(args.Address).Icon;

            //Gets the tmp Path to store all of the icons.
            string tmpPath = Path.GetTempPath() + @"MoonByte\" + ResourceInformation.ApplicationName + @"\";

            mainTabPage.ChangeTabIcon(iconIco);

        }

        private void browser_TitleChanged(object sender, TitleChangedEventArgs args)
        {
            mainTabPage.InvokeOnUiThreadIfRequired(() =>
            {

                //string adblock = @"function C(d,o){v=d.createElement('div');o.parentNode.replaceChild(v,o);}function A(d){for(j=0;t=["", Interaction.IIf(browser.Address.Contains(""youtube.com""), ""'iframe','marquee'"", ""'iframe','embed','marquee'"")), ""][j];++j){o=d.getElementsByTagName(t);for(i=o.length-1;i>=0;i--)C(d,o[i]);}g=d.images;for(k=g.length-1;k>=0;k--)if({'21x21':1,'48x48':1,'60x468':1,'88x31':1,'88x33':1,'88x62':1,'90x30':1,'90x32':1,'90x90':1,'100x30':1,'100x37':1,'100x45':1,'100x50':1,'100x70':1,'100x100':1,'100x275':1,'110x50':1,'110x55':1,'110x60':1,'110x110':1,'120x30':1,'120x60':1,'120x80':1,'120x90':1,'120x120':1,'120x163':1,'120x181':1,'120x234':1,'120x240':1,'120x300':1,'120x400':1,'120x410':1,'120x500':1,'120x600':1,'120x800':1,'125x40':1,'125x60':1,'125x65':1,'125x72':1,'125x80':1,'125x125':1,'125x170':1,'125x250':1,'125x255':1,'125x300':1,'125x350':1,'125x400':1,'125x600':1,'125x800':1,'126x110':1,'130x60':1,'130x65':1,'130x158':1,'130x200':1,'132x70':1,'140x55':1,'140x350':1,'145x145':1,'146x60':1,'150x26':1,'150x60':1,'150x90':1,'150x100':1,'150x150':1,'155x275':1,'155x470':1,'160x80':1,'160x126':1,'160x600':1,'180x30':1,'180x66':1,'180x132':1,'180x150':1,'194x165':1,'200x60':1,'220x100':1,'225x70':1,'230x30':1,'230x33':1,'230x60':1,'234x60':1,'234x68':1,'240x80':1,'240x300':1,'250x250':1,'275x60':1,'280x280':1,'300x60':1,'300x100':1,'300x250':1,'320x50':1,'320x70':1,'336x280':1,'350x300':1,'350x850':1,'360x300':1,'380x112':1,'380x250':1,'392x72':1,'400x40':1,'400x50':1,'425x600':1,'430x225':1,'440x40':1,'464x62':1,'468x16':1,'468x60':1,'468x76':1,'468x120':1,'468x248':1,'470x60':1,'480x400':1,'486x60':1,'545x90':1,'550x5':1,'600x30':1,'720x90':1,'720x300':1,'725x90':1,'728x90':1,'734x96':1,'745x90':1,'750x25':1,'750x100':1,'750x150':1,'850x120':1}[g[k].width+'x'+g[k].height])C(d,g[k]);}A(document);for(f=0;z=frames[f];++f)A(z.document)";
                //browser.ExecuteScriptAsync(adblock);

                try
                {
                    //Sets a string for the title
                    string p_title = args.Title;

                    //Activating the History title
                    AddHistoryEntry(args.Title, HistoryValueType.Title);

                    //Change the string to a list of Chars
                    List<Char> p_List = p_title.ToCharArray().ToList();

                    //Initialize the final string and the max length.
                    string finalString = string.Empty;
                    int maxStringLength = 18;

                    //Finally initialize the end string
                    string endString = "...";

                    //For loop for each int in maxStringLength
                    for (int i = 0; i < maxStringLength; i++)
                    {
                        //Initialize a string for the tmpChar
                        string tmpChar;

                        //Tries to set the Char in the List to a String
                        try { tmpChar = p_List[i].ToString(); } catch { break; }
                        if (tmpChar == null) break;

                        //Adds to the final string
                        finalString = finalString + tmpChar;

                        //If the string is equal to max length, adds the end string.
                        if (i == maxStringLength - 1) { finalString = finalString + endString; break; }
                    }

                    //Returns the event
                    OnTitleChange?.Invoke(browser, new DocumentTitleChange { DocumentTitle = finalString });
                }
                catch { }

            });
        }

        /// <summary>
        /// Changes the loading status
        /// </summary>
        private void browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
            {
                ChangeLoadingState(true);
            }
            else
            {
                ChangeLoadingState(false);
            }
        }

        #endregion

        #region VPN

        /// <summary>
        /// Used to get the VPN status
        /// </summary>
        /// <returns></returns>
        public bool GetVPNStatus()
        {
            return VPNEnabled;
        }

        /// <summary>
        /// Used to toggle the VPN service
        /// </summary>
        public bool ToggleVPNService()
        {
            if (VPNEnabled)
            {
                return TurnOffVPNService();
            }
            else
            {
                return TurnOnVPNService();
            }
        }

        /// <summary>
        /// Used to turn on the VPN service if subscribed
        /// </summary>
        public bool TurnOnVPNService()
        {
            //Set the proxy server IP and Port
            string IP = "localhost";
            string port = "5567";

            //Setting resource information to subscribed
            ResourceInformation.IsSubscribed = true;

            //Check if the user is subscribed
            if (!ResourceInformation.IsSubscribed)
            {
                Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    //Changing the Proxy Setting
                    var rc = browser.GetBrowser().GetHost().RequestContext;
                    var dict = new Dictionary<string, object>();
                    dict.Add("mode", "fixed_servers");
                    dict.Add("server", "" + IP + ":" + port + "");

                    //Configuring the error message for logging
                    string error; bool suc = rc.SetPreference("proxy", dict, out error);

                    if (suc)
                    {
                        //Log a sucessful change
                        ILogger.AddToLog("CefSharp", "Sucessfully changed proxy mode to true");

                        //Returns true
                        return true;
                    }
                    else
                    {
                        //Log a unsucessful change
                        ILogger.AddToLog("CEFSharp", "Unsucessfully changed proxy mode. Error : " + error);

                        //Return false
                        return false;
                    }
                });
            }

            //Return false
            return false;
        }

        //Used to turn off the VPN service
        public bool TurnOffVPNService()
        {

            //Set the proxy server IP and Port
            string IP = "localhost";
            string port = "5567";

            Cef.UIThreadTaskFactory.StartNew(delegate
            {
                //Changing the Proxy Setting
                var rc = browser.GetBrowser().GetHost().RequestContext;
                var dict = new Dictionary<string, object>();
                dict.Add("mode", "direct");
                dict.Add("server", "" + IP + ":" + port + "");

                //Configuring the error message for logging
                string error; bool suc = rc.SetPreference("proxy", dict, out error);

                if (suc)
                {
                    //Log a sucessful change
                    ILogger.AddToLog("CefSharp", "Sucessfully changed proxy mode to true");

                    //Returns true
                    return true;
                }
                else
                {
                    //Log a unsucessful change
                    ILogger.AddToLog("CEFSharp", "Unsucessfully changed proxy mode. Error : " + error);

                    //Return false
                    return false;
                }
            });
            // Returning false
            return false;
        }

        #endregion

        #region Browser States

        /// <summary>
        /// Navigates to a URL
        /// </summary>
        /// <param name="URL">Direct link of the browser</param>
        public void Navigate(string URL)
        {
            Console.WriteLine("navigated");
            browser.Load(URL);
        }

        /// <summary>
        /// Sends the browser states to Back
        /// </summary>
        public void OnBack()
        {
            browser.Back();
        }

        /// <summary>
        /// Sends the browser state to forward
        /// </summary>
        public void OnForward()
        {
            browser.Forward();
        }

        #endregion

        #region History

        //Value type for setting the Title and URL local vars
        private enum HistoryValueType { Title, URL }

        //String to store the Title and URL of the browser
        string egTitle = null; string egUrl = null;

        /// <summary>
        /// Used to add a HistoryEntry to the History.
        /// </summary>
        /// <param name="p_egValue">Value for the Value Type</param>
        /// <param name="type">To find what the Value Type is (URL or Title)</param>
        private void AddHistoryEntry(string p_egValue, HistoryValueType type)
        {
            //Set both of the Title and the URL depending on the ValueType
            if (type == HistoryValueType.Title) egTitle = p_egValue;
            if (type == HistoryValueType.URL) egUrl = p_egValue;

            //Chekcs if the local url and local title var is equal to null
            if (egUrl != null && egTitle != null)
            {
                //Add a new entry to the History
                IHistory.AddToHistory(egUrl, DateTime.Now.ToString(), egTitle);

                //Set local url and local title var to null
                egUrl = null;
                egTitle = null;
            }
        }

        #endregion

        #region Loading

        bool _IsLoading = false;

        public void ChangeLoadingState(bool Value)
        {
            _IsLoading = Value;
            LoadingStateChanged?.Invoke(this, new EventArgs());
        }

        public bool IsLoading() { return _IsLoading; }

        #endregion

        #region Back / Forward available

        public bool IsBackAvailiable()
        {
            return browser.GetBrowser().CanGoBack;
        }

        public bool IsForwardAvailable()
        {
            return browser.GetBrowser().CanGoForward;
        }

        #endregion
    }
}
