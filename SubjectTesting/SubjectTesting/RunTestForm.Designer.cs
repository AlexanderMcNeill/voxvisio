namespace SubjectTesting
{
    partial class RunTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunTestForm));
            this.taskProgressBar = new System.Windows.Forms.ProgressBar();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.taskTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // taskProgressBar
            // 
            resources.ApplyResources(this.taskProgressBar, "taskProgressBar");
            this.taskProgressBar.Name = "taskProgressBar";
            // 
            // lblTask
            // 
            resources.ApplyResources(this.lblTask, "lblTask");
            this.lblTask.Name = "lblTask";
            // 
            // lblInstruction
            // 
            resources.ApplyResources(this.lblInstruction, "lblInstruction");
            this.lblInstruction.Name = "lblInstruction";
            // 
            // taskTimer
            // 
            this.taskTimer.Interval = 1000;
            // 
            // RunTestForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lblTask);
            this.Controls.Add(this.taskProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RunTestForm";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RunTestForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar taskProgressBar;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Timer taskTimer;

    }
}

