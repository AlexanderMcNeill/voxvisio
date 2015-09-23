using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsInput.Native;

namespace VoxVisio.UI
{
    public partial class CreateCommandForm : Form
    {
        private Command command;
        private List<Keys> pressedKeys; 
        public CreateCommandForm()
        {
            InitializeComponent();
            command = null;
            pressedKeys = new List<Keys>();
        }
        public CreateCommandForm(Command command)
        {
            InitializeComponent();
            this.command = command;
            pressedKeys = new List<Keys>();
        }

     

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenProgram_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //txtProgramAddress.Text = ofd.FileName;
            }
        }

        private void CreateCommandForm_Load(object sender, EventArgs e)
        {

        }

        private void rbVoiceCommand_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCommandType();
        }

        private void rbKeyTrigger_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCommandType();
        }

        private void rbOpenProgram_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCommandType();
        }

        private void ChangeCommandType()
        {
            var checkedButton = grpbxCommand.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);
            switch (checkedButton.Text)
            {
                case "Voice Command":
                    
                    break;
                case "Trigger Key":
                    
                    break;
                case "Open Program":
                   
                    break;

            }
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            var checkedButton = grpbxCommand.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);
            switch (checkedButton.Text)
            {
                case "Voice Command":
                    string keystrings = "";
                    pressedKeys.ForEach(x => keystrings += (x).ToString() +" ");
                    keystrings = keystrings.TrimEnd();
                    keystrings = keystrings.Replace(" ", ",");
                    command = new VoiceCommand(txtboxOne.Text, keystrings,SharedDataSingleton.Instance().inputSimulator );
                    this.DialogResult = DialogResult.OK;
                    break;
                case "Trigger Key":
                    
                    break;
                case "Open Program":
                    
                    break;
                default:
                    this.DialogResult = DialogResult.Cancel;
                    break;

            }
            //this.DialogResult = DialogResult.OK;

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }
        public Command Command
        {
            get { return command; }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtboxTwo_KeyUp(object sender, KeyEventArgs e)
        {
            txtboxTwo.Text = "";
            pressedKeys.Add(e.KeyCode);
            pressedKeys.ForEach(x => txtboxTwo.Text += x.ToString());
        }
    }
}
