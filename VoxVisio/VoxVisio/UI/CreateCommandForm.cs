using System;
using System.Linq;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    public partial class CreateCommandForm : Form
    {
        public CreateCommandForm()
        {
            InitializeComponent();
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
                txtProgramAddress.Text = ofd.FileName;
            }
        }

        private void CreateCommandForm_Load(object sender, EventArgs e)
        {

        }

        private void rbVoiceCommand_CheckedChanged(object sender, EventArgs e)
        {
            SetCorrectPanel();
        }

        private void rbKeyTrigger_CheckedChanged(object sender, EventArgs e)
        {
            SetCorrectPanel();
        }

        private void rbOpenProgram_CheckedChanged(object sender, EventArgs e)
        {
            SetCorrectPanel();
        }

        private void SetCorrectPanel()
        {
            var checkedButton = this.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);
            switch (checkedButton.Text)
            {
                case "Voice Command":
                    pnlVoiceCommand.Visible = true;
                    pnlVoiceCommand.Enabled = true;
                    pnlOpenProgram.Visible = false;
                    pnlOpenProgram.Enabled = false;
                    pnlTriggerKey.Enabled = false;
                    pnlTriggerKey.Visible = false;
                    break;
                case "Trigger Key":
                    pnlVoiceCommand.Visible = false;
                    pnlVoiceCommand.Enabled = false;
                    pnlOpenProgram.Visible = false;
                    pnlOpenProgram.Enabled = false;
                    pnlTriggerKey.Enabled = true;
                    pnlTriggerKey.Visible = true;
                    break;
                case "Open Program":
                    pnlVoiceCommand.Visible = false;
                    pnlVoiceCommand.Enabled = false;
                    pnlOpenProgram.Visible = true;
                    pnlOpenProgram.Enabled = true;
                    pnlTriggerKey.Enabled = false;
                    pnlTriggerKey.Visible = false;
                    break;

            }
        }

    }
}
