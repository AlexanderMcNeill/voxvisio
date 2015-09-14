﻿namespace VoxVisio.UI
{
    partial class SettingsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabEyeTracking = new System.Windows.Forms.TabPage();
            this.tabVoiceRecognition = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabEyeTracking);
            this.tabControl1.Controls.Add(this.tabVoiceRecognition);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1139, 591);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(1131, 565);
            this.tabGeneral.TabIndex = 1;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabEyeTracking
            // 
            this.tabEyeTracking.Location = new System.Drawing.Point(4, 22);
            this.tabEyeTracking.Name = "tabEyeTracking";
            this.tabEyeTracking.Padding = new System.Windows.Forms.Padding(3);
            this.tabEyeTracking.Size = new System.Drawing.Size(1131, 565);
            this.tabEyeTracking.TabIndex = 2;
            this.tabEyeTracking.Text = "Eye Tracking";
            this.tabEyeTracking.UseVisualStyleBackColor = true;
            // 
            // tabVoiceRecognition
            // 
            this.tabVoiceRecognition.Location = new System.Drawing.Point(4, 22);
            this.tabVoiceRecognition.Name = "tabVoiceRecognition";
            this.tabVoiceRecognition.Size = new System.Drawing.Size(1131, 565);
            this.tabVoiceRecognition.TabIndex = 3;
            this.tabVoiceRecognition.Text = "Voice Recognition";
            this.tabVoiceRecognition.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 615);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabEyeTracking;
        private System.Windows.Forms.TabPage tabVoiceRecognition;
    }
}