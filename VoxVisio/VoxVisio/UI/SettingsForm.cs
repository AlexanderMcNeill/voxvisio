using System;
using System.Collections.Generic;using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxVisio.Singletons;

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
            FillVoiceCommandTable();
            FillKeyBindingTable();
            FillStartProgramTable();
            commandFocusCounter = 0;
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            CreateCommandForm newCreateCommandForm = new CreateCommandForm();
            newCreateCommandForm.ShowDialog(this);

            if (newCreateCommandForm.DialogResult == DialogResult.OK)
            {
                Command newCommand = newCreateCommandForm.Command;
            }
            newCreateCommandForm.Dispose();
        }

        private void btnAddVoiceCommand_Click(object sender, EventArgs e)
        {
            // Checking that the user has filled out the form correctly
            if (txtVoiceCommandWord.Text == "" || txtVoiceCommandKeys.Text == "")
            {
                MessageBox.Show("Please ensure you have added a voice keyword and selected the keys you want to fire.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                string keystrings = "";
                voiceCommandKeys.ForEach(x => keystrings += (x).ToString() + " ");
                keystrings = keystrings.TrimEnd();
                keystrings = keystrings.Replace(" ", ",");
                Command command = new VoiceCommand(txtVoiceCommandWord.Text, keystrings, SharedObjectsSingleton.Instance().inputSimulator);
                settings.Commands.Add(command);
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
                Command command = new OpenProgramCommand(txtExecutablePath.Text, txtOpenProgramCommandWord.Text);
            }
            else
            {
                MessageBox.Show("Please make sure the executable file's address is correct", "Error", MessageBoxButtons.OK);
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
            ofd.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
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
                MessageBox.Show("You must select a command that is triggered when the key is pressed", "Incorrect Input", MessageBoxButtons.OK);
            }
            else if (txtBindKey.Text == null)
            {
                MessageBox.Show("You must set a key that triggers the command", "Incorrect Input", MessageBoxButtons.OK);
            }
            else
            {
                KeyPressCommand newCommand = new KeyPressCommand(cmbxCommandWords.SelectedText, (Keys)bindingKey);
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

        private void btnDeleteSelectedVoiceCommands_Click(object sender, EventArgs e)
        {
            //If an item is selected
            int totalSelected = dgvVoiceCommands.SelectedCells.Count;
        }

        private void btnDeleteSelectedOpenProgramCommand_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSelectedKeyBinding_Click(object sender, EventArgs e)
        {

        }

        private void commandKeysFeildFocusChanged(object sender, EventArgs e)
        {
            commandFocusCounter = 0;
        }
    }
}
