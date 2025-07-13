namespace SW_BF2_PS4_Profile_Editor
{
    partial class About
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            lblVerNum = new Label();
            lblVerNo = new Label();
            lblAuthor = new Label();
            lblJek47 = new Label();
            lblWebsite = new Label();
            toolTip1 = new ToolTip(components);
            lblDescription = new Label();
            btnCFU = new Button();
            lnklblGithub = new LinkLabel();
            label1 = new Label();
            lblDesc = new Label();
            SuspendLayout();
            // 
            // lblVerNum
            // 
            lblVerNum.AutoSize = true;
            lblVerNum.Font = new Font("StarJedi Special Edition", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblVerNum.ForeColor = Color.White;
            lblVerNum.Location = new Point(15, 40);
            lblVerNum.Name = "lblVerNum";
            lblVerNum.Size = new Size(131, 20);
            lblVerNum.TabIndex = 1;
            lblVerNum.Text = "version numbeR";
            // 
            // lblVerNo
            // 
            lblVerNo.AutoSize = true;
            lblVerNo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblVerNo.ForeColor = Color.White;
            lblVerNo.Location = new Point(248, 40);
            lblVerNo.Name = "lblVerNo";
            lblVerNo.Size = new Size(27, 17);
            lblVerNo.TabIndex = 2;
            lblVerNo.Text = "ver";
            lblVerNo.TextAlign = ContentAlignment.TopRight;
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("StarJedi Special Edition", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblAuthor.ForeColor = Color.White;
            lblAuthor.Location = new Point(15, 60);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(67, 20);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "AuthoR";
            // 
            // lblJek47
            // 
            lblJek47.AutoSize = true;
            lblJek47.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblJek47.ForeColor = Color.White;
            lblJek47.Location = new Point(239, 62);
            lblJek47.Name = "lblJek47";
            lblJek47.Size = new Size(42, 17);
            lblJek47.TabIndex = 4;
            lblJek47.Text = "Jek47";
            lblJek47.TextAlign = ContentAlignment.TopRight;
            toolTip1.SetToolTip(lblJek47, "(Copilot did most of the work to be fair)");
            // 
            // lblWebsite
            // 
            lblWebsite.AutoSize = true;
            lblWebsite.Font = new Font("StarJedi Special Edition", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblWebsite.ForeColor = Color.White;
            lblWebsite.Location = new Point(15, 80);
            lblWebsite.Name = "lblWebsite";
            lblWebsite.Size = new Size(58, 20);
            lblWebsite.TabIndex = 5;
            lblWebsite.Text = "GithuB";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblDescription.ForeColor = Color.White;
            lblDescription.Location = new Point(3, 133);
            lblDescription.MaximumSize = new Size(277, 0);
            lblDescription.MinimumSize = new Size(300, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(300, 136);
            lblDescription.TabIndex = 10;
            lblDescription.Text = resources.GetString("lblDescription.Text");
            lblDescription.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblDescription, "(Copilot did most of the work to be fair)");
            // 
            // btnCFU
            // 
            btnCFU.BackColor = Color.Transparent;
            btnCFU.BackgroundImage = Properties.Resources.Update;
            btnCFU.BackgroundImageLayout = ImageLayout.Stretch;
            btnCFU.FlatAppearance.BorderSize = 0;
            btnCFU.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnCFU.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCFU.FlatStyle = FlatStyle.Flat;
            btnCFU.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnCFU.ForeColor = Color.White;
            btnCFU.Location = new Point(280, 40);
            btnCFU.Name = "btnCFU";
            btnCFU.Size = new Size(16, 16);
            btnCFU.TabIndex = 41;
            toolTip1.SetToolTip(btnCFU, "Check for update");
            btnCFU.UseVisualStyleBackColor = false;
            btnCFU.Click += btnCFU_Click;
            // 
            // lnklblGithub
            // 
            lnklblGithub.AutoSize = true;
            lnklblGithub.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lnklblGithub.Location = new Point(163, 82);
            lnklblGithub.Name = "lnklblGithub";
            lnklblGithub.Size = new Size(119, 17);
            lnklblGithub.TabIndex = 7;
            lnklblGithub.TabStop = true;
            lnklblGithub.Text = "github.com/Jek47";
            lnklblGithub.TextAlign = ContentAlignment.TopRight;
            lnklblGithub.LinkClicked += lnklblGithub_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("StarJedi Special Edition", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Yellow;
            label1.Location = new Point(18, 9);
            label1.Name = "label1";
            label1.Size = new Size(269, 31);
            label1.TabIndex = 8;
            label1.Text = "Sw bf2 profile editoR";
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Font = new Font("StarJedi Special Edition", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblDesc.ForeColor = Color.White;
            lblDesc.Location = new Point(9, 113);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(107, 23);
            lblDesc.TabIndex = 9;
            lblDesc.Text = "DescriptioN";
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(306, 279);
            Controls.Add(btnCFU);
            Controls.Add(lblDescription);
            Controls.Add(lblDesc);
            Controls.Add(lnklblGithub);
            Controls.Add(lblWebsite);
            Controls.Add(lblJek47);
            Controls.Add(lblAuthor);
            Controls.Add(lblVerNo);
            Controls.Add(lblVerNum);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(322, 318);
            MinimumSize = new Size(322, 318);
            Name = "About";
            Text = "About";
            Load += About_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblVerNum;
        private Label lblVerNo;
        private Label lblAuthor;
        private Label lblJek47;
        private Label lblWebsite;
        private ToolTip toolTip1;
        private LinkLabel lnklblGithub;
        private Label label1;
        private Label lblDesc;
        private Label lblDescription;
        private Button btnCFU;
    }
}