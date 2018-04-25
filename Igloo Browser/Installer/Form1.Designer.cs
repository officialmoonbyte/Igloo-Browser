namespace Installer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbl_Welcome = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.layer1 = new System.Windows.Forms.Panel();
            this.lbl_Layer1_InstallDist = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.btn_Layer1_FindDirectory = new IndieGoat.MaterialFramework.Controls.FlatButton();
            this.layer2 = new System.Windows.Forms.Panel();
            this.pgb_Install = new IndieGoat.MaterialFramework.Controls.MaterialProgressBar();
            this.pgb_Download = new IndieGoat.MaterialFramework.Controls.MaterialProgressBar();
            this.lbl_Layer2_Installing = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.lbl_Layer2_Downloading = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.lbl_Layer2_Title = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.txt_Layer1_Directory = new IndieGoat.MaterialFramework.Controls.MaterialTextBox();
            this.lbl_Layer1_Args = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.btn_Layer1_Canc = new IndieGoat.MaterialFramework.Controls.FlatButton();
            this.btn_Layer1_Next = new IndieGoat.MaterialFramework.Controls.FlatButton();
            this.lbl_Layer1_Title = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.Layer3 = new System.Windows.Forms.Panel();
            this.lbl_Layer3_Title = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.lbl_Layer3_StartMenu = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.cek_Layer3_Desktop = new IndieGoat.MaterialFramework.Controls.MaterialCheckBox();
            this.cek_Layer3_StartMenu = new IndieGoat.MaterialFramework.Controls.MaterialCheckBox();
            this.lbl_Layer3_Desktop = new IndieGoat.MaterialFramework.Controls.MaterialLabel();
            this.btn_Layer3_FinishButton = new IndieGoat.MaterialFramework.Controls.FlatButton();
            this.layer1.SuspendLayout();
            this.layer2.SuspendLayout();
            this.Layer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Welcome
            // 
            this.lbl_Welcome.AutoSize = true;
            this.lbl_Welcome.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Welcome.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lbl_Welcome.Location = new System.Drawing.Point(315, 355);
            this.lbl_Welcome.Name = "lbl_Welcome";
            this.lbl_Welcome.Size = new System.Drawing.Size(114, 32);
            this.lbl_Welcome.TabIndex = 2;
            this.lbl_Welcome.Text = "Welcome";
            // 
            // layer1
            // 
            this.layer1.Controls.Add(this.lbl_Layer1_InstallDist);
            this.layer1.Controls.Add(this.btn_Layer1_FindDirectory);
            this.layer1.Controls.Add(this.txt_Layer1_Directory);
            this.layer1.Controls.Add(this.lbl_Layer1_Args);
            this.layer1.Controls.Add(this.btn_Layer1_Canc);
            this.layer1.Controls.Add(this.btn_Layer1_Next);
            this.layer1.Controls.Add(this.lbl_Layer1_Title);
            this.layer1.Location = new System.Drawing.Point(395, 18);
            this.layer1.Name = "layer1";
            this.layer1.Size = new System.Drawing.Size(320, 250);
            this.layer1.TabIndex = 3;
            // 
            // lbl_Layer1_InstallDist
            // 
            this.lbl_Layer1_InstallDist.AutoSize = true;
            this.lbl_Layer1_InstallDist.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer1_InstallDist.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer1_InstallDist.Location = new System.Drawing.Point(29, 66);
            this.lbl_Layer1_InstallDist.Name = "lbl_Layer1_InstallDist";
            this.lbl_Layer1_InstallDist.Size = new System.Drawing.Size(263, 38);
            this.lbl_Layer1_InstallDist.TabIndex = 6;
            this.lbl_Layer1_InstallDist.Text = "Install Directory - Click the button on the\r\n right side to change the install di" +
    "rectory. ";
            this.lbl_Layer1_InstallDist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Layer1_FindDirectory
            // 
            this.btn_Layer1_FindDirectory.BackColor = System.Drawing.Color.White;
            this.btn_Layer1_FindDirectory.BackgroundColorClicked = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Layer1_FindDirectory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btn_Layer1_FindDirectory.BorderWidth = 0;
            this.btn_Layer1_FindDirectory.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btn_Layer1_FindDirectory.Location = new System.Drawing.Point(275, 121);
            this.btn_Layer1_FindDirectory.MouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Layer1_FindDirectory.Name = "btn_Layer1_FindDirectory";
            this.btn_Layer1_FindDirectory.Size = new System.Drawing.Size(22, 22);
            this.btn_Layer1_FindDirectory.TabIndex = 5;
            this.btn_Layer1_FindDirectory.text = "...";
            this.btn_Layer1_FindDirectory.TextColor = System.Drawing.Color.Black;
            this.btn_Layer1_FindDirectory.WaveColor = System.Drawing.Color.Black;
            this.btn_Layer1_FindDirectory.Click += new System.EventHandler(this.btn_Layer1_FindDirectory_Click);
            // 
            // layer2
            // 
            this.layer2.Controls.Add(this.pgb_Install);
            this.layer2.Controls.Add(this.pgb_Download);
            this.layer2.Controls.Add(this.lbl_Layer2_Installing);
            this.layer2.Controls.Add(this.lbl_Layer2_Downloading);
            this.layer2.Controls.Add(this.lbl_Layer2_Title);
            this.layer2.Location = new System.Drawing.Point(721, 29);
            this.layer2.Name = "layer2";
            this.layer2.Size = new System.Drawing.Size(320, 250);
            this.layer2.TabIndex = 7;
            // 
            // pgb_Install
            // 
            this.pgb_Install.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.pgb_Install.Location = new System.Drawing.Point(11, 144);
            this.pgb_Install.Maximum = 100;
            this.pgb_Install.Minimum = 0;
            this.pgb_Install.Name = "pgb_Install";
            this.pgb_Install.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(176)))), ((int)(((byte)(37)))));
            this.pgb_Install.Size = new System.Drawing.Size(301, 10);
            this.pgb_Install.TabIndex = 4;
            this.pgb_Install.Value = 0;
            // 
            // pgb_Download
            // 
            this.pgb_Download.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.pgb_Download.Location = new System.Drawing.Point(11, 98);
            this.pgb_Download.Maximum = 100;
            this.pgb_Download.Minimum = 0;
            this.pgb_Download.Name = "pgb_Download";
            this.pgb_Download.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(176)))), ((int)(((byte)(37)))));
            this.pgb_Download.Size = new System.Drawing.Size(301, 10);
            this.pgb_Download.TabIndex = 3;
            this.pgb_Download.Value = 0;
            // 
            // lbl_Layer2_Installing
            // 
            this.lbl_Layer2_Installing.AutoSize = true;
            this.lbl_Layer2_Installing.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer2_Installing.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer2_Installing.Location = new System.Drawing.Point(129, 121);
            this.lbl_Layer2_Installing.Name = "lbl_Layer2_Installing";
            this.lbl_Layer2_Installing.Size = new System.Drawing.Size(64, 19);
            this.lbl_Layer2_Installing.TabIndex = 2;
            this.lbl_Layer2_Installing.Text = "Installing";
            // 
            // lbl_Layer2_Downloading
            // 
            this.lbl_Layer2_Downloading.AutoSize = true;
            this.lbl_Layer2_Downloading.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer2_Downloading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer2_Downloading.Location = new System.Drawing.Point(116, 74);
            this.lbl_Layer2_Downloading.Name = "lbl_Layer2_Downloading";
            this.lbl_Layer2_Downloading.Size = new System.Drawing.Size(90, 19);
            this.lbl_Layer2_Downloading.TabIndex = 1;
            this.lbl_Layer2_Downloading.Text = "Downloading";
            // 
            // lbl_Layer2_Title
            // 
            this.lbl_Layer2_Title.AutoSize = true;
            this.lbl_Layer2_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer2_Title.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lbl_Layer2_Title.Location = new System.Drawing.Point(73, 18);
            this.lbl_Layer2_Title.Name = "lbl_Layer2_Title";
            this.lbl_Layer2_Title.Size = new System.Drawing.Size(176, 42);
            this.lbl_Layer2_Title.TabIndex = 0;
            this.lbl_Layer2_Title.Text = "Please wait while Crash \r\ndownload and install";
            this.lbl_Layer2_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Layer1_Directory
            // 
            this.txt_Layer1_Directory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.txt_Layer1_Directory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.txt_Layer1_Directory.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txt_Layer1_Directory.FontColor = System.Drawing.SystemColors.WindowText;
            this.txt_Layer1_Directory.Location = new System.Drawing.Point(24, 121);
            this.txt_Layer1_Directory.Name = "txt_Layer1_Directory";
            this.txt_Layer1_Directory.SelectedBottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(250)))));
            this.txt_Layer1_Directory.Size = new System.Drawing.Size(245, 22);
            this.txt_Layer1_Directory.TabIndex = 4;
            this.txt_Layer1_Directory.UseSystemPasswordChar = false;
            // 
            // lbl_Layer1_Args
            // 
            this.lbl_Layer1_Args.AutoSize = true;
            this.lbl_Layer1_Args.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer1_Args.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer1_Args.Location = new System.Drawing.Point(6, 167);
            this.lbl_Layer1_Args.Name = "lbl_Layer1_Args";
            this.lbl_Layer1_Args.Size = new System.Drawing.Size(309, 38);
            this.lbl_Layer1_Args.TabIndex = 3;
            this.lbl_Layer1_Args.Text = "By clicking the continue button, you agree to our\r\nTerms of Service located on th" +
    "e product website.";
            this.lbl_Layer1_Args.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Layer1_Canc
            // 
            this.btn_Layer1_Canc.BackColor = System.Drawing.Color.White;
            this.btn_Layer1_Canc.BackgroundColorClicked = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Layer1_Canc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btn_Layer1_Canc.BorderWidth = 0;
            this.btn_Layer1_Canc.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btn_Layer1_Canc.Location = new System.Drawing.Point(3, 208);
            this.btn_Layer1_Canc.MouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Layer1_Canc.Name = "btn_Layer1_Canc";
            this.btn_Layer1_Canc.Size = new System.Drawing.Size(86, 39);
            this.btn_Layer1_Canc.TabIndex = 2;
            this.btn_Layer1_Canc.text = "Close";
            this.btn_Layer1_Canc.TextColor = System.Drawing.Color.Black;
            this.btn_Layer1_Canc.WaveColor = System.Drawing.Color.Black;
            this.btn_Layer1_Canc.Click += new System.EventHandler(this.btn_Layer1_Canc_Click);
            // 
            // btn_Layer1_Next
            // 
            this.btn_Layer1_Next.BackColor = System.Drawing.Color.White;
            this.btn_Layer1_Next.BackgroundColorClicked = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Layer1_Next.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btn_Layer1_Next.BorderWidth = 0;
            this.btn_Layer1_Next.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btn_Layer1_Next.Location = new System.Drawing.Point(95, 208);
            this.btn_Layer1_Next.MouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Layer1_Next.Name = "btn_Layer1_Next";
            this.btn_Layer1_Next.Size = new System.Drawing.Size(221, 39);
            this.btn_Layer1_Next.TabIndex = 1;
            this.btn_Layer1_Next.text = "Continue installation of Crash";
            this.btn_Layer1_Next.TextColor = System.Drawing.Color.Black;
            this.btn_Layer1_Next.WaveColor = System.Drawing.Color.Black;
            this.btn_Layer1_Next.Click += new System.EventHandler(this.btn_Layer1_Next_Click);
            // 
            // lbl_Layer1_Title
            // 
            this.lbl_Layer1_Title.AutoSize = true;
            this.lbl_Layer1_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer1_Title.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lbl_Layer1_Title.Location = new System.Drawing.Point(37, 0);
            this.lbl_Layer1_Title.Name = "lbl_Layer1_Title";
            this.lbl_Layer1_Title.Size = new System.Drawing.Size(246, 25);
            this.lbl_Layer1_Title.TabIndex = 0;
            this.lbl_Layer1_Title.Text = "Crash - Application Installer";
            // 
            // Layer3
            // 
            this.Layer3.Controls.Add(this.lbl_Layer3_Title);
            this.Layer3.Controls.Add(this.lbl_Layer3_StartMenu);
            this.Layer3.Controls.Add(this.cek_Layer3_Desktop);
            this.Layer3.Controls.Add(this.cek_Layer3_StartMenu);
            this.Layer3.Controls.Add(this.lbl_Layer3_Desktop);
            this.Layer3.Controls.Add(this.btn_Layer3_FinishButton);
            this.Layer3.Location = new System.Drawing.Point(1062, 18);
            this.Layer3.Name = "Layer3";
            this.Layer3.Size = new System.Drawing.Size(320, 250);
            this.Layer3.TabIndex = 4;
            // 
            // lbl_Layer3_Title
            // 
            this.lbl_Layer3_Title.AutoSize = true;
            this.lbl_Layer3_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer3_Title.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lbl_Layer3_Title.Location = new System.Drawing.Point(83, 17);
            this.lbl_Layer3_Title.Name = "lbl_Layer3_Title";
            this.lbl_Layer3_Title.Size = new System.Drawing.Size(157, 25);
            this.lbl_Layer3_Title.TabIndex = 5;
            this.lbl_Layer3_Title.Text = "Installer complete!";
            // 
            // lbl_Layer3_StartMenu
            // 
            this.lbl_Layer3_StartMenu.AutoSize = true;
            this.lbl_Layer3_StartMenu.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer3_StartMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer3_StartMenu.Location = new System.Drawing.Point(75, 87);
            this.lbl_Layer3_StartMenu.Name = "lbl_Layer3_StartMenu";
            this.lbl_Layer3_StartMenu.Size = new System.Drawing.Size(175, 19);
            this.lbl_Layer3_StartMenu.TabIndex = 4;
            this.lbl_Layer3_StartMenu.Text = "Create start menu shortcut";
            // 
            // cek_Layer3_Desktop
            // 
            this.cek_Layer3_Desktop.BackColor = System.Drawing.Color.Transparent;
            this.cek_Layer3_Desktop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.cek_Layer3_Desktop.Checked = false;
            this.cek_Layer3_Desktop.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.cek_Layer3_Desktop.Location = new System.Drawing.Point(59, 118);
            this.cek_Layer3_Desktop.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.cek_Layer3_Desktop.Name = "cek_Layer3_Desktop";
            this.cek_Layer3_Desktop.OnMouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cek_Layer3_Desktop.Size = new System.Drawing.Size(16, 16);
            this.cek_Layer3_Desktop.TabIndex = 3;
            // 
            // cek_Layer3_StartMenu
            // 
            this.cek_Layer3_StartMenu.BackColor = System.Drawing.Color.Transparent;
            this.cek_Layer3_StartMenu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.cek_Layer3_StartMenu.Checked = false;
            this.cek_Layer3_StartMenu.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.cek_Layer3_StartMenu.Location = new System.Drawing.Point(59, 88);
            this.cek_Layer3_StartMenu.MiddleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.cek_Layer3_StartMenu.Name = "cek_Layer3_StartMenu";
            this.cek_Layer3_StartMenu.OnMouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cek_Layer3_StartMenu.Size = new System.Drawing.Size(16, 16);
            this.cek_Layer3_StartMenu.TabIndex = 2;
            // 
            // lbl_Layer3_Desktop
            // 
            this.lbl_Layer3_Desktop.AutoSize = true;
            this.lbl_Layer3_Desktop.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Layer3_Desktop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbl_Layer3_Desktop.Location = new System.Drawing.Point(75, 116);
            this.lbl_Layer3_Desktop.Name = "lbl_Layer3_Desktop";
            this.lbl_Layer3_Desktop.Size = new System.Drawing.Size(157, 19);
            this.lbl_Layer3_Desktop.TabIndex = 1;
            this.lbl_Layer3_Desktop.Text = "Create desktop shortcut";
            // 
            // btn_Layer3_FinishButton
            // 
            this.btn_Layer3_FinishButton.BackColor = System.Drawing.Color.White;
            this.btn_Layer3_FinishButton.BackgroundColorClicked = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Layer3_FinishButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btn_Layer3_FinishButton.BorderWidth = 0;
            this.btn_Layer3_FinishButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btn_Layer3_FinishButton.Location = new System.Drawing.Point(45, 189);
            this.btn_Layer3_FinishButton.MouseOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Layer3_FinishButton.Name = "btn_Layer3_FinishButton";
            this.btn_Layer3_FinishButton.Size = new System.Drawing.Size(232, 50);
            this.btn_Layer3_FinishButton.TabIndex = 0;
            this.btn_Layer3_FinishButton.text = "Finish";
            this.btn_Layer3_FinishButton.TextColor = System.Drawing.Color.Black;
            this.btn_Layer3_FinishButton.WaveColor = System.Drawing.Color.Black;
            this.btn_Layer3_FinishButton.Click += new System.EventHandler(this.btn_Layer3_FinishButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(219)))));
            this.BorderSize = 2;
            this.ClientSize = new System.Drawing.Size(322, 280);
            this.Controls.Add(this.layer2);
            this.Controls.Add(this.Layer3);
            this.Controls.Add(this.layer1);
            this.Controls.Add(this.lbl_Welcome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowCloseButton = false;
            this.Showicon = false;
            this.ShowMaxButton = false;
            this.ShowMinButton = false;
            this.ShowTitleLabel = false;
            this.Sizeable = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.layer1.ResumeLayout(false);
            this.layer1.PerformLayout();
            this.layer2.ResumeLayout(false);
            this.layer2.PerformLayout();
            this.Layer3.ResumeLayout(false);
            this.Layer3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Welcome;
        private System.Windows.Forms.Panel layer1;
        private IndieGoat.MaterialFramework.Controls.FlatButton btn_Layer1_Canc;
        private IndieGoat.MaterialFramework.Controls.FlatButton btn_Layer1_Next;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer1_Title;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer1_Args;
        private IndieGoat.MaterialFramework.Controls.FlatButton btn_Layer1_FindDirectory;
        private IndieGoat.MaterialFramework.Controls.MaterialTextBox txt_Layer1_Directory;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer1_InstallDist;
        private System.Windows.Forms.Panel layer2;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer2_Title;
        private IndieGoat.MaterialFramework.Controls.MaterialProgressBar pgb_Install;
        private IndieGoat.MaterialFramework.Controls.MaterialProgressBar pgb_Download;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer2_Installing;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer2_Downloading;
        private System.Windows.Forms.Panel Layer3;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer3_Title;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer3_StartMenu;
        private IndieGoat.MaterialFramework.Controls.MaterialCheckBox cek_Layer3_Desktop;
        private IndieGoat.MaterialFramework.Controls.MaterialCheckBox cek_Layer3_StartMenu;
        private IndieGoat.MaterialFramework.Controls.MaterialLabel lbl_Layer3_Desktop;
        private IndieGoat.MaterialFramework.Controls.FlatButton btn_Layer3_FinishButton;
    }
}