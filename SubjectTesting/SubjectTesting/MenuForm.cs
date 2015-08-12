using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubjectTesting
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnEditQuestions_Click(object sender, EventArgs e)
        {

        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            RunTestForm runTestForm = new RunTestForm(new String[] { "Question 1", "Question 2", "Question 3" });
            runTestForm.Show();
        }

        private void btnLoadTasks_Click(object sender, EventArgs e)
        {
            OpenFileDialog openTasksDialog = new OpenFileDialog();

            openTasksDialog.Filter = "JSON File (.json)|*.json";

        }
    }
}
