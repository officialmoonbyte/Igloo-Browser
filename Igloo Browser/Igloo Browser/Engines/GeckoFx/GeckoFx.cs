using Gecko;
using Igloo.Events;
using Igloo.History;
using IndieGoat.MaterialFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Igloo.Engines.GeckoFx
{
    #region Startup

    public class VoidGecko
    {

        /// <summary>
        /// Initialize XpCom for Gecko
        /// </summary>
        public static void InitializeGecko()
        {
            Xpcom.Initialize("Firefox");
        }

    }

    #endregion

    public class Geckofx : BrowserEngineInterface
    {

        #region Vars

        //Parent of the browser
        MaterialTabPage mainTabPage;

        //Browser Control
        GeckoWebBrowser browser;

        //Local var to check if vpn is enabled
        bool VPNEnabled = false;

        #endregion

        #region Event's

        public event EventHandler<DocumentTitleChange> OnTitleChange;
        public event EventHandler<DocumentURLChange> OnDocumentURLChange;
        public event EventHandler<EventArgs> LoadingStateChanged;

        #endregion

        #region GeckoWebBrowser

        /// <summary>
        /// Used to initialize the browser instance
        /// </summary>
        /// <param name="URL">Start page of the browser</param>
        /// <param name="tabPage">Hosted tab page</param>
        public void CreateBrowserHandle(string URL, MaterialTabPage tabPage)
        {
            //Set the default tab page
            tabPage = mainTabPage;

            //Initialize the new browser
            browser = new GeckoWebBrowser { Dock = DockStyle.Fill };

            //Navigate to the url
            if (URL != null) browser.Navigate(URL);
        }

        /// <summary>
        /// Returns the browser object
        /// </summary>
        /// <returns>Browser Object</returns>
        public System.Windows.Forms.Control GetBrowser()
        {
            return browser;
        }

        /// <summary>
        /// Returns the tab page the browser is hosted on
        /// </summary>
        public MaterialTabPage getTabPage()
        {
            return mainTabPage;
        }

        #endregion

        #region Document Method

        /// <summary>
        /// Changes the Doucment or Title of the Browser Tab Page
        /// </summary>
        /// <param name="URL">The URL of the browser</param>
        /// <param name="Title">The Title of the Document</param>
        public void ChangeDocumentURL(string URL) { OnDocumentURLChange?.Invoke(browser, new DocumentURLChange { DocumentURL = URL }); }
        public void ChangeTitle(string Title) { OnTitleChange?.Invoke(browser, new DocumentTitleChange { DocumentTitle = Title }); }

        #endregion

        #region TabIcon

        public void ChangeTabIcon(Image ico)
        {
            mainTabPage.ChangeTabIcon(ico);
        }

        #endregion

        #region Browser Event's



        #endregion

        #region VPN

        /// <summary>
        /// Used to get the VPN status
        /// </summary>
        public bool GetVPNStatus()
        {
            return VPNEnabled;
        }

        /// <summary>
        /// Used to toggle the VPN service
        /// </summary>
        public bool ToggleVPNService()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to turn on the VPN service
        /// </summary>
        public bool TurnOffVPNService()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to turn off the VPN service
        /// </summary>
        public bool TurnOnVPNService()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Browser States

        /// <summary>
        /// Navigates to a URL
        /// </summary>
        /// <param name="URL">Direct link of the browser</param>
        public void Navigate(string URL)
        {
            browser.Navigate(URL);
        }

        /// <summary>
        /// Sends the browser back one page
        /// </summary>
        public void OnBack()
        {
            browser.GoBack();
        }

        /// <summary>
        /// Sends the browser forward one page
        /// </summary>
        public void OnForward()
        {
            browser.GoForward();
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

        //Local var Loading
        bool _IsLoading = false;

        /// <summary>
        /// Changes the loading state of the browser
        /// </summary>
        /// <param name="Value">Loading state</param>
        public void ChangeLoadingState(bool Value)
        {
            //Set the local var to value
            _IsLoading = Value;

            //Invoke the method
            LoadingStateChanged?.Invoke(this, new EventArgs());
        }

        //Returns the loading var
        public bool IsLoading() { return _IsLoading; }

        #endregion

        #region Back / Forward available

        public bool IsBackAvailiable()
        {
            return browser.CanGoBack;
        }

        public bool IsForwardAvailable()
        {
            return browser.CanGoForward;
        }

        #endregion

    }
}
