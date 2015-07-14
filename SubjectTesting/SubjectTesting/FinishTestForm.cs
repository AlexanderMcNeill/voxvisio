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
    public partial class FinishTestForm : Form
    {
        public FinishTestForm(double[] results, string[] tasks)
        {
            InitializeComponent();

            for (int i = 0; i < tasks.Length; i++)
            {
                resultsListBox.Items.Add(tasks[i] + ": " + results[i]);
            }
        }
    }
}
