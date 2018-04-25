namespace Igloo.Pages.Browser
{
    public partial class BrowserWindow : MaterialForm
    {

        #region Vars and Objects

        TabHeader tabHeader = new TabHeader();
        MaterialTabPage draggedTab = null;
        MaterialTabControl tabControl = new MaterialTabControl();
        bool StartMousePoint = false;

        #endregion

        public BrowserWindow(bool startMousePoint = false, MaterialTabPage page = null)
        {
            if (page != null) draggedTab = page;
            this.Size = new Size(1280, 720);

            this.InitializeComponent();

            StartMousePoint = startMousePoint;
        }

        protected override void OnLoad(EventArgs e)
        {

            if (StartMousePoint)
            {
                Console.WriteLine(MousePosition);
                this.Location = MousePosition;
            }

            // MaterialForm //
            this.HeaderHeight = 33;
            this.HeaderColor = Color.FromArgb(35, 35, 64);
            this.Showicon = false;
            this.ShowTitleLabel = false;
            this.Icon = new Icon(Application.StartupPath + @"\icon.ico");
            this.Text = "Crash";

            base.OnLoad(e);

            // TabHeader //
            tabHeader.Location = new Point(32, 0);
            tabHeader.BasedTabControl = tabControl;
            tabHeader.EnableAddButton = true;
            tabHeader.ShowCloseButton = true;
            tabHeader.Width = this.Width - 178 - 32;
            tabHeader.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            tabHeader.BackColor = Color.FromArgb(35, 35, 64);
            this.BorderColor = Color.FromArgb(35, 35, 64);
            tabHeader.CloseButtonHoverColor = Color.FromArgb(255, 90, 90);
            tabHeader.TabDragOut += (obj, args) =>
            {
                //Set new event args
                TabDragOutArgs Args = (TabDragOutArgs)args;

                if (Args.DraggedTab != null)
                {
                    new BrowserWindow(true, Args.DraggedTab).Show();
                }
            };

            tabHeader.NewTabButtonClick += tabHeader_NewTabButtonClick;

            this.Controls.Add(tabHeader);

            // TabControl
            tabControl.Location = new Point(1, tabHeader.Height);
            tabControl.Size = new Size(this.Width - 2, this.Height - tabHeader.Height - 2);
            tabControl.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);

            // Tabpage //
            if (draggedTab == null)
            {
                MaterialTabPage tabPage = new IBrowser().getIBrowserTab(IBrowser.BrowserType.CefSharp, "https://www.google.com/", tabControl.Size);
                tabControl.TabPages.Add(tabPage);
            }
            else
            {
                tabControl.TabPages.Add(draggedTab);
            }

            //Tabcontrol events
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += (obj, args) =>
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    if (tabControl.TabPages.Count == 0) { this.Close(); } //Closes the browser window when tabpages is equal to 0
                    Thread.Sleep(1000);
                })); thread.Start();
            };
            timer.Start();

            this.Controls.Add(tabControl);
        }

        private void tabHeader_NewTabButtonClick(object sender, NewTabButtonClickedArgs e)
        {
            //Set a private var as a local tab page
            MaterialTabPage tabPage = e.NewTabpage;

            //Set an instance of the TabBrowser
            IBrowser tabControlInst = new IBrowser();

            //Change the local tabpage of the TabBrowser to the new Tab Page
            //Then create the instance.
            tabControlInst.tabPage = tabPage;
            tabControlInst.getIBrowserTab(IBrowser.BrowserType.CefSharp, "google.com", tabPage.Size);
        }

        #region Resize

        //Prevents flickering on resize
        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);

            //Pauses tabControl design
            tabControl.SuspendLayout();
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            //Resume layout design
            tabControl.ResumeLayout();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.Invalidate();
        }

        #endregion

        private void BrowserWindow_Load(object sender, EventArgs e)
        {

        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            new BrowserWindow().Show();
        }
    }
}
