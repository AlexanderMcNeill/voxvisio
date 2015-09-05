using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            DialogResult result = openTasksDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ReadTasks(openTasksDialog.OpenFile());
            }
        }

        private void ReadTasks(Stream inputFileStream)
        { 
            StreamReader reader = new StreamReader(inputFileStream);
            JObject tasksBase = JObject.Parse(reader.ReadToEnd());
            JArray taskArray = (JArray)tasksBase["tasks"];

            List<TestTask> tasks = new List<TestTask>();

            for (int i = 0; i < taskArray.Count; i++)
            {
                JObject taskJson = (JObject)taskArray[i];

                string taskDescription = (string)taskJson["description"];
                JArray taskStepsJson = (JArray)taskJson["steps"];
                List<string> taskSteps = new List<string>();

                for (int j = 0; j < taskStepsJson.Count; j++)
                {
                    taskSteps.Add((String)taskStepsJson[j]);
                }

                tasks.Add(new TestTask(taskDescription, taskSteps));
            }
            
        }
    }
}
