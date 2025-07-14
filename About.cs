using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_BF2_PS4_Profile_Editor
{
    public partial class About : Form
    {
        public bool CFUfromMain { get; set; }
        public About()
        {
            InitializeComponent();
        }
        public void TriggerUpdateCheck()
        {
            if(this.CFUfromMain)
            {
                this.CFUfromMain = false;
                btnCFU.PerformClick();
            }
        }
        private void About_Load(object sender, EventArgs e){}
        private void About_Load_1(object sender, EventArgs e)
        {
            label1.Font = FontManager.StarWarsSecondaryFont;
            lblVerNum.Font = FontManager.StarWarsTinyFont;
            lblAuthor.Font = FontManager.StarWarsTinyFont;
            lblWebsite.Font = FontManager.StarWarsTinyFont;
            lblDesc.Font = FontManager.StarWarsMediumFont;
            label1.UseCompatibleTextRendering = true;
            lblVerNum.UseCompatibleTextRendering = true;
            lblAuthor.UseCompatibleTextRendering = true;
            lblWebsite.UseCompatibleTextRendering = true;
            lblDesc.UseCompatibleTextRendering = true;
            Version? verNo = Assembly.GetExecutingAssembly().GetName().Version;
            lblVerNo.Text = verNo != null ? $"v{verNo}" : "Error";
        }
        private void lnklblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Jek47",
                UseShellExecute = true
            });
        }
        private void About_Shown(object sender, EventArgs e)
        {
            btnCFU.PerformClick();
        }
        private async void btnCFU_Click(object sender, EventArgs e)
        {
            CFUfromMain = false;
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0";
            MessageBox.Show("Current version: " + currentVersion);
            string latestVersion = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    latestVersion = await client.GetStringAsync("https://github.com/Jek47/SW-BF2-Profile-Editor/blob/main/Properties/ver");
                    latestVersion = latestVersion.Trim();
                    if (latestVersion != currentVersion)
                    {
                        DialogResult updatePrompt = MessageBox.Show($"New version available: v{latestVersion}\nYou’re currently on v{currentVersion}.\n\nUpdate now?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (updatePrompt == DialogResult.Yes)
                        {
                            string updateUrl = await client.GetStringAsync("https://github.com/Jek47/SW-BF2-Profile-Editor/blob/main/Properties/rel");
                            updateUrl = updateUrl.Trim();
                            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                            string downloadPath = Path.Combine(downloadsFolder, "SWBF2Editor_v", latestVersion, ".exe");
                            {
                                try
                                {
                                    byte[] data = await client.GetByteArrayAsync(updateUrl);
                                    File.WriteAllBytes(downloadPath, data);

                                    Process.Start(new ProcessStartInfo
                                    {
                                        FileName = downloadPath,
                                        UseShellExecute = true
                                    });

                                    Application.Exit();
                                }
                                catch (Exception dlerror)
                                {
                                    MessageBox.Show($"Update failed. Could not download the new version.\nError: {dlerror.Message}", "Update failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                            return;
                    }
                    else
                        MessageBox.Show("You’re already using the latest version.", "Up to date",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to check for updates.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
