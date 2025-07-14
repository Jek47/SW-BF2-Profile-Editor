using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW_BF2_PS4_Profile_Editor
{
    public partial class PlatformSelection : Form
    {
        public PlatformSelection() { InitializeComponent(); }
        public string? SelectedPlatform { get; private set; }
        private void PlatformSelection_Load(object sender, EventArgs e) 
        {
            label1.Font = FontManager.StarWarsMainFont;
            label1.UseCompatibleTextRendering = true;
        }
        private void returnToMainWindow()
            {
            MessageBox.Show("You have chosen " + SelectedPlatform, "Platform chosen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
            }
        private void YodaError(string gamePlat)
            { MessageBox.Show("Support " + gamePlat + ", I do not.\nCheck back later you must.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void lblRed_Click(object sender, EventArgs e)
            { YodaError("the Nintendo Switch"); }
        private void lblOrange_Click(object sender, EventArgs e)
            {
            SelectedPlatform = "PC - Original release (2005)";
            returnToMainWindow();
            }
        private void lblYellow_Click(object sender, EventArgs e)
            { YodaError("PC - Classic Collection (2024)"); }
        private void lblGreen_Click(object sender, EventArgs e)
            { YodaError("the Xbox"); }
        private void lblLightGreen_Click(object sender, EventArgs e)
            { YodaError("the Xbox One / Series S / Series X"); }
        private void lblLightBlue_Click(object sender, EventArgs e)
            { YodaError("the PS2"); }
        private void lblBlue_Click(object sender, EventArgs e)
            { 
            SelectedPlatform = "PS4 - Classic Collection (2024)";
            returnToMainWindow();
            }
        private void lblPurple_Click(object sender, EventArgs e)
            { YodaError("the PS5"); }
    }
}
