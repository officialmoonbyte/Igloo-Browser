using IndieGoat.MaterialFramework.Controls;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class Form1 : MaterialForm
    {

        #region Timers

        System.Windows.Forms.Timer WelcomeFadeIn = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer WelcomeFadeOut = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer Layer2SlideIn = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer Layer3SlideIn = new System.Windows.Forms.Timer();

        #endregion

        #region Installer Object

        Installer installer = new Installer();

        #endregion

        #region Tick Count

        int WelcomeFadeIn_TickA;
        int WelcomeFadeOut_TickA;

        #endregion

        #region Startup

        /// <summary>
        /// Before initialization of the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Startup of the form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Startup for all objects
            lbl_Welcome.Location = new Point((this.Width / 2) - lbl_Welcome.Width / 2, (this.Height / 2) - lbl_Welcome.Height / 2);
            lbl_Welcome.ForeColor = Color.White;
            txt_Layer1_Directory.Text = installer.DefaultDirectory;

            pgb_Download.BackColor = Color.White;
            pgb_Download.BorderColor = Color.FromArgb(244, 244, 244);
            pgb_Install.BackColor = Color.White;
            pgb_Install.BorderColor = Color.FromArgb(244, 244, 244);

            cek_Layer3_Desktop.Checked = true;
            cek_Layer3_StartMenu.Checked = true;

            Console.WriteLine("Width : " + this.Width + ", Height : " + this.Height);
            Console.WriteLine("Calc point 1 : " + new Point((this.Width / 2) - ((btn_Layer3_FinishButton.Width / 2))));

            //Hides all of the panels
            layer1.Location = new Point(322, 29);
            layer2.Location = new Point(322, 29);
            Layer3.Location = new Point(322, 29);

            //All timer starts
            #region Timer Events

            #region Welcome label

            #region Fade In

            WelcomeFadeIn.Tick += (obj, args) =>
            {
                //Increase the tick amount by 1
                WelcomeFadeIn_TickA++;

                if (WelcomeFadeIn_TickA > 26)
                {
                    int tmpTick = WelcomeFadeIn_TickA - 26;

                    //Checks if the color of the label is less then 
                    if (lbl_Welcome.ForeColor.R < (255 - (tmpTick * 16))) { lbl_Welcome.ForeColor = Color.Black; }
                    else
                    {
                        //Sets the fore color of the label to black
                        int RGBNum = 255 - (tmpTick * 16);
                        if (RGBNum < 2) { lbl_Welcome.ForeColor = Color.Black; }
                        else
                        { lbl_Welcome.ForeColor = Color.FromArgb(RGBNum, RGBNum, RGBNum); }
                    }

                    //Activates the fade out timer
                    if (tmpTick == 35)
                    {
                        WelcomeFadeOut.Start();
                        WelcomeFadeIn.Stop();
                    }
                }
            };

            #endregion

            #region Fade Out

            WelcomeFadeOut.Tick += (obj, args) =>
            {
                //Increases the tick amount by 1
                WelcomeFadeOut_TickA++;

                //Used to show the layer1 
                if ((WelcomeFadeOut_TickA * 12) > 380)
                {
                    //Shows the main layer
                    layer1.Location = new Point(1, 29);

                    

                    //Stops the WelcomeFadeOut timer
                    WelcomeFadeOut.Stop();
                }

                //Checks if the RGB output is greater then 255
                if (255 < (WelcomeFadeOut_TickA * 12))
                {
                    //Removes the control from the form
                    this.Controls.Remove(lbl_Welcome);

                    //Dispose of lbl_Welcome
                    lbl_Welcome.Dispose();
                }
                else
                {
                    //Sets the fore color of the label to white
                    int RGBNum = (WelcomeFadeOut_TickA * 12);
                    if (RGBNum > 255) { lbl_Welcome.ForeColor = Color.White; }
                    else
                    { lbl_Welcome.ForeColor = Color.FromArgb(RGBNum, RGBNum, RGBNum); }
                }
            };

            #endregion

            #endregion

            #endregion

            //Set all timer interval's
            WelcomeFadeIn.Interval = 16;
            WelcomeFadeOut.Interval = 16;
            Layer2SlideIn.Interval = 16;
            Layer3SlideIn.Interval = 16;

            //Starts timers
            WelcomeFadeIn.Start();
        }

        #endregion

        #region Layer 1 button events

        /// <summary>
        /// Used to progress through the installation of Crash
        /// </summary>
        private void btn_Layer1_Next_Click(object sender, EventArgs e)
        {
            //Checks if the directory is equal to null
            if (txt_Layer1_Directory.Text != null)
            {
                for (int frame = 0; frame < 90000; frame++)
                {
                    //Calculates the minus by value
                    int minusBy = frame * 4;

                    //Moves the main layer
                    layer1.Location = new Point(1 - minusBy, 29);
                    layer2.Location = new Point(320 - minusBy, 29);
                    

                    //Stops the timer if minus by is equal to 320
                    if (minusBy > 322)
                    {
                        layer2.Location = new Point(1, 29);
                        frame = 999999;
                    }

                    Application.DoEvents();
                    Thread.Sleep(1);
                }

                //Set the progress bar's for the installer
                installer.DownloadProgressBar = pgb_Download;
                installer.InstallProgressBar = pgb_Install;

                //Starts to download the file for the installer.
                installer.DownloadFile();

                //Installer events
                installer.DownloadFinished += (obj, args) =>
                {
                    //Extracts the download of the installer
                    installer.ExtractDownload();
                }; installer.ExtractProgressChanged += (obj, args) =>
                {
                    //Checks if the extract progress is done
                    if (installer.ExtractPercent == 100)
                    {
                        for (int frame = 0; frame < 999999; frame++)
                        {
                            if (layer2.InvokeRequired)
                            {
                                layer2.Invoke(new Action(() =>
                                {
                                    //Calculates the minus by value
                                    int minusBy = frame * 4;

                                    //Moves the layer's
                                    layer2.Location = new Point(1 - minusBy, 29);
                                    Layer3.Location = new Point(320 - minusBy, 29);

                                    //Stops the timer is minus by is equal to 320
                                    if (minusBy > 322)
                                    {
                                        Layer3.Location = new Point(1, 29);
                                        frame = 999999;
                                    }

                                    //Let's the application sync controls
                                    Application.DoEvents();
                                    Thread.Sleep(1); //Sleeps the thread for 1Ms
                                }));
                            }
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Used to close out of the application when the close button is clicked
        /// </summary>
        private void btn_Layer1_Canc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Used to get a new install directory
        /// </summary>
        private void btn_Layer1_FindDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog g = new FolderBrowserDialog())
            {
                g.ShowDialog();
                txt_Layer1_Directory.Text = g.SelectedPath;
            }
        }

        #endregion

        #region Layer 3 Method's and Events

        private void btn_Layer3_FinishButton_Click(object sender, EventArgs e)
        {
            //Create all of the shortcuts
            if (cek_Layer3_Desktop.Checked) CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            if (cek_Layer3_StartMenu.Checked) CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

            //Closes out of the process
            this.Close(); Application.Exit(); Environment.Exit(1);
        }

        #endregion

        #region Create Shortcut

        public void CreateShortcut(string ShortcutDirectory)
        {
            var wsh = new IWshShell_Class();
            IWshShortcut shortcut = wsh.CreateShortcut(ShortcutDirectory + "\\Crash.lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.Arguments = "";
            shortcut.TargetPath = installer.DefaultDirectory + @"\Crash.exe";
            // not sure about what this is for
            shortcut.WindowStyle = 1;
            shortcut.Description = "A web browser made by MoonByte";
            shortcut.WorkingDirectory = installer.DefaultDirectory;
            shortcut.IconLocation = installer.DefaultDirectory + @"\icon.ico";
            shortcut.Save();
        }

        #endregion
    }
}
