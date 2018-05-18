using CefSharp.WinForms;
using Igloo.Download;
using Igloo.Engines;
using Igloo.Engines.CefSharp.Lib;
using Igloo.Events;
using Igloo.Logger;
using Igloo.Pages.Settings;
using Igloo.Resources.lib;
using Igloo.Settings;
using IndieGoat.MaterialFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Igloo.Control.Browser
{
    public class IBrowser
    {

        #region Vars

        //The return TabPage
        public MaterialTabPage tabPage = new MaterialTabPage();

        //Download handler for the Browser
        DownloadRequest p_DownloadItem = Settings.Settings.downloadItem;

        //Host control for the IBrowser Control
        Panel pnl = new Panel();

        //Header of the browser.
        Igloo.Control.Browser.IBrowserHeader BrowserHeader;

        //Browser Class
        BrowserEngineInterface browser;

        public enum BrowserType { CefSharp, Gecko }

        #endregion

        #region MaterialTabPage

        /// <summary>
        /// Used to get the browser tab with controls inside contaiing the IBrowserObject
        /// </summary>
        /// <param name="URL">If used, start the browser with that URL</param>
        /// <returns>A empty Tab with events attached to it to populate it</returns>
        public MaterialTabPage getIBrowserTab(BrowserType browserType, Size defaultSize)
        { return getIBrowserTab(browserType, null, defaultSize); }
        public MaterialTabPage getIBrowserTab(string URL, Size defaultSize)
        { return getIBrowserTab(BrowserType.CefSharp, URL, defaultSize); }
        public MaterialTabPage getIBrowserTab(string URL, BrowserType browserType, Size defaultSize)
        { return getIBrowserTab(browserType, URL, defaultSize); }
        public MaterialTabPage getIBrowserTab(BrowserType browserType, string URL, Size defaultSize)
        {
            // TabPage //
            tabPage.Size = defaultSize;
            tabPage.BackColor = Color.White;
            //tabPage.ChangeTabIcon(Image.FromFile(Application.StartupPath + "\\settingslogo.ico"));

            // BrowserHandler //
            BrowserHeader = new Igloo.Control.Browser.IBrowserHeader(defaultSize.Width);
            BrowserHeader.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            BrowserHeader.Location = new Point(0, 0);

            tabPage.Controls.Add(BrowserHeader);

            // Browser Panel //
            pnl.Location = new Point(0, BrowserHeader.Height);
            pnl.Size = new Size(tabPage.Width, tabPage.Height - BrowserHeader.Height);
            pnl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            pnl.BackColor = Color.White;

            tabPage.Controls.Add(pnl);

            // IBrowser //
            GenerateBrowserHandle();

            //Initialize the browser
            browser.CreateBrowserHandle(URL, tabPage);

            //Setting the browser event's
            BrowserHeader.SendForwardClick += ((obj, args) => { browser.OnForward(); });
            BrowserHeader.SendBackClick += ((obj, args) => { browser.OnBack(); });
            BrowserHeader.VPNButtonClicked += ((obj, args) =>
            {
                bool vpnSuc = browser.ToggleVPNService();

                if (vpnSuc)
                {
                    if (browser.GetVPNStatus())
                    {
                        BrowserHeader.ChangeVPNText("Disable VPN");
                    }
                    else
                    {
                        BrowserHeader.ChangeVPNText("Enable VPN");
                    }
                }
            });
            BrowserHeader.TextSearch_EnterPressed += ((obj, args) =>
            {
                string searchEntry = null;

                if (!CheckURLValid(BrowserHeader.GetTXTSearchValue()))
                {
                    searchEntry = GetSearchLink(BrowserHeader.GetTXTSearchValue());
                }
                else { searchEntry = BrowserHeader.GetTXTSearchValue(); }

                browser.Navigate(searchEntry);
            });
            BrowserHeader.SettingsClicked += ((obj, args) =>
            {

                //Modifying the settings form
                SettingsForm settings = new SettingsForm();
                settings.TopLevel = false;
                settings.Visible = true;
                settings.FormBorderStyle = FormBorderStyle.None;
                settings.Dock = DockStyle.Fill;

                //Setting TabControl as local var
                MaterialTabControl parentControl = (MaterialTabControl)tabPage.Parent;

                //Creating a new MaterialTabPage and adding the form as a control
                MaterialTabPage settingsTab = new MaterialTabPage();
                settingsTab.Text = ResourceInformation.ApplicationName + " Settings";
                settingsTab.Controls.Add(settings);

                //Add MaterialTabPage to the TabControl and select that tab
                parentControl.TabPages.Add(settingsTab);
                parentControl.SelectTab(settingsTab);

                //Change the tab icon
                settingsTab.ChangeTabIcon(Image.FromFile(Application.StartupPath + "\\settingslogo.ico"));
            });
            BrowserHeader.SendReloadClick += ((obj, args) =>
            {
                //Check if the browser is loading
                if (browser.IsLoading())
                {
                    //Stop loading the page
                    ((ChromiumWebBrowser)browser.GetBrowser()).GetBrowser().StopLoad();
                }
                else
                {
                    //Reload the page
                    ((ChromiumWebBrowser)browser.GetBrowser()).GetBrowser().Reload();
                }
            });
            browser.OnTitleChange += ((obj, args) =>
            {
                DocumentTitleChange e = args;

                //Change the text of the TabPage to the title.
                tabPage.Text = e.DocumentTitle;
            });
            browser.OnDocumentURLChange += ((obj, args) =>
            {
                DocumentURLChange e = args;

                //Change the text of the Search Bar
                BrowserHeader.ChangeTXTSearchValue(e.DocumentURL);
            });
            browser.LoadingStateChanged += ((obj, args) =>
            {
                //Chanes the look of the reload button depending on the status of the browser
                BrowserHeader.ChangeReloadStatus(browser.IsLoading());

                //Changes the IHeaderBrowser button status's
                Action action = new Action(() =>
                {
                    BrowserHeader.CanGoBackBool(browser.IsBackAvailiable());
                    BrowserHeader.CanGoForwardBool(browser.IsForwardAvailable());
                }); BrowserHeader.Invoke(new MethodInvoker(action));
            });
            browser.FirstLoad += ((obj, args) =>
            {
                pnl.Controls.Add(browser.GetBrowser());
            });

            pnl.Controls.Add(browser.GetBrowser());

            //Send the browser to the home page
            Console.WriteLine(URL);
            if (URL != null) { Console.WriteLine("Url is not null"); browser.Navigate(URL); }
            else { browser.Navigate("google.com"); }

            return tabPage;
        }

        /// <summary>
        /// Checks if it can turn a string into 
        /// </summary>
        private static bool CheckURLValid(string Soruce)
        {
            //Create a instance of the URI
            Uri uriResult;

            //Returns if the URI sucesffully creates
            return Uri.TryCreate(Soruce, UriKind.Absolute, out uriResult);
        }

        private void GenerateBrowserHandle()
        {
            SettingsManager.BrowserEngine = SettingsManager.BrowserEngines.CefSharp;
            if (SettingsManager.BrowserEngine == SettingsManager.BrowserEngines.CefSharp)
            { browser = new CefSharpHandle(); ILogger.AddToLog("IBrowser", "Using CEFSHARP as browser engine."); }
        }
        #endregion

        #region Search

        private string GetSearchLink(string urlType)
        {
            //The return value
            string searchEntry = null;

            //Get the search url by the type of SearchEngine
            if (SettingsManager.SearchEngine == SettingsManager.SearchEngines.Google)
            { searchEntry = "http://google.com/search?q=" + urlType; }
            if (SettingsManager.SearchEngine == SettingsManager.SearchEngines.Yahoo)
            { searchEntry = "http://yahoo.com/search?q=" + urlType; }
            if (SettingsManager.SearchEngine == SettingsManager.SearchEngines.Bing)
            { searchEntry = "http://bing.com/search?q=" + urlType; }

            //Set SearchEntry to the default search engine if null
            if (searchEntry == null) { searchEntry = "http://google.com/search?q=" + urlType; }

            //Return the value
            return searchEntry;
        }

        #endregion
    }
}
