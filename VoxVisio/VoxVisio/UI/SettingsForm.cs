using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VoxVisio.Resources;
using VoxVisio.Singletons;
using VoxVisio.Commands;
using VoxVisio.Properties;

namespace VoxVisio.UI
{
    public partial class SettingsForm : Form
    {
        private List<Keys> voiceCommandKeys = new List<Keys>();
        private Keys? bindingKey = null;
        private SettingsSingleton settings;
        private int commandFocusCounter;

        public SettingsForm()
        {
            InitializeComponent();
            settings = SettingsSingleton.Instance();
            //Populate the command lists with the loaded commands
            updateTables(null, eListEvent.ItemAdded);
            //A counter that makes sure when the user clicks on the text box to add key presses, the initial mouse click is not added.
            commandFocusCounter = 0;
            //Bind the update tables method to the change event of the command list
            settings.Commands.OnChange += updateTables;
            //Load all of the settings into the controls so they display the correct information
            setUpSettingsControls();
            setDebugEyeState();

        }

        //Configure all the settings controls to show the correct values from the settings file
        private void setUpSettingsControls()
        {
            chkbxZoomEnabled.Checked = settings.ZoomEnabled;
            trkbrMagnificationAmount.Value = (int) settings.ZoomMagnification;
            udFormWidth.Value = settings.ZoomFormSize.Width;
            udFormHeight.Value = settings.ZoomFormSize.Height;
            chkbxDebugEyeTracking.Checked = settings.DebugEyeMouseMode;
            chkbxOptikeyEnabled.Checked = Settings.Default.OptiKeyEnabled;
            txtbxDragonFile.Text = Settings.Default.DragonFileAddress;
            txtbxOptikeyAddress.Text = Settings.Default.OptiKeyFileAddress;
           
            if (Settings.Default.DragonEnabled)
            {
                rbDragon.Checked = true;
            }
            else
            {
                rbWindowsVoice.Checked = true;
            }

        }

        private void updateTables(object sender, eListEvent changeType)
        {
            FillVoiceCommandTable();
            FillKeyBindingTable();
            FillStartProgramTable();
        }

        private void btnAddVoiceCommand_Click(object sender, EventArgs e)
        {
            // Checking that the user has filled out the form correctly
            if (txtVoiceCommandWord.Text == "" || txtVoiceCommandKeys.Text == "")
            {
                MessageBox.Show("Please ensure you have added a voice keyword and selected the keys you want to fire.",
                    "Error", MessageBoxButtons.OK);
            }
            else
            {
                string keystrings = "";
                voiceCommandKeys.ForEach(x => keystrings += (x).ToString() + " ");
                keystrings = keystrings.TrimEnd();
                keystrings = keystrings.Replace(" ", ",");
                Command command = new VoiceCommand(txtVoiceCommandWord.Text.ToLower(), keystrings,
                    SharedObjectsSingleton.Instance().inputSimulator);
                settings.Commands.Add(command);
                // Update the list of commands
                FillVoiceCommandTable();
                // Clear the text fields for possible new input
                txtVoiceCommandWord.Text = "";
                txtVoiceCommandKeys.Text = "";
                //Empty the keylist for new possible input
                voiceCommandKeys = new List<Keys>();
            }


        }

        private void txtVoiceCommandKeys_KeyUp(object sender, KeyEventArgs e)
        {
            addVoiceCommandKey(e.KeyCode);
        }

        private void txtVoiceCommandKeys_MouseDown(object sender, MouseEventArgs e)
        {

            if (commandFocusCounter > 0)
            {
                MouseButtons mb = e.Button;

                switch (mb)
                {
                    case MouseButtons.Left:
                        addVoiceCommandKey(Keys.LButton);
                        break;
                    case MouseButtons.Right:
                        addVoiceCommandKey(Keys.RButton);
                        break;
                    case MouseButtons.Middle:
                        addVoiceCommandKey(Keys.MButton);
                        break;
                }
            }
            else
            {
                commandFocusCounter ++;
            }

        }

        private void addVoiceCommandKey(Keys newKey)
        {
            txtVoiceCommandKeys.Text = "";

            voiceCommandKeys.Add(newKey);

            string[] keyStrings = new string[voiceCommandKeys.Count];
            for (int i = 0; i < keyStrings.Length; i++)
            {
                keyStrings[i] = voiceCommandKeys[i].ToString();
            }

            txtVoiceCommandKeys.Text = String.Join(", ", keyStrings);
        }

        private void btnClearVoiceCommand_Click(object sender, EventArgs e)
        {
            txtVoiceCommandWord.Clear();
            txtVoiceCommandKeys.Clear();
            voiceCommandKeys = new List<Keys>();
        }

        private void FillVoiceCommandTable()
        {
            dgvVoiceCommands.Rows.Clear();

            //Currently only displays voice commands
            foreach (VoiceCommand c in settings.Commands.OfType<VoiceCommand>())
            {
                dgvVoiceCommands.Rows.Add(c.VoiceKeyword, c.GetKeyStrings());
            }
            dgvVoiceCommands.ClearSelection();
        }

        private void FillStartProgramTable()
        {
            dgvOpenProgram.Rows.Clear();
            //Currently only displays voice commands
            foreach (OpenProgramCommand c in settings.Commands.OfType<OpenProgramCommand>())
            {
                dgvOpenProgram.Rows.Add(c.GetKeyWord(), c.GetProgramLocation());
            }
        }

        private void FillKeyBindingTable()
        {
            dgvKeyBinding.Rows.Clear();

            foreach (KeyPressCommand c in settings.Commands.OfType<KeyPressCommand>())
            {
                dgvKeyBinding.Rows.Add(c.triggerKey.ToString(), c.GetKeyWord());
            }
        }

        private void btnAddOpenProgramCommand_Click(object sender, EventArgs e)
        {
            //Checking that the user has extered a valid program path
            if (File.Exists(txtExecutablePath.Text))
            {
                Command command = new OpenProgramCommand(txtExecutablePath.Text, txtOpenProgramCommandWord.Text.ToLower());
                settings.Commands.Add(command);
                FillStartProgramTable();
            }
            else
            {
                MessageBox.Show("Please make sure the executable file's address is correct", "Error",
                    MessageBoxButtons.OK);
            }

        }

        private void btnClearOpenProgram_Click(object sender, EventArgs e)
        {
            txtExecutablePath.Clear();
            txtOpenProgramCommandWord.Clear();
        }

        private void btnOpenProgram_Click(object sender, EventArgs e)
        {
            //Creating open file dialog for user to find the program they want to command to open
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            ofd.Filter = "Executable (*.exe)|*.exe";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtExecutablePath.Text = ofd.FileName;
            }
        }

        private void btnAddKeyBinding_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbxCommandWords.SelectedText))
            {
                MessageBox.Show("You must select a command that is triggered when the key is pressed", "Incorrect Input",
                    MessageBoxButtons.OK);
            }
            else if (txtBindKey.Text == null)
            {
                MessageBox.Show("You must set a key that triggers the command", "Incorrect Input", MessageBoxButtons.OK);
            }
            else
            {
                KeyPressCommand newCommand = new KeyPressCommand(cmbxCommandWords.SelectedText, (Keys) bindingKey);
            }
        }

        private void txtBindKey_KeyUp(object sender, KeyEventArgs e)
        {

            bindingKey = e.KeyCode;
            txtBindKey.Text = bindingKey.ToString();
        }

        private void btnClearKeyBinding_Click(object sender, EventArgs e)
        {
            txtBindKey.Clear();
            bindingKey = null;
        }

        #region //delete functions

        private void btnDeleteSelectedVoiceCommands_Click(object sender, EventArgs e)
        {
            deleteCommand<VoiceCommand>(dgvVoiceCommands);
        }

        private void btnDeleteSelectedOpenProgramCommand_Click(object sender, EventArgs e)
        {
            deleteCommand<OpenProgramCommand>(dgvOpenProgram);
        }

        private void btnDeleteSelectedKeyBinding_Click(object sender, EventArgs e)
        {
            deleteCommand<KeyPressCommand>(dgvKeyBinding);
        }

        private void deleteCommand<T>(DataGridView selecteDataGridView)
        {
            if (
                MessageBox.Show("Are you sure you want to delete this command?", "Confirm deletion",
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                // Break off the method if the user does not wish to delete the command
                return;
            }
            //Check if an item is selected
            if (selecteDataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("You must first select a command to delete", "Error", MessageBoxButtons.OK);
                return;
            }
            int selectedCommandWordIndex = selecteDataGridView.SelectedRows[0].Index;

            //Commands must be searched through manually since each datya grid view only holds a subset of each command type, so the 
            //straight index from the dgv won't nessicarily match the idext of the item in the list that needs to be removed.
            int searchindex = 0;
            foreach (T command in settings.Commands.OfType<T>())
            {
                if (searchindex == selectedCommandWordIndex)
                {
                    settings.Commands.Remove((Command) command);
                    return;
                }
                else
                {
                    searchindex++;
                }
            }
        }

        #endregion

        private void commandKeysFeildFocusChanged(object sender, EventArgs e)
        {
            commandFocusCounter = 0;
        }

        private void txtVoiceCommandKeys_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chkbxDebugEyeTracking_CheckedChanged(object sender, EventArgs e)
        {
            SettingsSingleton.Instance().DebugEyeMouseMode = chkbxDebugEyeTracking.Checked;
            setDebugEyeState();
        }

        private void setDebugEyeState()
        {
            EventSingleton.Instance().setMouseFixationsStatus(chkbxDebugEyeTracking.Checked);
        }

        private void btnVisualiseFixations_CheckedChanged(object sender, EventArgs e)
        {
            SharedFormsSingleton.Instance().EnableFixationVisualisation(btnVisualiseFixations.Checked);
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            EventSingleton.Instance().eyex.LaunchRecalibration();
        }

        private void chkbxZoomEnabled_CheckedChanged(object sender, EventArgs e)
        {
            settings.ZoomEnabled = chkbxZoomEnabled.Checked;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int width, height;
            if (int.TryParse(udFormWidth.Text, out width) && int.TryParse(udFormHeight.Text, out height))
            {
                settings.ZoomFormSize = new Size(width, height);
            }
            
            settings.saveCommands();
            settings.saveSettings();
        }

        private void rbWindowsVoice_CheckedChanged(object sender, EventArgs e)
        {
            settings.DragonEnabled = false;
        }

        private void rbDragon_CheckedChanged(object sender, EventArgs e)
        {
            //if the radio box is checked, and a file path has been set, then allow the program to be enabled, otherwise give an error.
            if (File.Exists(Settings.Default.DragonFileAddress) && rbDragon.Checked)
            {
                settings.DragonEnabled = true;
                txtbxDragonFile.Text = Settings.Default.DragonFileAddress;
            }
            else if (rbDragon.Checked)
            {
                MessageBox.Show("You must fist add an address for the dragon exe file", "Error", MessageBoxButtons.OK);
                rbDragon.Checked = false;
                rbWindowsVoice.Checked = true;
            }
            else
            {
                settings.DragonEnabled = false;
            }
            
        }

        private void btnDragonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            ofd.Filter = "Executable (*.exe)|*.exe";
            ofd.Title = "Find Dragon NaturallySpeaking .exe File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxDragonFile.Text = ofd.FileName;
                Settings.Default.DragonFileAddress = ofd.FileName;
                txtbxDragonFile.Text = Settings.Default.DragonFileAddress;
            }
        }

        private void chkbxOptikeyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            //if the check box is checked, and a file path has been set, then allow the program to be enabled, otherwise give an error.
            if (chkbxOptikeyEnabled.Checked && File.Exists(Settings.Default.OptiKeyFileAddress)) 
            {
               SettingsSingleton.Instance().OptiKeyEnabled = true;
            }
            else if(chkbxOptikeyEnabled.Checked)
            {
                MessageBox.Show("You must fist add an address for the OptiKey .exe file", "Error", MessageBoxButtons.OK);
                chkbxOptikeyEnabled.Checked = false;
            }
            else
            {
                SettingsSingleton.Instance().OptiKeyEnabled = false;
            }
        }

        private void btnOptikeyAddress_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            ofd.Filter = "Executable (*.exe)|*.exe";
            ofd.Title = "Find Optikey .exe File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtbxDragonFile.Text = ofd.FileName;
                Settings.Default.OptiKeyFileAddress = ofd.FileName;
                txtbxOptikeyAddress.Text = Settings.Default.OptiKeyFileAddress;
            }
        }
       

        private void trkbrMagnificationAmount_Scroll(object sender, EventArgs e)
        {
            settings.ZoomMagnification = trkbrMagnificationAmount.Value;
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            EventSingleton.Instance().eyex.LaunchProfileCreation();
        }
    }
}
