using Igloo.Events;
using IndieGoat.MaterialFramework.Controls;
using System;
using System.Drawing;

namespace Igloo.Engines
{
    //Interface for the browser.
    public interface BrowserEngineInterface
    {
        event EventHandler<DocumentTitleChange> OnTitleChange;
        event EventHandler<DocumentURLChange> OnDocumentURLChange;
        event EventHandler<EventArgs> LoadingStateChanged;
        event EventHandler<EventArgs> FirstLoad;

        void CreateBrowserHandle(string URL, MaterialTabPage tabPage);
        System.Windows.Forms.Control GetBrowser();
        void OnBack();
        void OnForward();
        void Navigate(string URL);
        void ChangeTabIcon(Image ico);
        bool ToggleVPNService();
        bool TurnOnVPNService();
        bool TurnOffVPNService();
        bool GetVPNStatus();
        void ChangeLoadingState(bool Value);
        bool IsLoading();
        bool IsBackAvailiable();
        bool IsForwardAvailable();
        MaterialTabPage getTabPage();

        void ChangeTitle(string Title);
        void ChangeDocumentURL(string Title);
    }
}
