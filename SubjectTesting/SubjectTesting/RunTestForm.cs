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
    public partial class RunTestForm : Form
    {
        private bool timing = false;
        private DateTime taskStartTime;
        private string[] tasks;
        private double[] taskResults;
        private int currentTask = 0;
        private int progressBarStep;

        public RunTestForm(string[] tasks)
        {
            InitializeComponent();

            this.tasks = tasks;
            taskResults = new double[tasks.Length];
            progressBarStep = 100 / tasks.Length;

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, 30);
            TopMost = true;
            
            //Displaying the first task to the subject
            displayTask();
        }

        private void RunTestForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (timing)
                {
                    nextQuestion();
                }
                else
                {
                    startTimer();
                }
            }
        }

        private void nextQuestion()
        {
            //Getting how long it took the subject to complete the task in milliseconds
            DateTime taskEndTime = DateTime.Now;
            TimeSpan taskTime = taskEndTime - taskStartTime;
            taskResults[currentTask] = taskTime.TotalMilliseconds;

            //Updating bool that keeps track if the subject is reading the question or performing the task
            timing = false;

            //Updating the current task and displaying the new task to the subject
            currentTask++;

            taskProgressBar.Value = progressBarStep * currentTask;

            // Checking if the user has completed all the tasks
            if (currentTask < tasks.Length)
            {
                displayTask();
            }
            else 
            {
                FinishTestForm finishTestForm = new FinishTestForm(taskResults, tasks);
                finishTestForm.Show();
                this.Close();
            }
            
        }

        private void displayTask()
        {
            //Displaying the task and instructions on how to start the task
            lblTask.Text = tasks[currentTask];
            lblInstruction.Text = "Press space to start the task";
        }

        private void startTimer()
        {
            //Updating bool that keeps track if the subject is reading the question or performing the task
            timing = true;

            //Setting the start time to be the current time for working out how long the task has taken later
            taskStartTime = DateTime.Now;

            lblInstruction.Text = "Press space when you have completed the task";
        }
    }
}
