using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_BF2_PS4_Profile_Editor
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        bool SkipUpdatePrompts = Properties.Settings.Default.SkipUpdatePrompts;
        bool MusicMuted = Properties.Settings.Default.MusicMuted;
        private void Settings_Load(object sender, EventArgs e)
        {
            lblSettings.Font = FontManager.StarWarsSecondaryFont;
            lblAutoUpdate.Font = FontManager.StarWarsTinyFont;
            lblThemeSong.Font = FontManager.StarWarsTinyFont;
            lblSettings.UseCompatibleTextRendering = true;
            lblAutoUpdate.UseCompatibleTextRendering = true;
            lblThemeSong.UseCompatibleTextRendering = true;
            if (!MusicMuted)
                chkbxThemeSong.Checked = true;
            if(!SkipUpdatePrompts)
                chkbxAutomaticUpdates.Checked = true;
        }
        private void chkbxAutomaticUpdates_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbxAutomaticUpdates.Checked)
                Properties.Settings.Default.SkipUpdatePrompts = false;
            else
                Properties.Settings.Default.SkipUpdatePrompts = true;
            Properties.Settings.Default.Save();
        }
        private void chkbxThemeSong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxThemeSong.Checked)
                Properties.Settings.Default.MusicMuted = false;
            else
                Properties.Settings.Default.MusicMuted = true;
            Properties.Settings.Default.Save();
        }
    }
}
