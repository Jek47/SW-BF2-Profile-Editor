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
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void About_Load_1(object sender, EventArgs e)
        {
            Version? verNo = Assembly.GetExecutingAssembly().GetName().Version;
            lblVerNo.Text = verNo != null ? $"v{verNo.Major}.{verNo.Minor}" : "Error";
        }

        private void lnklblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Jek47",
                UseShellExecute = true
            });
        }

        private async void btnCFU_Click(object sender, EventArgs e)
        {
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0";
            string latestVersion = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    latestVersion = await client.GetStringAsync("https://your-url/version.txt");
                    latestVersion = latestVersion.Trim();

                    if (latestVersion != currentVersion)
                    {
                        MessageBox.Show($"New version available: v{latestVersion}\nYou’re currently on v{currentVersion}.", "Update Available");
                    }
                    else
                    {
                        MessageBox.Show("You’re already using the latest version.", "Up to Date");
                    }
                }
                catch
                {
                    MessageBox.Show("Could not check for updates. Are you offline or did the Empire jam the signal?", "Error");
                }
            }

            string updateUrl = "https://yourdomain.com/SWBF2Editor_v1.3.exe";
            string tempPath = Path.Combine(Path.GetTempPath(), "SWBF2Editor_New.exe");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] data = await client.GetByteArrayAsync(updateUrl);
                    File.WriteAllBytes(tempPath, data);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = tempPath,
                        UseShellExecute = true
                    });

                    Application.Exit();
                }
                catch
                {
                    MessageBox.Show("Update failed. Could not download the new version.");
                }
            }
        }


    }
}
