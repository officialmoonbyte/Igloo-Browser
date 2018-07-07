using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Igloo.Resources.lib;
using IglooBrowser.Browser.Engines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Igloo.Engines.CefSharp.Lib
{
    /// <summary>
    /// Used for CEFSharp Display Handler
    /// </summary>
    public class DisplayHandler : IDisplayHandler
    {

        //Parent control of the ChromiumWebBrowser
        private System.Windows.Forms.Control Parent; // (Panel)

        //Creating a var for the Fullscreen form
        private Form fullScreenForm;

        //Title for the browser.
        string title;

        BrowserEngineInterface instanceBrowser;

        /// <summary>
        /// Used to set the IBrowserInstance
        /// </summary>
        public DisplayHandler(BrowserEngineInterface n)
        { instanceBrowser = n; }

        /// <summary>
        /// Triggers when the Address of the browser is changed
        /// </summary>
        public void OnAddressChanged(IWebBrowser browserControl, AddressChangedEventArgs addressChangedArgs)
        {
            //Not Implemented because we are using the event under the control for AddressChanging.
        }

        public bool OnAutoResize(IWebBrowser browserControl, IBrowser browser, global::CefSharp.Structs.Size newSize)
        {
            //Not implemented
            return false;
        }

        public bool OnAutoResize(IWebBrowser browserControl, IBrowser browser)
        {
            //Not implemented
            return false;
        }

        /// <summary>
        /// Fires when there is an update to the console
        /// </summary>
        public bool OnConsoleMessage(IWebBrowser browserControl, ConsoleMessageEventArgs consoleMessageArgs)
        {
            //Not Implemented, we currently don't have a feature using the console.
            return false;
        }

        /// <summary>
        /// Happens when the Favicon of the URL load's
        /// </summary>
        public void OnFaviconUrlChange(IWebBrowser browserControl, IBrowser browser, IList<string> urls)
        {
            //Gets the tmp Path to store all of the icons.
            string tmpPath = Path.GetTempPath() + @"MoonByte\" + ResourceInformation.ApplicationName;

            //Sets ttl to title.
            string ttl = title;

            try
            {
                //Using a web client, download's the website icon.
                using (WebClient b = new WebClient())
                {
                    if (!File.Exists(tmpPath + ttl + ".ico"))
                    {
                        b.DownloadFile(urls[0], tmpPath + ttl + ".ico");
                    }
                }

                //Setting the HistoryIconDirectory
                string historyIconDirectory = @"C:\VortexStudio\" + ResourceInformation.ApplicationName + @" Cache\HIcons\";

                //Checking if History Icon Directory exist, if not creates the directory.
                if (!Directory.Exists(historyIconDirectory)) Directory.CreateDirectory(historyIconDirectory);

                //Copy the icon file to the History Icon Directory
                if (!File.Exists(historyIconDirectory + ttl + ".ico")) File.Copy(tmpPath + ttl + ".ico", historyIconDirectory + ttl + ".ico");

            }
            catch { }

            try
            {
                //Gets icon into Image
                Image ico = Bitmap.FromFile(tmpPath + ttl + ".ico");

                //Sets the icon by SelectedTab. Will later be used to set the icon of the tab individually.
                instanceBrowser.getTabPage().ChangeTabIcon(ico);

                Console.WriteLine("Changed tab icon");
            }
            catch { }
        }

        /// <summary>
        /// Used to make the browser full screen
        /// </summary>
        public void OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {
            //Setting a new ChromiumWebBrowser
            var chromiumBrowser = (ChromiumWebBrowser)browserControl;

            //Invoke on UI thread
            chromiumBrowser.InvokeOnUiThreadIfRequired(() =>
            {
                //Checks if we are going into fullscreen
                if (fullscreen)
                {
                    //Setting the parent of the ChromiumWebBrowser
                    Parent = chromiumBrowser.Parent;

                    //Remove the tab browser from the BrowserTab
                    Parent.Controls.Remove(chromiumBrowser);

                    //Initialize fullScreenForm
                    fullScreenForm = new Form();

                    //Set Windows state and FormBorderStyle to be full screen.
                    fullScreenForm.WindowState = FormWindowState.Maximized;
                    fullScreenForm.FormBorderStyle = FormBorderStyle.None;

                    //Add the ChromiumWebBrowser to the fullscreenform.
                    fullScreenForm.Controls.Add(chromiumBrowser);

                    //Show the Dialog of the fullscreenform.
                    fullScreenForm.ShowDialog(Parent.FindForm());
                }
                else
                {
                    //Remove the ChromiumWebBrowser from the fullscreenform
                    fullScreenForm.Controls.Remove(chromiumBrowser);

                    //Add the ChromiumWebBrowser back to the parent.
                    Parent.Controls.Add(chromiumBrowser);

                    //Closes and dispose of FullScreenForm
                    fullScreenForm.Close();
                    fullScreenForm.Dispose();
                    fullScreenForm = null;
                }
            });
        }

        /// <summary>
        /// Not really sure what this is used for.
        /// </summary>
        public void OnStatusMessage(IWebBrowser browserControl, StatusMessageEventArgs statusMessageArgs)
        {

        }

        /// <summary>
        /// Fires when the title of the Browser changes
        /// </summary>
        public void OnTitleChanged(IWebBrowser browserControl, TitleChangedEventArgs titleChangedArgs)
        {
            //Initialize temp path
            string tmpPath = Path.GetTempPath() + @"MooonByte\" + ResourceInformation.ApplicationName;

            try
            {
                //Sets Title
                title = titleChangedArgs.Title;

                //Checks if icon exists
                if (File.Exists(tmpPath + title + ".ico"))
                {

                    instanceBrowser.getTabPage().Text = title;

                    try
                    {
                        //Locate and set icon by selected tab.
                        Image ico = Bitmap.FromFile(tmpPath + title + ".ico");
                        instanceBrowser.getTabPage().ChangeTabIcon(ico);

                        Console.WriteLine("Changed tab icon");
                    }
                    catch { }
                }
            }
            catch { }
        }

        /// <summary>
        /// Not really sure what this is used for.
        /// </summary>
        public bool OnTooltipChanged(IWebBrowser browserControl, string text)
        {
            return false;
        }

        public bool OnTooltipChanged(IWebBrowser browserControl, ref string text)
        {
            return false;
        }
    }
}
