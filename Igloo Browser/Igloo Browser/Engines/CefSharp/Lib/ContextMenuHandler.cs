namespace Igloo.Engines.CefSharp.Lib
{
    /// <summary>
    /// A context menu strip for the browser
    /// </summary>
    public class ContextMenuHandler : IContextMenuHandler
    {

        #region Custom Command Int's

        private const int Copy = 26503;
        private const int OpenInNewTab = 18293;
        MaterialTabPage mainTabPage;

        #endregion 

        public ContextMenuHandler(MaterialTabPage i) { mainTabPage = i; }

        /// <summary>
        /// Before the ContextMenu opens
        /// </summary>
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            //Remove existing Menu Items
            bool r; r = model.Remove(CefMenuCommand.ViewSource);
            r = model.Remove(CefMenuCommand.Back);
            r = model.Remove(CefMenuCommand.Forward);
            r = model.Remove(CefMenuCommand.Print);
            Console.WriteLine(parameters.UnfilteredLinkUrl);

            //Initializing custom menu items
            if (parameters.LinkUrl != "") { model.AddItem((CefMenuCommand)OpenInNewTab, "Open In New Tab"); }
            if (parameters.LinkUrl != "" || parameters.SourceUrl != "") { model.AddItem((CefMenuCommand)Copy, "Copy link Address"); }
        }

        /// <summary>
        /// When the user interacts with the context menu
        /// </summary>
        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            // Custom Commands //

            //Copy link Address
            if ((int)commandId == Copy)
            {
                //Save the link into windows clipboard
                if (parameters.LinkUrl != "") Clipboard.SetText(parameters.LinkUrl);
            }

            if ((int)commandId == OpenInNewTab)
            {
                //Validates that the LinkURL isn't null
                if (parameters.LinkUrl == "") { return false; }

                //Getting the TabControl
                MaterialTabControl basedTabControl = (MaterialTabControl)mainTabPage.Parent;

                //Creating a new Tab
                MaterialTabPage newTab = new Crash.Control.Browser.IBrowser().getIBrowserTab(parameters.LinkUrl, basedTabControl.Size);

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
            }

            return false;
        }

        /// <summary>
        /// When the ContextMenu is dismissed
        /// </summary>
        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        /// <summary>
        /// When the context menu is ran
        /// </summary>
        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
