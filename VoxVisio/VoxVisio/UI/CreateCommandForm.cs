using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.UI
{
    public partial class CreateCommandForm : Form
    {
        private Command command;
        private List<Keys> pressedKeys;
        private Keys? triggerKey;
        public CreateCommandForm()
        {
            InitializeComponent();
            command = null;
            pressedKeys = new List<Keys>();
            triggerKey = null;
        }

        public CreateCommandForm(Command command)
        {
            InitializeComponent();
            this.command = command;
            pressedKeys = new List<Keys>();
        }

        private void btnOpenProgram_Click(object sender, EventArgs e)
        {
            //Creating open file dialog for user to find the program they want to command to open
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            ofd.Filter = "Executable (*.exe)|*.exe";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFileAddress.Text = ofd.FileName;
            }
        }

        private void radioButtons_CheckChanged(object sender, EventArgs e)
        {
            ChangeCommandType();
        }

        private void ChangeCommandType()
        {
            var checkedButton = grpbxCommand.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);

            // Displaying the panel associated with the selected radio button
            switch (checkedButton.Text)
            {
                case "Voice Command":
                    enableSinglePanel(pnlVoiceCommand);
                    break;
                case "Trigger Key":
                    enableSinglePanel(pnlTriggerKey);
                    //Reloading the command words the selected key could trigger
                    cmbxCommandWords.Items.Clear();
                    SettingsSingleton.Instance().Commands.ForEach(x => cmbxCommandWords.Items.Add(x.GetKeyWord()));
                    break;
                case "Open Program":
                    enableSinglePanel(pnlOpenProgram);
                    break;
            }
        }

        //Method that displays the input panel and hides the rest
        private void enableSinglePanel(Panel toEnablePanel)
        {
            foreach (var panel in this.Controls.OfType<Panel>())
            {
                panel.Visible = panel == toEnablePanel;
            }
            
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            var checkedButton = grpbxCommand.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);

            switch (checkedButton.Text)
            {
                case "Voice Command":
                    CreateVoiceCommand();
                    break;
                case "Trigger Key":
                    CreateTriggerCommand();
                    break;
                case "Open Program":
                    CreateOpenProgramCommand();
                    break;
                default:
                    this.DialogResult = DialogResult.Cancel;
                    break;

            }
            //this.DialogResult = DialogResult.OK;

        }

        private void CreateOpenProgramCommand()
        {
            //Checking that the user has extered a valid program path
            if (File.Exists(txtFileAddress.Text))
            {
                MessageBox.Show("Please make sure the executable file's address is correct", "Error", MessageBoxButtons.OK);
                return;
            }
            command = new OpenProgramCommand(txtFileAddress.Text, txtProgramKeyWord.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void CreateVoiceCommand()
        {
            // Checking that the user has filled out the form correctly
            if (txtVoiceKeyword.Text == "" || txtKeysToPress.Text == "")
            {
                MessageBox.Show("Please ensure you have added a voice keyword and selected the keys you want to fire.", "Error", MessageBoxButtons.OK);
                return;
            }

            string keystrings = "";
            pressedKeys.ForEach(x => keystrings += (x).ToString() + " ");
            keystrings = keystrings.TrimEnd();
            keystrings = keystrings.Replace(" ", ",");
            command = new VoiceCommand(txtVoiceKeyword.Text, keystrings, SharedObjectsSingleton.Instance().inputSimulator);
            this.DialogResult = DialogResult.OK;
        }

        private void CreateTriggerCommand()
        {
            if (string.IsNullOrEmpty(cmbxCommandWords.SelectedText))
            {
                MessageBox.Show("You must select a command that is triggered when the key is pressed","Incorrect Input",
                    MessageBoxButtons.OK);
                return;
            }
            if (triggerKey == null)
            {
                MessageBox.Show("You must set a key that triggers the command", "Incorrect Input",
                   MessageBoxButtons.OK);
                return;
            }
            KeyPressCommand newCommand = new KeyPressCommand(cmbxCommandWords.SelectedText, (Keys) triggerKey);
            this.DialogResult = DialogResult.OK;
        }

        public Command Command
        {
            get { return command; }
        }

        private void txtboxTwo_KeyUp(object sender, KeyEventArgs e)
        {
            txtKeysToPress.Text = "";
            pressedKeys.Add(e.KeyCode);
            pressedKeys.ForEach(x => txtKeysToPress.Text += x.ToString() + ",");
        }

    }
}
