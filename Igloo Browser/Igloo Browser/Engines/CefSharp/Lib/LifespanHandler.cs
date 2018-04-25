namespace Igloo.Engines.CefSharp.Lib
{
    /// <summary>
    /// Used to open a popup in a new tab when asked
    /// </summary>
    public class LifespanHandler : ILifeSpanHandler
    {

        //Program Download Handler
        MaterialTabPage tabPage;

        /// <summary>
        /// Sets the local DownloadItem from the Global Download Item
        /// </summary>
        /// <param name="DownloadHandler">Global Download Item to set the Local Download Item</param>
        public LifespanHandler(MaterialTabPage basedTabPage) { tabPage = basedTabPage; }

        /// <summary>
        /// Not Currently Used
        /// </summary>
        /// <param name="browserControl">ChromiumWebBrowser control</param>
        /// <param name="browser">IBrowser for the browser control</param>
        /// <returns></returns>
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        /// <summary>
        /// Not Currently Used
        /// </summary>
        /// <param name="browserControl">ChromiumWebBrowser control</param>
        /// <param name="browser">IBrowser for the browser control</param>
        /// <returns></returns>
        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        /// <summary>
        /// Not Currently Used
        /// </summary>
        /// <param name="browserControl">ChromiumWebBrowser control</param>
        /// <param name="browser">IBrowser for the browser control</param>
        /// <returns></returns>
        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }

        /// <summary>
        /// Used to show the popup of the browser in a new tab
        /// </summary>
        /// <param name="browserControl">The ChromiumWebBrowser control</param>
        /// <param name="browser">IBRowser control of the browser</param>
        /// <param name="frame">IFrame?</param>
        /// <param name="targetUrl">Target URL of the popup</param>
        /// <param name="targetFrameName">Frame of the target</param>
        /// <param name="targetDisposition"></param>
        /// <param name="userGesture">User movement</param>
        /// <param name="popupFeatures">Features of the popup (Currently not supported)</param>
        /// <param name="windowInfo">Window Info (name, etc)</param>
        /// <param name="browserSettings">Settings</param>
        /// <param name="noJavascriptAccess">Java Script Access</param>
        /// <param name="newBrowser">New browser window for the popup</param>
        /// <returns>Returns nothing to prevent a popup</returns>
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            //Getting the TabControl
            MaterialTabControl basedTabControl = (MaterialTabControl)tabPage.Parent;

            //Creating a new Tab
            MaterialTabPage newTab = new Crash.Control.Browser.IBrowser().getIBrowserTab(targetUrl, basedTabControl.Size);

            //Inovke on UI thread
            if (basedTabControl.InvokeRequired)
            {
                basedTabControl.Invoke(new Action(() =>
                {
                    //Add the new TabPage to the TabControl and then selects it
                    basedTabControl.TabPages.Add(newTab);
                    basedTabControl.SelectTab(newTab);
                }));
            }

            //Returns false
            newBrowser = null; return true;
        }
    }
}
