﻿namespace SubjectTesting
{
    partial class MenuForm
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
            this.btnStartTest = new System.Windows.Forms.Button();
            this.btnEditQuestions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(384, 131);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(190, 74);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // btnEditQuestions
            // 
            this.btnEditQuestions.Location = new System.Drawing.Point(384, 241);
            this.btnEditQuestions.Name = "btnEditQuestions";
            this.btnEditQuestions.Size = new System.Drawing.Size(190, 74);
            this.btnEditQuestions.TabIndex = 1;
            this.btnEditQuestions.Text = "Edit Questions";
            this.btnEditQuestions.UseVisualStyleBackColor = true;
            this.btnEditQuestions.Click += new System.EventHandler(this.btnEditQuestions_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 537);
            this.Controls.Add(this.btnEditQuestions);
            this.Controls.Add(this.btnStartTest);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnEditQuestions;
    }
}