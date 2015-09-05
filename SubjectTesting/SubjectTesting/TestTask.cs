using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTesting
{
    class TestTask
    {
        private string taskDescription;
        private List<String> taskSteps;

        public TestTask(string taskDescription, List<string> taskSteps)
        {
            this.taskDescription = taskDescription;
            this.taskSteps = taskSteps;
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }
        public List<String> TaskSteps
        {
            get { return taskSteps; }
            set { taskSteps = value; }
        }
    }
}
