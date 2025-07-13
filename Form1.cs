// Antoine if you read this... stop judging me
// I'm not a coder 😂
// Also, fuck you Yousef
using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SW_BF2_PS4_Profile_Editor
{
    public partial class Form1 : Form
    {
        SoundPlayer themePlayer = new SoundPlayer();
        private System.Windows.Forms.Timer? fileCheckTimer;
        private ProfilePlatform currentPlatform;
        string? filePath;
        bool isMuted = Properties.Settings.Default.MusicMuted;

        public Form1()
        {
            InitializeComponent();
        }
        private bool ApplyOffsets(string filePath)
        {
            string? platformName = DetectPlatform(filePath);
            if (platformName == "Unknown")
            {
                MessageBox.Show("Could not automatically determine the platform for this profile.\nPlease select the platform that the profile was created on.", "Platform undetected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                platformName = PromptUserToSelectPlatform();
                if (platformName == null)
                    return false;
            }
            else
            {
                var confirmResult = MessageBox.Show($"Platform detected: {platformName}.\nIs this correct?", "Confirm platform", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.No)
                {
                    platformName = PromptUserToSelectPlatform();
                    if (platformName == null)
                        return false;
                }
            }
            currentPlatform = platformName switch
            {
                "PS4 - Classic Collection (2024)" => ProfilePlatform.PS4,
                "PC - Original release (2005)" => ProfilePlatform.PC,
                _ => throw new InvalidOperationException("Invalid platform.")
            };
            toolStripStatusLabelPlatform.Text = platformName switch
            {
                "PS4 - Classic Collection (2024)" => "PS4 (2024)",
                "PC - Original release (2005)" => "PC (2005)",
                _ => "Platform: Unknown"
            };
            return true;
        }
        private void BlankTextBoxes()
        {
            txtProfileName.Text = "";
            txtPlayerPoints.Text = "";
            txtKills.Text = "";
            txtDeaths.Text = "";
            txtGunslinger.Text = "";
            txtTechnician.Text = "";
            txtDemolition.Text = "";
            txtMarksman.Text = "";
            txtRegulator.Text = "";
            txtEndurance.Text = "";
            txtGuardian.Text = "";
            txtWarHero.Text = "";
            txtFrenzy.Text = "";
            txtRank.Text = "";
            txtKillDeathRatio.Text = "";
            txtPointsPerLife.Text = "";
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (var AboutForm = new About())
            {
                AboutForm.StartPosition = FormStartPosition.CenterParent;
                AboutForm.ShowDialog(this);
            }
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Jek47",
                UseShellExecute = true
            });
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!string.Equals(toolStripStatusLabelStatus.Text, "Idle", StringComparison.OrdinalIgnoreCase))
             {
                DialogResult response = PromptToSaveBeforeOpening();
                switch (response)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        try
                        {
                            btnSave.PerformClick();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Save failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Profile Files (*.profile)|*.profile";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                if (!ApplyOffsets(filePath))
                {
                    MessageBox.Show("Unable to determine the profile's platform. The profile will not be opened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ReadStatsFromFile(filePath);
                StartFileMonitor();
                TextboxStatus(true);
                btnSave.Enabled = true;
                toolStripStatusLabelFile.Text = $" │   {(filePath)}   │ ";
                toolStripStatusLabelStatus.Text = "Editing";
                txtProfileName.Text = Path.GetFileNameWithoutExtension(filePath);
            }
        }
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (isMuted)
            {
                btnMute.BackgroundImage = Properties.Resources.Mute;
                themePlayer.PlayLooping();
                Properties.Settings.Default.MusicMuted = false;
            }
            else
            {
                btnMute.BackgroundImage = Properties.Resources.MuteOn;
                themePlayer.Stop();
                Properties.Settings.Default.MusicMuted = true;
            }
            btnMute.BackgroundImageLayout = ImageLayout.Stretch;
            isMuted = Properties.Settings.Default.MusicMuted;
            Properties.Settings.Default.Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                fileCheckTimer?.Stop();
                fileCheckTimer?.Dispose();
                fileCheckTimer = null;
                toolStripStatusLabelFile.Text = "";
                toolStripStatusLabelStatus.Text = "Idle";
                toolStripStatusLabelPlatform.Text = "";
                var result = MessageBox.Show("The profile is inaccessible. Would you like to open it again?", "Profile missing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnLoad.PerformClick();
                }
                else
                {
                    btnSave.Enabled = false;
                    TextboxStatus(false);
                    BlankTextBoxes();
                }
                return;
            }
            bool IsValidShortRangeUShort(string input, out ushort value)
            {
                if (ushort.TryParse(input, out value))
                {
                    return value <= short.MaxValue;
                }
                return false;
            }
            bool IsValidUInt32(string input, out uint value)
            {
                if (uint.TryParse(input, out value))
                {
                    return value <= int.MaxValue;
                }
                return false;

            }
            if (string.IsNullOrEmpty(filePath)) return;

            if (!IsValidUInt32(txtPlayerPoints.Text, out uint PlayerPoints) ||
                !IsValidUInt32(txtKills.Text, out uint Kills) ||
                !IsValidUInt32(txtDeaths.Text, out uint Deaths)
                )
            {
                MessageBox.Show("Please enter a valid number between 0 and 2,147,483,647 for all player stats.", "Invalid statistics", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidShortRangeUShort(txtGunslinger.Text, out ushort Gunslinger) ||
                !IsValidShortRangeUShort(txtFrenzy.Text, out ushort Frenzy) ||
                !IsValidShortRangeUShort(txtDemolition.Text, out ushort Demolition) ||
                !IsValidShortRangeUShort(txtTechnician.Text, out ushort Technician) ||
                !IsValidShortRangeUShort(txtMarksman.Text, out ushort Marksman) ||
                !IsValidShortRangeUShort(txtRegulator.Text, out ushort Regulator) ||
                !IsValidShortRangeUShort(txtEndurance.Text, out ushort Endurance) ||
                !IsValidShortRangeUShort(txtGuardian.Text, out ushort Guardian) ||
                !IsValidShortRangeUShort(txtWarHero.Text, out ushort WarHero)
                )
            {
                MessageBox.Show("Please enter a valid number between 0 and 32,767 for all medals.", "Invalid medals", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            createBackup(filePath);
            WriteStatsToFile(filePath);
            ShowTemporaryStatus("Saved successfully");
            System.Media.SystemSounds.Asterisk.Play();
            MessageBox.Show("Your profile has been saved successfully!", "Save complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void createBackup(string filePath)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss");
                string backupPath = Path.Combine(
                    Path.GetDirectoryName(filePath)!,
                    Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath) + ".backup " + timestamp
                );
                File.Copy(filePath, backupPath, overwrite: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create backup:\n{ex.Message}", "Backup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string DetectPlatform(string filePath)
        {
            long fileLength = new FileInfo(filePath).Length;

            string? platformBySize = fileLength switch
            {
                5352 => "PS4 - Classic Collection (2024)",
                4356 => "PC - Original release (2005)",
                _ => "Unknown"
            };

            return platformBySize;

            // if file length isn't enough to accurately detect the platform, look for headers //
            // needs more testing really, but file length has been consistent in all cases i've seen so far //

            /*
            uint header;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                header = br.ReadUInt32();
            }

            return header switch
            {
                0x3D69A3C4 => "PS4 - Classic Collection (2024)",
                0xCE7E8262 => "PC - Original release (2005)",
                _ => "Unknown"
            };
            */
        }
        private void Form1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string droppedFile = files[0];

                    if (Path.GetExtension(droppedFile).Equals(".profile", StringComparison.OrdinalIgnoreCase))
                    {
                        LoadProfileFromPath(droppedFile);
                    }
                    else
                    {
                        MessageBox.Show("Only .profile files are supported.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!string.Equals(toolStripStatusLabelStatus.Text, "Idle", StringComparison.OrdinalIgnoreCase))
            {
                var result = MessageBox.Show("Do you want to save your profile before exiting?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        try
                        {
                            btnSave.PerformClick();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to save before exit:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            themePlayer.Stream = Properties.Resources.Duel;
            if (isMuted)
                btnMute.BackgroundImage = Properties.Resources.MuteOn;
            else
                themePlayer.PlayLooping();
            btnMute.BackgroundImageLayout = ImageLayout.Stretch;
            TextboxStatus(false);
            btnSave.Enabled = false;
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
            this.FormClosing += Form1_FormClosing;
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            byte[] fontData = Properties.Resources.StarjediSpecialEdition_9Bqy;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCollection.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);
            Font starWarsFont = new Font(fontCollection.Families[0], 14F, FontStyle.Regular);
            lblStats.Font = starWarsFont;
            lblMedals.Font = starWarsFont;
            Version? appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            toolStripStatusLabelAppVer.Text = (appVersion != null)
                ? $"v{appVersion.Major}.{appVersion.Minor}"
                : "";
        }
        private void lblHelloThere_Click(object sender, EventArgs e)
        {
            if (!isMuted)
            {
                btnMute.BackgroundImage = Properties.Resources.MuteOn;
                themePlayer.Stop();
                Properties.Settings.Default.MusicMuted = true;
                btnMute.BackgroundImageLayout = ImageLayout.Stretch;
                isMuted = Properties.Settings.Default.MusicMuted;
            }
            SoundPlayer helloThere = new SoundPlayer();
            helloThere.Stream = Properties.Resources.Hello;
            helloThere.Play();
        }
        private void lblStormtrooperEasterEgg_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://youtu.be/JaG9QMmEY8c",
                UseShellExecute = true
            });
        }
        private void LoadProfileFromPath(string FilePath)
        {
            if (!string.Equals(toolStripStatusLabelStatus.Text, "Idle", StringComparison.OrdinalIgnoreCase))
            {
                DialogResult response = PromptToSaveBeforeOpening();
                switch (response)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        try
                        {
                            btnSave.PerformClick();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Save failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            if (!ApplyOffsets(FilePath))
            {
                MessageBox.Show("Unable to determine the profile's platform. The profile will not be opened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReadStatsFromFile(FilePath);
            StartFileMonitor();
            TextboxStatus(true);
            btnSave.Enabled = true;
            toolStripStatusLabelFile.Text = $" │   {(FilePath)}   │ ";
            toolStripStatusLabelStatus.Text = "Editing";
            txtProfileName.Text = Path.GetFileNameWithoutExtension(FilePath);
        }
        private void OnlyAllowDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private DialogResult PromptToSaveBeforeOpening()
        {
            return MessageBox.Show("A profile is already being edited. \nDo you want to save your changes before opening the new profile?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        }
        private string? PromptUserToSelectPlatform()
        {
            using (var form = new PlatformSelection())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                    return form.SelectedPlatform;
                else
                    return null;
            }
        }
        private void ReadStatsFromFile(string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            var offsets = ProfileOffsets.Map[currentPlatform];

            fs.Seek(offsets.PlayerPointsOffset, SeekOrigin.Begin);
            txtPlayerPoints.Text = EndianUtils.ReadUInt32LE(br).ToString();
            fs.Seek(offsets.KillsOffset, SeekOrigin.Begin);
            txtKills.Text = EndianUtils.ReadUInt32LE(br).ToString();
            fs.Seek(offsets.DeathsOffset, SeekOrigin.Begin);
            txtDeaths.Text = EndianUtils.ReadUInt32LE(br).ToString();
            fs.Seek(offsets.GunslingerOffset, SeekOrigin.Begin);
            txtGunslinger.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.FrenzyOffset, SeekOrigin.Begin);
            txtFrenzy.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.DemolitionOffset, SeekOrigin.Begin);
            txtDemolition.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.TechnicianOffset, SeekOrigin.Begin);
            txtTechnician.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.MarksmanOffset, SeekOrigin.Begin);
            txtMarksman.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.RegulatorOffset, SeekOrigin.Begin);
            txtRegulator.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.EnduranceOffset, SeekOrigin.Begin);
            txtEndurance.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.GuardianOffset, SeekOrigin.Begin);
            txtGuardian.Text = EndianUtils.ReadUInt16LE(br).ToString();
            fs.Seek(offsets.WarHeroOffset, SeekOrigin.Begin);
            txtWarHero.Text = EndianUtils.ReadUInt16LE(br).ToString();
        }
        private void ShowTemporaryStatus(string message, int durationMs = 3000)
        {
            toolStripStatusLabelStatus.Text = message;
            var timer = new System.Windows.Forms.Timer { Interval = durationMs };
            timer.Tick += (s, e) =>
            {
                if (!string.Equals(toolStripStatusLabelStatus.Text, "Idle", StringComparison.OrdinalIgnoreCase))
                {
                    toolStripStatusLabelStatus.Text = "Editing";
                }
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        private void StartFileMonitor()
        {
            fileCheckTimer = new System.Windows.Forms.Timer();
            fileCheckTimer.Interval = 1000;
            fileCheckTimer.Tick += (s, e) =>
            {
                if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
                {
                    fileCheckTimer.Stop();
                    fileCheckTimer.Dispose();
                    fileCheckTimer = null;
                    toolStripStatusLabelFile.Text = "";
                    toolStripStatusLabelStatus.Text = "Idle";
                    toolStripStatusLabelPlatform.Text = "";
                    var result = MessageBox.Show("The profile is inaccessible. Would you like to open it again?", "Profile missing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        btnLoad.PerformClick();
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        TextboxStatus(false);
                        BlankTextBoxes();
                    }

                }
            };
            fileCheckTimer.Start();
        }
        private void TextboxStatus(bool enabled)
        {
            txtProfileName.Enabled = false;
            txtPlayerPoints.Enabled = enabled;
            txtKills.Enabled = enabled;
            txtDeaths.Enabled = enabled;
            txtRank.Enabled = enabled;
            txtKillDeathRatio.Enabled = enabled;
            txtPointsPerLife.Enabled = enabled;
            txtGunslinger.Enabled = enabled;
            txtFrenzy.Enabled = enabled;
            txtDemolition.Enabled = enabled;
            txtTechnician.Enabled = enabled;
            txtMarksman.Enabled = enabled;
            txtRegulator.Enabled = enabled;
            txtEndurance.Enabled = enabled;
            txtGuardian.Enabled = enabled;
            txtWarHero.Enabled = enabled;
        }
        private void UpdateKDRatio()
        {
            if (int.TryParse(txtKills.Text, out int kills) &&
                int.TryParse(txtDeaths.Text, out int deaths))
            {
                if (deaths == 0)
                {
                    txtKillDeathRatio.Text = "--";
                }
                else
                {
                    float ratio = (float)kills / deaths;
                    txtKillDeathRatio.Text = ratio.ToString("0.00");
                }
            }
            else
            {
                txtKillDeathRatio.Text = "--";
            }
        }
        private void UpdatePPL()
        {
            if (int.TryParse(txtPlayerPoints.Text, out int PlayerPoints) &&
                int.TryParse(txtDeaths.Text, out int deaths))
            {
                float ratio = (float)PlayerPoints / (deaths == 0 ? 1 : deaths);
                txtPointsPerLife.Text = ratio.ToString("0.00");
            }
            else
            {
                txtPointsPerLife.Text = "—-";
            }
        }
        private void UpdateRank()
        {
            if (int.TryParse(txtGunslinger.Text, out int Gunslinger) &&
                int.TryParse(txtFrenzy.Text, out int Frenzy) &&
                int.TryParse(txtDemolition.Text, out int Demolition) &&
                int.TryParse(txtTechnician.Text, out int Technician) &&
                int.TryParse(txtMarksman.Text, out int Marksman) &&
                int.TryParse(txtRegulator.Text, out int Regulator) &&
                int.TryParse(txtEndurance.Text, out int Endurance) &&
                int.TryParse(txtGuardian.Text, out int Guardian) &&
                int.TryParse(txtWarHero.Text, out int WarHero)
                )
            {
                int total = Gunslinger + Frenzy + Demolition + Technician + Marksman + Regulator + Endurance + Guardian + WarHero;
                if (total < 20)
                    txtRank.Text = "Private";
                else if (total < 100)
                    txtRank.Text = "Sergeant";
                else if (total < 300)
                    txtRank.Text = "Captain";
                else
                    txtRank.Text = "General";
            }
            else
            {
                txtRank.Text = "--";
            }
        }
        private void WriteStatsToFile(string FilePath)
        {
            try
            {
                var offsets = ProfileOffsets.Map[currentPlatform];
                using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Write);
                using var bw = new BinaryWriter(fs);
                fs.Seek(offsets.PlayerPointsOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt32LE(bw, uint.Parse(txtPlayerPoints.Text));
                fs.Seek(offsets.KillsOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt32LE(bw, uint.Parse(txtKills.Text));
                fs.Seek(offsets.DeathsOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt32LE(bw, uint.Parse(txtDeaths.Text));
                fs.Seek(offsets.GunslingerOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtGunslinger.Text));
                fs.Seek(offsets.FrenzyOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtFrenzy.Text));
                fs.Seek(offsets.DemolitionOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtDemolition.Text));
                fs.Seek(offsets.TechnicianOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtTechnician.Text));
                fs.Seek(offsets.MarksmanOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtMarksman.Text));
                fs.Seek(offsets.RegulatorOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtRegulator.Text));
                fs.Seek(offsets.EnduranceOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtEndurance.Text));
                fs.Seek(offsets.GuardianOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtGuardian.Text));
                fs.Seek(offsets.WarHeroOffset, SeekOrigin.Begin);
                EndianUtils.WriteUInt16LE(bw, ushort.Parse(txtWarHero.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while writing data to file:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public enum ProfilePlatform
    {
        PS4, PC
    }
    public class ProfileOffsets
    {
        public int PlayerPointsOffset;
        public int KillsOffset;
        public int DeathsOffset;
        public int GunslingerOffset;
        public int DemolitionOffset;
        public int RegulatorOffset;
        public int GuardianOffset;
        public int FrenzyOffset;
        public int TechnicianOffset;
        public int MarksmanOffset;
        public int EnduranceOffset;
        public int WarHeroOffset;

        public static Dictionary<ProfilePlatform, ProfileOffsets> Map = new()
        {
            [ProfilePlatform.PS4] = new ProfileOffsets
            {
                PlayerPointsOffset = 0x0BF0,
                KillsOffset = 0x0BF4,
                DeathsOffset = 0x0BF8,
                GunslingerOffset = 0x07E4,
                FrenzyOffset = 0x07E6,
                DemolitionOffset = 0x07E8,
                TechnicianOffset = 0x07EA,
                MarksmanOffset = 0x07EC,
                RegulatorOffset = 0x07EE,
                EnduranceOffset = 0x07F0,
                GuardianOffset = 0x07F2,
                WarHeroOffset = 0x07F4

            },
            [ProfilePlatform.PC] = new ProfileOffsets
            {
                PlayerPointsOffset = 0x0F8C,
                KillsOffset = 0x0F90,
                DeathsOffset = 0x0F94,
                GunslingerOffset = 0x057C,
                FrenzyOffset = 0x057E,
                DemolitionOffset = 0x0580,
                TechnicianOffset = 0x0582,
                MarksmanOffset = 0x0584,
                RegulatorOffset = 0x0586,
                EnduranceOffset = 0x0588,
                GuardianOffset = 0x058A,
                WarHeroOffset = 0x058C
            }
        };
    }
}