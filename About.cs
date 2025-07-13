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

        private void btnCFU_Click(object sender, EventArgs e)
        {

        }
    }
}
