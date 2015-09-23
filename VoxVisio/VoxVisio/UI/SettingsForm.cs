using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
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
    }
}
