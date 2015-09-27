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
            this.rbOpenProgram = new System.Windows.Forms.RadioButton();
            this.rbKeyTrigger = new System.Windows.Forms.RadioButton();
            this.rbVoiceCommand = new System.Windows.Forms.RadioButton();
            this.txtboxTwo = new System.Windows.Forms.TextBox();
            this.txtboxOne = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenProgram = new System.Windows.Forms.Button();
            this.btnAddCommand = new System.Windows.Forms.Button();
            this.pnlVoiceCommand = new System.Windows.Forms.Panel();
            this.pnlTriggerKey = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbxCommandWords = new System.Windows.Forms.ComboBox();
            this.pnlOpenProgram = new System.Windows.Forms.Panel();
            this.txtFileAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProgramKeyWord = new System.Windows.Forms.TextBox();
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
            // txtboxTwo
            // 
            this.txtboxTwo.Location = new System.Drawing.Point(99, 39);
            this.txtboxTwo.Name = "txtboxTwo";
            this.txtboxTwo.Size = new System.Drawing.Size(100, 20);
            this.txtboxTwo.TabIndex = 3;
            this.txtboxTwo.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            this.txtboxTwo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            this.txtboxTwo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtboxTwo_KeyUp);
            // 
            // txtboxOne
            // 
            this.txtboxOne.Location = new System.Drawing.Point(99, 9);
            this.txtboxOne.Name = "txtboxOne";
            this.txtboxOne.Size = new System.Drawing.Size(100, 20);
            this.txtboxOne.TabIndex = 2;
            this.txtboxOne.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Keys To Press :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voice Key Word :";
            // 
            // btnOpenProgram
            // 
            this.btnOpenProgram.Location = new System.Drawing.Point(112, 47);
            this.btnOpenProgram.Name = "btnOpenProgram";
            this.btnOpenProgram.Size = new System.Drawing.Size(64, 23);
            this.btnOpenProgram.TabIndex = 4;
            this.btnOpenProgram.Text = "Browse..";
            this.btnOpenProgram.UseVisualStyleBackColor = true;
            this.btnOpenProgram.Click += new System.EventHandler(this.btnOpenProgram_Click);
            // 
            // btnAddCommand
            // 
            this.btnAddCommand.Location = new System.Drawing.Point(13, 145);
            this.btnAddCommand.Name = "btnAddCommand";
            this.btnAddCommand.Size = new System.Drawing.Size(142, 23);
            this.btnAddCommand.TabIndex = 4;
            this.btnAddCommand.Text = "Add Command";
            this.btnAddCommand.UseVisualStyleBackColor = true;
            this.btnAddCommand.Click += new System.EventHandler(this.btnAddCommand_Click);
            // 
            // pnlVoiceCommand
            // 
            this.pnlVoiceCommand.Controls.Add(this.label1);
            this.pnlVoiceCommand.Controls.Add(this.txtboxOne);
            this.pnlVoiceCommand.Controls.Add(this.label2);
            this.pnlVoiceCommand.Controls.Add(this.txtboxTwo);
            this.pnlVoiceCommand.Location = new System.Drawing.Point(170, 15);
            this.pnlVoiceCommand.Name = "pnlVoiceCommand";
            this.pnlVoiceCommand.Size = new System.Drawing.Size(266, 91);
            this.pnlVoiceCommand.TabIndex = 5;
            this.pnlVoiceCommand.Visible = false;
            // 
            // pnlTriggerKey
            // 
            this.pnlTriggerKey.Controls.Add(this.label4);
            this.pnlTriggerKey.Controls.Add(this.textBox1);
            this.pnlTriggerKey.Controls.Add(this.label3);
            this.pnlTriggerKey.Controls.Add(this.cmbxCommandWords);
            this.pnlTriggerKey.Location = new System.Drawing.Point(159, 20);
            this.pnlTriggerKey.Name = "pnlTriggerKey";
            this.pnlTriggerKey.Size = new System.Drawing.Size(261, 75);
            this.pnlTriggerKey.TabIndex = 6;
            this.pnlTriggerKey.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Trigger Key :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Command to Trigger :";
            // 
            // cmbxCommandWords
            // 
            this.cmbxCommandWords.FormattingEnabled = true;
            this.cmbxCommandWords.Location = new System.Drawing.Point(117, 8);
            this.cmbxCommandWords.Name = "cmbxCommandWords";
            this.cmbxCommandWords.Size = new System.Drawing.Size(121, 21);
            this.cmbxCommandWords.TabIndex = 1;
            // 
            // pnlOpenProgram
            // 
            this.pnlOpenProgram.Controls.Add(this.label5);
            this.pnlOpenProgram.Controls.Add(this.txtProgramKeyWord);
            this.pnlOpenProgram.Controls.Add(this.txtFileAddress);
            this.pnlOpenProgram.Controls.Add(this.btnOpenProgram);
            this.pnlOpenProgram.Location = new System.Drawing.Point(168, 14);
            this.pnlOpenProgram.Name = "pnlOpenProgram";
            this.pnlOpenProgram.Size = new System.Drawing.Size(202, 114);
            this.pnlOpenProgram.TabIndex = 7;
            this.pnlOpenProgram.Visible = false;
            // 
            // txtFileAddress
            // 
            this.txtFileAddress.Enabled = false;
            this.txtFileAddress.Location = new System.Drawing.Point(6, 49);
            this.txtFileAddress.Name = "txtFileAddress";
            this.txtFileAddress.Size = new System.Drawing.Size(100, 20);
            this.txtFileAddress.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Voice Key Word :";
            // 
            // txtProgramKeyWord
            // 
            this.txtProgramKeyWord.Location = new System.Drawing.Point(95, 13);
            this.txtProgramKeyWord.Name = "txtProgramKeyWord";
            this.txtProgramKeyWord.Size = new System.Drawing.Size(100, 20);
            this.txtProgramKeyWord.TabIndex = 7;
            // 
            // CreateCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 177);
            this.Controls.Add(this.pnlOpenProgram);
            this.Controls.Add(this.pnlTriggerKey);
            this.Controls.Add(this.pnlVoiceCommand);
            this.Controls.Add(this.btnAddCommand);
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
        private System.Windows.Forms.TextBox txtboxTwo;
        private System.Windows.Forms.TextBox txtboxOne;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenProgram;
        private System.Windows.Forms.Button btnAddCommand;
        private System.Windows.Forms.Panel pnlVoiceCommand;
        private System.Windows.Forms.Panel pnlTriggerKey;
        private System.Windows.Forms.Panel pnlOpenProgram;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbxCommandWords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProgramKeyWord;
        private System.Windows.Forms.TextBox txtFileAddress;
    }
}