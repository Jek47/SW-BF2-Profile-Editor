namespace SW_BF2_PS4_Profile_Editor
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            lblSettings = new Label();
            lblAutoUpdate = new Label();
            lblThemeSong = new Label();
            chkbxAutomaticUpdates = new CheckBox();
            chkbxThemeSong = new CheckBox();
            SuspendLayout();
            // 
            // lblSettings
            // 
            lblSettings.AutoSize = true;
            lblSettings.Font = new Font("StarJedi Special Edition", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblSettings.ForeColor = Color.Yellow;
            lblSettings.Location = new Point(24, 9);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(170, 31);
            lblSettings.TabIndex = 9;
            lblSettings.Text = "S e t t i n g S";
            // 
            // lblAutoUpdate
            // 
            lblAutoUpdate.AutoSize = true;
            lblAutoUpdate.Font = new Font("StarJedi Special Edition", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblAutoUpdate.ForeColor = Color.White;
            lblAutoUpdate.Location = new Point(15, 40);
            lblAutoUpdate.Name = "lblAutoUpdate";
            lblAutoUpdate.Size = new Size(160, 20);
            lblAutoUpdate.TabIndex = 10;
            lblAutoUpdate.Text = "Automatic updateS";
            // 
            // lblThemeSong
            // 
            lblThemeSong.AutoSize = true;
            lblThemeSong.Font = new Font("StarJedi Special Edition", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblThemeSong.ForeColor = Color.White;
            lblThemeSong.Location = new Point(15, 60);
            lblThemeSong.Name = "lblThemeSong";
            lblThemeSong.Size = new Size(95, 20);
            lblThemeSong.TabIndex = 11;
            lblThemeSong.Text = "Theme sonG";
            // 
            // chkbxAutomaticUpdates
            // 
            chkbxAutomaticUpdates.AutoSize = true;
            chkbxAutomaticUpdates.Location = new Point(188, 45);
            chkbxAutomaticUpdates.Name = "chkbxAutomaticUpdates";
            chkbxAutomaticUpdates.Size = new Size(15, 14);
            chkbxAutomaticUpdates.TabIndex = 12;
            chkbxAutomaticUpdates.UseVisualStyleBackColor = true;
            chkbxAutomaticUpdates.CheckedChanged += chkbxAutomaticUpdates_CheckedChanged;
            // 
            // chkbxThemeSong
            // 
            chkbxThemeSong.AutoSize = true;
            chkbxThemeSong.Location = new Point(188, 65);
            chkbxThemeSong.Name = "chkbxThemeSong";
            chkbxThemeSong.Size = new Size(15, 14);
            chkbxThemeSong.TabIndex = 13;
            chkbxThemeSong.UseVisualStyleBackColor = true;
            chkbxThemeSong.CheckedChanged += chkbxThemeSong_CheckedChanged;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(222, 94);
            Controls.Add(chkbxThemeSong);
            Controls.Add(chkbxAutomaticUpdates);
            Controls.Add(lblThemeSong);
            Controls.Add(lblAutoUpdate);
            Controls.Add(lblSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(238, 133);
            MinimumSize = new Size(238, 133);
            Name = "Settings";
            Text = "Settings";
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSettings;
        private Label lblAutoUpdate;
        private Label lblThemeSong;
        private CheckBox chkbxAutomaticUpdates;
        private CheckBox chkbxThemeSong;
    }
}