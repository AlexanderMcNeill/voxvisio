namespace VoxVisio.UI
{
    partial class CreateCommandForm
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
            this.grpbxCommand = new System.Windows.Forms.GroupBox();
            this.rbVoiceCommand = new System.Windows.Forms.RadioButton();
            this.rbKeyTrigger = new System.Windows.Forms.RadioButton();
            this.rbOpenProgram = new System.Windows.Forms.RadioButton();
            this.pnlVoiceCommand = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pnlTriggerKey = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlOpenProgram = new System.Windows.Forms.Panel();
            this.txtProgramAddress = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpenProgram = new System.Windows.Forms.Button();
            this.grpbxCommand.SuspendLayout();
            this.pnlVoiceCommand.SuspendLayout();
            this.pnlTriggerKey.SuspendLayout();
            this.pnlOpenProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxCommand
            // 
            this.grpbxCommand.Controls.Add(this.rbOpenProgram);
            this.grpbxCommand.Controls.Add(this.rbKeyTrigger);
            this.grpbxCommand.Controls.Add(this.rbVoiceCommand);
            this.grpbxCommand.Location = new System.Drawing.Point(13, 12);
            this.grpbxCommand.Name = "grpbxCommand";
            this.grpbxCommand.Size = new System.Drawing.Size(142, 129);
            this.grpbxCommand.TabIndex = 0;
            this.grpbxCommand.TabStop = false;
            this.grpbxCommand.Text = "Command Type";
            // 
            // rbVoiceCommand
            // 
            this.rbVoiceCommand.AutoSize = true;
            this.rbVoiceCommand.Checked = true;
            this.rbVoiceCommand.Location = new System.Drawing.Point(22, 28);
            this.rbVoiceCommand.Name = "rbVoiceCommand";
            this.rbVoiceCommand.Size = new System.Drawing.Size(102, 17);
            this.rbVoiceCommand.TabIndex = 0;
            this.rbVoiceCommand.TabStop = true;
            this.rbVoiceCommand.Text = "Voice Command";
            this.rbVoiceCommand.UseVisualStyleBackColor = true;
            this.rbVoiceCommand.CheckedChanged += new System.EventHandler(this.rbVoiceCommand_CheckedChanged);
            // 
            // rbKeyTrigger
            // 
            this.rbKeyTrigger.AutoSize = true;
            this.rbKeyTrigger.Location = new System.Drawing.Point(22, 51);
            this.rbKeyTrigger.Name = "rbKeyTrigger";
            this.rbKeyTrigger.Size = new System.Drawing.Size(79, 17);
            this.rbKeyTrigger.TabIndex = 1;
            this.rbKeyTrigger.Text = "Trigger Key";
            this.rbKeyTrigger.UseVisualStyleBackColor = true;
            this.rbKeyTrigger.CheckedChanged += new System.EventHandler(this.rbKeyTrigger_CheckedChanged);
            // 
            // rbOpenProgram
            // 
            this.rbOpenProgram.AutoSize = true;
            this.rbOpenProgram.Location = new System.Drawing.Point(22, 74);
            this.rbOpenProgram.Name = "rbOpenProgram";
            this.rbOpenProgram.Size = new System.Drawing.Size(93, 17);
            this.rbOpenProgram.TabIndex = 2;
            this.rbOpenProgram.Text = "Open Program";
            this.rbOpenProgram.UseVisualStyleBackColor = true;
            this.rbOpenProgram.CheckedChanged += new System.EventHandler(this.rbOpenProgram_CheckedChanged);
            // 
            // pnlVoiceCommand
            // 
            this.pnlVoiceCommand.Controls.Add(this.textBox2);
            this.pnlVoiceCommand.Controls.Add(this.textBox1);
            this.pnlVoiceCommand.Controls.Add(this.label2);
            this.pnlVoiceCommand.Controls.Add(this.label1);
            this.pnlVoiceCommand.Location = new System.Drawing.Point(177, 12);
            this.pnlVoiceCommand.Name = "pnlVoiceCommand";
            this.pnlVoiceCommand.Size = new System.Drawing.Size(265, 127);
            this.pnlVoiceCommand.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voice Key Word :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Keys To Press :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(123, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // pnlTriggerKey
            // 
            this.pnlTriggerKey.Controls.Add(this.textBox3);
            this.pnlTriggerKey.Controls.Add(this.textBox4);
            this.pnlTriggerKey.Controls.Add(this.label3);
            this.pnlTriggerKey.Controls.Add(this.label4);
            this.pnlTriggerKey.Location = new System.Drawing.Point(176, 145);
            this.pnlTriggerKey.Name = "pnlTriggerKey";
            this.pnlTriggerKey.Size = new System.Drawing.Size(265, 127);
            this.pnlTriggerKey.TabIndex = 2;
            this.pnlTriggerKey.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(123, 74);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(123, 28);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Keys To Trigger :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Voice Key Word :";
            // 
            // pnlOpenProgram
            // 
            this.pnlOpenProgram.Controls.Add(this.btnOpenProgram);
            this.pnlOpenProgram.Controls.Add(this.txtProgramAddress);
            this.pnlOpenProgram.Controls.Add(this.textBox6);
            this.pnlOpenProgram.Controls.Add(this.label5);
            this.pnlOpenProgram.Controls.Add(this.label6);
            this.pnlOpenProgram.Location = new System.Drawing.Point(177, 278);
            this.pnlOpenProgram.Name = "pnlOpenProgram";
            this.pnlOpenProgram.Size = new System.Drawing.Size(265, 127);
            this.pnlOpenProgram.TabIndex = 3;
            this.pnlOpenProgram.Visible = false;
            // 
            // txtProgramAddress
            // 
            this.txtProgramAddress.Location = new System.Drawing.Point(123, 70);
            this.txtProgramAddress.Name = "txtProgramAddress";
            this.txtProgramAddress.ReadOnly = true;
            this.txtProgramAddress.Size = new System.Drawing.Size(100, 20);
            this.txtProgramAddress.TabIndex = 3;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(123, 28);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Program To Open :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Voice Key Word :";
            // 
            // btnOpenProgram
            // 
            this.btnOpenProgram.Location = new System.Drawing.Point(123, 96);
            this.btnOpenProgram.Name = "btnOpenProgram";
            this.btnOpenProgram.Size = new System.Drawing.Size(100, 23);
            this.btnOpenProgram.TabIndex = 4;
            this.btnOpenProgram.Text = "Browse..";
            this.btnOpenProgram.UseVisualStyleBackColor = true;
            this.btnOpenProgram.Click += new System.EventHandler(this.btnOpenProgram_Click);
            // 
            // CreateCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 445);
            this.Controls.Add(this.pnlOpenProgram);
            this.Controls.Add(this.pnlTriggerKey);
            this.Controls.Add(this.pnlVoiceCommand);
            this.Controls.Add(this.grpbxCommand);
            this.Name = "CreateCommandForm";
            this.Text = "CreateCommandForm";
            this.Load += new System.EventHandler(this.CreateCommandForm_Load);
            this.grpbxCommand.ResumeLayout(false);
            this.grpbxCommand.PerformLayout();
            this.pnlVoiceCommand.ResumeLayout(false);
            this.pnlVoiceCommand.PerformLayout();
            this.pnlTriggerKey.ResumeLayout(false);
            this.pnlTriggerKey.PerformLayout();
            this.pnlOpenProgram.ResumeLayout(false);
            this.pnlOpenProgram.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxCommand;
        private System.Windows.Forms.RadioButton rbOpenProgram;
        private System.Windows.Forms.RadioButton rbKeyTrigger;
        private System.Windows.Forms.RadioButton rbVoiceCommand;
        private System.Windows.Forms.Panel pnlVoiceCommand;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTriggerKey;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlOpenProgram;
        private System.Windows.Forms.TextBox txtProgramAddress;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOpenProgram;
    }
}