using System.ComponentModel;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udFormHeight = new System.Windows.Forms.NumericUpDown();
            this.udFormWidth = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trkbrMagnificationAmount = new System.Windows.Forms.TrackBar();
            this.chkbxZoomEnabled = new System.Windows.Forms.CheckBox();
            this.tabEyeTracking = new System.Windows.Forms.TabPage();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.btnVisualiseFixations = new System.Windows.Forms.CheckBox();
            this.chkbxDebugEyeTracking = new System.Windows.Forms.CheckBox();
            this.tabVoiceRecognition = new System.Windows.Forms.TabPage();
            this.grpbxVoiceOption = new System.Windows.Forms.GroupBox();
            this.rbWindowsVoice = new System.Windows.Forms.RadioButton();
            this.rbDragon = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbAddHotkeyCmd = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddKeyBinding = new System.Windows.Forms.Button();
            this.btnClearKeyBinding = new System.Windows.Forms.Button();
            this.cmbxCommandWords = new System.Windows.Forms.ComboBox();
            this.txtBindKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbAddProgramCmd = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddOpenProgramCommand = new System.Windows.Forms.Button();
            this.btnClearOpenProgram = new System.Windows.Forms.Button();
            this.txtOpenProgramCommandWord = new System.Windows.Forms.TextBox();
            this.txtExecutablePath = new System.Windows.Forms.TextBox();
            this.btnOpenProgram = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAddVoiceCmd = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddVoiceCommand = new System.Windows.Forms.Button();
            this.btnClearVoiceCommand = new System.Windows.Forms.Button();
            this.txtVoiceCommandWord = new System.Windows.Forms.TextBox();
            this.txtVoiceCommandKeys = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteSelectedKeyBinding = new System.Windows.Forms.Button();
            this.btnDeleteSelectedOpenProgramCommand = new System.Windows.Forms.Button();
            this.btnDeleteSelectedVoiceCommands = new System.Windows.Forms.Button();
            this.dgvKeyBinding = new System.Windows.Forms.DataGridView();
            this.colKeyBind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoiceCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOpenProgram = new System.Windows.Forms.DataGridView();
            this.colProgramCommandWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProgramPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVoiceCommands = new System.Windows.Forms.DataGridView();
            this.colCommandWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblKeyBindingTitle = new System.Windows.Forms.Label();
            this.lblOpenProgramTitle = new System.Windows.Forms.Label();
            this.lblVoiceCommandTitle = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtbxDragonFile = new System.Windows.Forms.TextBox();
            this.btnDragonFile = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFormWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbrMagnificationAmount)).BeginInit();
            this.tabEyeTracking.SuspendLayout();
            this.tabVoiceRecognition.SuspendLayout();
            this.grpbxVoiceOption.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbAddHotkeyCmd.SuspendLayout();
            this.gbAddProgramCmd.SuspendLayout();
            this.gbAddVoiceCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyBinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenProgram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoiceCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabEyeTracking);
            this.tabControl1.Controls.Add(this.tabVoiceRecognition);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1139, 583);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(1131, 557);
            this.tabGeneral.TabIndex = 1;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udFormHeight);
            this.groupBox1.Controls.Add(this.udFormWidth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.trkbrMagnificationAmount);
            this.groupBox1.Controls.Add(this.chkbxZoomEnabled);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1118, 244);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zoom Click Settings";
            // 
            // udFormHeight
            // 
            this.udFormHeight.Location = new System.Drawing.Point(102, 180);
            this.udFormHeight.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.udFormHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udFormHeight.Name = "udFormHeight";
            this.udFormHeight.Size = new System.Drawing.Size(120, 20);
            this.udFormHeight.TabIndex = 6;
            this.udFormHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // udFormWidth
            // 
            this.udFormWidth.Location = new System.Drawing.Point(102, 139);
            this.udFormWidth.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.udFormWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udFormWidth.Name = "udFormWidth";
            this.udFormWidth.Size = new System.Drawing.Size(120, 20);
            this.udFormWidth.TabIndex = 5;
            this.udFormWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Form Height:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Form Width:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Magnification:";
            // 
            // trkbrMagnificationAmount
            // 
            this.trkbrMagnificationAmount.LargeChange = 2;
            this.trkbrMagnificationAmount.Location = new System.Drawing.Point(102, 54);
            this.trkbrMagnificationAmount.Maximum = 5;
            this.trkbrMagnificationAmount.Minimum = 1;
            this.trkbrMagnificationAmount.Name = "trkbrMagnificationAmount";
            this.trkbrMagnificationAmount.Size = new System.Drawing.Size(104, 45);
            this.trkbrMagnificationAmount.TabIndex = 1;
            this.trkbrMagnificationAmount.Value = 1;
            // 
            // chkbxZoomEnabled
            // 
            this.chkbxZoomEnabled.AutoSize = true;
            this.chkbxZoomEnabled.Location = new System.Drawing.Point(6, 19);
            this.chkbxZoomEnabled.Name = "chkbxZoomEnabled";
            this.chkbxZoomEnabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkbxZoomEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkbxZoomEnabled.TabIndex = 0;
            this.chkbxZoomEnabled.Text = "Enabled";
            this.chkbxZoomEnabled.UseVisualStyleBackColor = true;
            this.chkbxZoomEnabled.CheckedChanged += new System.EventHandler(this.chkbxZoomEnabled_CheckedChanged);
            // 
            // tabEyeTracking
            // 
            this.tabEyeTracking.Controls.Add(this.btnCalibrate);
            this.tabEyeTracking.Controls.Add(this.btnVisualiseFixations);
            this.tabEyeTracking.Controls.Add(this.chkbxDebugEyeTracking);
            this.tabEyeTracking.Location = new System.Drawing.Point(4, 22);
            this.tabEyeTracking.Name = "tabEyeTracking";
            this.tabEyeTracking.Padding = new System.Windows.Forms.Padding(3);
            this.tabEyeTracking.Size = new System.Drawing.Size(1131, 557);
            this.tabEyeTracking.TabIndex = 2;
            this.tabEyeTracking.Text = "Eye Tracking";
            this.tabEyeTracking.UseVisualStyleBackColor = true;
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrate.Location = new System.Drawing.Point(385, 227);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(280, 105);
            this.btnCalibrate.TabIndex = 2;
            this.btnCalibrate.Text = "Calibrate Eye Tracker";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // btnVisualiseFixations
            // 
            this.btnVisualiseFixations.AutoSize = true;
            this.btnVisualiseFixations.Location = new System.Drawing.Point(22, 54);
            this.btnVisualiseFixations.Name = "btnVisualiseFixations";
            this.btnVisualiseFixations.Size = new System.Drawing.Size(111, 17);
            this.btnVisualiseFixations.TabIndex = 1;
            this.btnVisualiseFixations.Text = "Visualise Fixations";
            this.btnVisualiseFixations.UseVisualStyleBackColor = true;
            this.btnVisualiseFixations.CheckedChanged += new System.EventHandler(this.btnVisualiseFixations_CheckedChanged);
            // 
            // chkbxDebugEyeTracking
            // 
            this.chkbxDebugEyeTracking.AutoSize = true;
            this.chkbxDebugEyeTracking.Location = new System.Drawing.Point(22, 22);
            this.chkbxDebugEyeTracking.Name = "chkbxDebugEyeTracking";
            this.chkbxDebugEyeTracking.Size = new System.Drawing.Size(158, 17);
            this.chkbxDebugEyeTracking.TabIndex = 0;
            this.chkbxDebugEyeTracking.Text = "Debug Mouse as Eye Mode";
            this.chkbxDebugEyeTracking.UseVisualStyleBackColor = true;
            this.chkbxDebugEyeTracking.CheckedChanged += new System.EventHandler(this.chkbxDebugEyeTracking_CheckedChanged);
            // 
            // tabVoiceRecognition
            // 
            this.tabVoiceRecognition.Controls.Add(this.txtbxDragonFile);
            this.tabVoiceRecognition.Controls.Add(this.btnDragonFile);
            this.tabVoiceRecognition.Controls.Add(this.label10);
            this.tabVoiceRecognition.Controls.Add(this.grpbxVoiceOption);
            this.tabVoiceRecognition.Location = new System.Drawing.Point(4, 22);
            this.tabVoiceRecognition.Name = "tabVoiceRecognition";
            this.tabVoiceRecognition.Size = new System.Drawing.Size(1131, 557);
            this.tabVoiceRecognition.TabIndex = 3;
            this.tabVoiceRecognition.Text = "Voice Recognition";
            this.tabVoiceRecognition.UseVisualStyleBackColor = true;
            // 
            // grpbxVoiceOption
            // 
            this.grpbxVoiceOption.Controls.Add(this.rbWindowsVoice);
            this.grpbxVoiceOption.Controls.Add(this.rbDragon);
            this.grpbxVoiceOption.Location = new System.Drawing.Point(15, 12);
            this.grpbxVoiceOption.Name = "grpbxVoiceOption";
            this.grpbxVoiceOption.Size = new System.Drawing.Size(200, 124);
            this.grpbxVoiceOption.TabIndex = 0;
            this.grpbxVoiceOption.TabStop = false;
            this.grpbxVoiceOption.Text = "Voice Dictation Mode";
            // 
            // rbWindowsVoice
            // 
            this.rbWindowsVoice.AutoSize = true;
            this.rbWindowsVoice.Location = new System.Drawing.Point(20, 29);
            this.rbWindowsVoice.Name = "rbWindowsVoice";
            this.rbWindowsVoice.Size = new System.Drawing.Size(159, 17);
            this.rbWindowsVoice.TabIndex = 1;
            this.rbWindowsVoice.TabStop = true;
            this.rbWindowsVoice.Text = "Windows Voice Recognition";
            this.rbWindowsVoice.UseVisualStyleBackColor = true;
            this.rbWindowsVoice.CheckedChanged += new System.EventHandler(this.rbWindowsVoice_CheckedChanged);
            // 
            // rbDragon
            // 
            this.rbDragon.AutoSize = true;
            this.rbDragon.Location = new System.Drawing.Point(20, 67);
            this.rbDragon.Name = "rbDragon";
            this.rbDragon.Size = new System.Drawing.Size(149, 17);
            this.rbDragon.TabIndex = 0;
            this.rbDragon.TabStop = true;
            this.rbDragon.Text = "Dragon NaturallySpeaking";
            this.rbDragon.UseVisualStyleBackColor = true;
            this.rbDragon.CheckedChanged += new System.EventHandler(this.rbDragon_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbAddHotkeyCmd);
            this.tabPage1.Controls.Add(this.gbAddProgramCmd);
            this.tabPage1.Controls.Add(this.gbAddVoiceCmd);
            this.tabPage1.Controls.Add(this.btnDeleteSelectedKeyBinding);
            this.tabPage1.Controls.Add(this.btnDeleteSelectedOpenProgramCommand);
            this.tabPage1.Controls.Add(this.btnDeleteSelectedVoiceCommands);
            this.tabPage1.Controls.Add(this.dgvKeyBinding);
            this.tabPage1.Controls.Add(this.dgvOpenProgram);
            this.tabPage1.Controls.Add(this.dgvVoiceCommands);
            this.tabPage1.Controls.Add(this.lblKeyBindingTitle);
            this.tabPage1.Controls.Add(this.lblOpenProgramTitle);
            this.tabPage1.Controls.Add(this.lblVoiceCommandTitle);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1131, 557);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Commands";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbAddHotkeyCmd
            // 
            this.gbAddHotkeyCmd.Controls.Add(this.label3);
            this.gbAddHotkeyCmd.Controls.Add(this.btnAddKeyBinding);
            this.gbAddHotkeyCmd.Controls.Add(this.btnClearKeyBinding);
            this.gbAddHotkeyCmd.Controls.Add(this.cmbxCommandWords);
            this.gbAddHotkeyCmd.Controls.Add(this.txtBindKey);
            this.gbAddHotkeyCmd.Controls.Add(this.label6);
            this.gbAddHotkeyCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddHotkeyCmd.Location = new System.Drawing.Point(773, 460);
            this.gbAddHotkeyCmd.Name = "gbAddHotkeyCmd";
            this.gbAddHotkeyCmd.Size = new System.Drawing.Size(350, 94);
            this.gbAddHotkeyCmd.TabIndex = 38;
            this.gbAddHotkeyCmd.TabStop = false;
            this.gbAddHotkeyCmd.Text = "Add Hotkey Command";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Key:";
            // 
            // btnAddKeyBinding
            // 
            this.btnAddKeyBinding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddKeyBinding.Location = new System.Drawing.Point(9, 67);
            this.btnAddKeyBinding.Name = "btnAddKeyBinding";
            this.btnAddKeyBinding.Size = new System.Drawing.Size(160, 23);
            this.btnAddKeyBinding.TabIndex = 13;
            this.btnAddKeyBinding.Text = "Add";
            this.btnAddKeyBinding.UseVisualStyleBackColor = true;
            this.btnAddKeyBinding.Click += new System.EventHandler(this.btnAddKeyBinding_Click);
            // 
            // btnClearKeyBinding
            // 
            this.btnClearKeyBinding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearKeyBinding.Location = new System.Drawing.Point(184, 65);
            this.btnClearKeyBinding.Name = "btnClearKeyBinding";
            this.btnClearKeyBinding.Size = new System.Drawing.Size(160, 23);
            this.btnClearKeyBinding.TabIndex = 16;
            this.btnClearKeyBinding.Text = "Clear";
            this.btnClearKeyBinding.UseVisualStyleBackColor = true;
            this.btnClearKeyBinding.Click += new System.EventHandler(this.btnClearKeyBinding_Click);
            // 
            // cmbxCommandWords
            // 
            this.cmbxCommandWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxCommandWords.FormattingEnabled = true;
            this.cmbxCommandWords.Location = new System.Drawing.Point(184, 41);
            this.cmbxCommandWords.Name = "cmbxCommandWords";
            this.cmbxCommandWords.Size = new System.Drawing.Size(160, 21);
            this.cmbxCommandWords.TabIndex = 20;
            // 
            // txtBindKey
            // 
            this.txtBindKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBindKey.Location = new System.Drawing.Point(9, 41);
            this.txtBindKey.Name = "txtBindKey";
            this.txtBindKey.Size = new System.Drawing.Size(160, 20);
            this.txtBindKey.TabIndex = 21;
            this.txtBindKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBindKey_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(181, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Voice Command:";
            // 
            // gbAddProgramCmd
            // 
            this.gbAddProgramCmd.Controls.Add(this.label4);
            this.gbAddProgramCmd.Controls.Add(this.btnAddOpenProgramCommand);
            this.gbAddProgramCmd.Controls.Add(this.btnClearOpenProgram);
            this.gbAddProgramCmd.Controls.Add(this.txtOpenProgramCommandWord);
            this.gbAddProgramCmd.Controls.Add(this.txtExecutablePath);
            this.gbAddProgramCmd.Controls.Add(this.btnOpenProgram);
            this.gbAddProgramCmd.Controls.Add(this.label2);
            this.gbAddProgramCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddProgramCmd.Location = new System.Drawing.Point(391, 460);
            this.gbAddProgramCmd.Name = "gbAddProgramCmd";
            this.gbAddProgramCmd.Size = new System.Drawing.Size(350, 94);
            this.gbAddProgramCmd.TabIndex = 37;
            this.gbAddProgramCmd.TabStop = false;
            this.gbAddProgramCmd.Text = "Add Open Program Command";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(181, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Program Path:";
            // 
            // btnAddOpenProgramCommand
            // 
            this.btnAddOpenProgramCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOpenProgramCommand.Location = new System.Drawing.Point(6, 67);
            this.btnAddOpenProgramCommand.Name = "btnAddOpenProgramCommand";
            this.btnAddOpenProgramCommand.Size = new System.Drawing.Size(160, 23);
            this.btnAddOpenProgramCommand.TabIndex = 12;
            this.btnAddOpenProgramCommand.Text = "Add";
            this.btnAddOpenProgramCommand.UseVisualStyleBackColor = true;
            this.btnAddOpenProgramCommand.Click += new System.EventHandler(this.btnAddOpenProgramCommand_Click);
            // 
            // btnClearOpenProgram
            // 
            this.btnClearOpenProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearOpenProgram.Location = new System.Drawing.Point(184, 65);
            this.btnClearOpenProgram.Name = "btnClearOpenProgram";
            this.btnClearOpenProgram.Size = new System.Drawing.Size(160, 23);
            this.btnClearOpenProgram.TabIndex = 15;
            this.btnClearOpenProgram.Text = "Clear";
            this.btnClearOpenProgram.UseVisualStyleBackColor = true;
            this.btnClearOpenProgram.Click += new System.EventHandler(this.btnClearOpenProgram_Click);
            // 
            // txtOpenProgramCommandWord
            // 
            this.txtOpenProgramCommandWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenProgramCommandWord.Location = new System.Drawing.Point(6, 41);
            this.txtOpenProgramCommandWord.Name = "txtOpenProgramCommandWord";
            this.txtOpenProgramCommandWord.Size = new System.Drawing.Size(160, 20);
            this.txtOpenProgramCommandWord.TabIndex = 17;
            // 
            // txtExecutablePath
            // 
            this.txtExecutablePath.Enabled = false;
            this.txtExecutablePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExecutablePath.Location = new System.Drawing.Point(184, 41);
            this.txtExecutablePath.Name = "txtExecutablePath";
            this.txtExecutablePath.Size = new System.Drawing.Size(133, 20);
            this.txtExecutablePath.TabIndex = 18;
            // 
            // btnOpenProgram
            // 
            this.btnOpenProgram.Location = new System.Drawing.Point(317, 39);
            this.btnOpenProgram.Name = "btnOpenProgram";
            this.btnOpenProgram.Size = new System.Drawing.Size(27, 23);
            this.btnOpenProgram.TabIndex = 19;
            this.btnOpenProgram.Text = "...";
            this.btnOpenProgram.UseVisualStyleBackColor = true;
            this.btnOpenProgram.Click += new System.EventHandler(this.btnOpenProgram_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Command Word:";
            // 
            // gbAddVoiceCmd
            // 
            this.gbAddVoiceCmd.Controls.Add(this.label1);
            this.gbAddVoiceCmd.Controls.Add(this.btnAddVoiceCommand);
            this.gbAddVoiceCmd.Controls.Add(this.btnClearVoiceCommand);
            this.gbAddVoiceCmd.Controls.Add(this.txtVoiceCommandWord);
            this.gbAddVoiceCmd.Controls.Add(this.txtVoiceCommandKeys);
            this.gbAddVoiceCmd.Controls.Add(this.label5);
            this.gbAddVoiceCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddVoiceCmd.Location = new System.Drawing.Point(7, 460);
            this.gbAddVoiceCmd.Name = "gbAddVoiceCmd";
            this.gbAddVoiceCmd.Size = new System.Drawing.Size(349, 94);
            this.gbAddVoiceCmd.TabIndex = 36;
            this.gbAddVoiceCmd.TabStop = false;
            this.gbAddVoiceCmd.Text = "Add Voice Command";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Command Word:";
            // 
            // btnAddVoiceCommand
            // 
            this.btnAddVoiceCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVoiceCommand.Location = new System.Drawing.Point(6, 65);
            this.btnAddVoiceCommand.Name = "btnAddVoiceCommand";
            this.btnAddVoiceCommand.Size = new System.Drawing.Size(160, 23);
            this.btnAddVoiceCommand.TabIndex = 11;
            this.btnAddVoiceCommand.Text = "Add";
            this.btnAddVoiceCommand.UseVisualStyleBackColor = true;
            this.btnAddVoiceCommand.Click += new System.EventHandler(this.btnAddVoiceCommand_Click);
            // 
            // btnClearVoiceCommand
            // 
            this.btnClearVoiceCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearVoiceCommand.Location = new System.Drawing.Point(183, 65);
            this.btnClearVoiceCommand.Name = "btnClearVoiceCommand";
            this.btnClearVoiceCommand.Size = new System.Drawing.Size(160, 23);
            this.btnClearVoiceCommand.TabIndex = 14;
            this.btnClearVoiceCommand.Text = "Clear";
            this.btnClearVoiceCommand.UseVisualStyleBackColor = true;
            this.btnClearVoiceCommand.Click += new System.EventHandler(this.btnClearVoiceCommand_Click);
            // 
            // txtVoiceCommandWord
            // 
            this.txtVoiceCommandWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoiceCommandWord.Location = new System.Drawing.Point(6, 38);
            this.txtVoiceCommandWord.Name = "txtVoiceCommandWord";
            this.txtVoiceCommandWord.Size = new System.Drawing.Size(160, 20);
            this.txtVoiceCommandWord.TabIndex = 22;
            // 
            // txtVoiceCommandKeys
            // 
            this.txtVoiceCommandKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoiceCommandKeys.Location = new System.Drawing.Point(183, 39);
            this.txtVoiceCommandKeys.Name = "txtVoiceCommandKeys";
            this.txtVoiceCommandKeys.Size = new System.Drawing.Size(160, 20);
            this.txtVoiceCommandKeys.TabIndex = 23;
            this.txtVoiceCommandKeys.TextChanged += new System.EventHandler(this.txtVoiceCommandKeys_TextChanged);
            this.txtVoiceCommandKeys.Enter += new System.EventHandler(this.commandKeysFeildFocusChanged);
            this.txtVoiceCommandKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVoiceCommandKeys_KeyUp);
            this.txtVoiceCommandKeys.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtVoiceCommandKeys_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(180, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Keys:";
            // 
            // btnDeleteSelectedKeyBinding
            // 
            this.btnDeleteSelectedKeyBinding.Location = new System.Drawing.Point(773, 430);
            this.btnDeleteSelectedKeyBinding.Name = "btnDeleteSelectedKeyBinding";
            this.btnDeleteSelectedKeyBinding.Size = new System.Drawing.Size(352, 23);
            this.btnDeleteSelectedKeyBinding.TabIndex = 35;
            this.btnDeleteSelectedKeyBinding.Text = "Delete Selected";
            this.btnDeleteSelectedKeyBinding.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedKeyBinding.Click += new System.EventHandler(this.btnDeleteSelectedKeyBinding_Click);
            // 
            // btnDeleteSelectedOpenProgramCommand
            // 
            this.btnDeleteSelectedOpenProgramCommand.Location = new System.Drawing.Point(391, 430);
            this.btnDeleteSelectedOpenProgramCommand.Name = "btnDeleteSelectedOpenProgramCommand";
            this.btnDeleteSelectedOpenProgramCommand.Size = new System.Drawing.Size(352, 23);
            this.btnDeleteSelectedOpenProgramCommand.TabIndex = 34;
            this.btnDeleteSelectedOpenProgramCommand.Text = "Delete Selected";
            this.btnDeleteSelectedOpenProgramCommand.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedOpenProgramCommand.Click += new System.EventHandler(this.btnDeleteSelectedOpenProgramCommand_Click);
            // 
            // btnDeleteSelectedVoiceCommands
            // 
            this.btnDeleteSelectedVoiceCommands.Location = new System.Drawing.Point(4, 430);
            this.btnDeleteSelectedVoiceCommands.Name = "btnDeleteSelectedVoiceCommands";
            this.btnDeleteSelectedVoiceCommands.Size = new System.Drawing.Size(352, 23);
            this.btnDeleteSelectedVoiceCommands.TabIndex = 33;
            this.btnDeleteSelectedVoiceCommands.Text = "Delete Selected";
            this.btnDeleteSelectedVoiceCommands.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedVoiceCommands.Click += new System.EventHandler(this.btnDeleteSelectedVoiceCommands_Click);
            // 
            // dgvKeyBinding
            // 
            this.dgvKeyBinding.AllowUserToAddRows = false;
            this.dgvKeyBinding.AllowUserToDeleteRows = false;
            this.dgvKeyBinding.AllowUserToResizeRows = false;
            this.dgvKeyBinding.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeyBinding.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKeyBind,
            this.colVoiceCommand});
            this.dgvKeyBinding.Location = new System.Drawing.Point(775, 39);
            this.dgvKeyBinding.Name = "dgvKeyBinding";
            this.dgvKeyBinding.ReadOnly = true;
            this.dgvKeyBinding.RowHeadersVisible = false;
            this.dgvKeyBinding.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeyBinding.Size = new System.Drawing.Size(350, 384);
            this.dgvKeyBinding.TabIndex = 32;
            // 
            // colKeyBind
            // 
            this.colKeyBind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKeyBind.HeaderText = "Key";
            this.colKeyBind.Name = "colKeyBind";
            this.colKeyBind.ReadOnly = true;
            // 
            // colVoiceCommand
            // 
            this.colVoiceCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colVoiceCommand.HeaderText = "Voice Command";
            this.colVoiceCommand.Name = "colVoiceCommand";
            this.colVoiceCommand.ReadOnly = true;
            // 
            // dgvOpenProgram
            // 
            this.dgvOpenProgram.AllowUserToAddRows = false;
            this.dgvOpenProgram.AllowUserToDeleteRows = false;
            this.dgvOpenProgram.AllowUserToResizeRows = false;
            this.dgvOpenProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProgramCommandWord,
            this.colProgramPath});
            this.dgvOpenProgram.Location = new System.Drawing.Point(391, 39);
            this.dgvOpenProgram.Name = "dgvOpenProgram";
            this.dgvOpenProgram.ReadOnly = true;
            this.dgvOpenProgram.RowHeadersVisible = false;
            this.dgvOpenProgram.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOpenProgram.Size = new System.Drawing.Size(350, 384);
            this.dgvOpenProgram.TabIndex = 31;
            // 
            // colProgramCommandWord
            // 
            this.colProgramCommandWord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProgramCommandWord.HeaderText = "Command Word";
            this.colProgramCommandWord.Name = "colProgramCommandWord";
            this.colProgramCommandWord.ReadOnly = true;
            // 
            // colProgramPath
            // 
            this.colProgramPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProgramPath.HeaderText = "Program Path";
            this.colProgramPath.Name = "colProgramPath";
            this.colProgramPath.ReadOnly = true;
            // 
            // dgvVoiceCommands
            // 
            this.dgvVoiceCommands.AllowUserToAddRows = false;
            this.dgvVoiceCommands.AllowUserToDeleteRows = false;
            this.dgvVoiceCommands.AllowUserToResizeRows = false;
            this.dgvVoiceCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoiceCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCommandWord,
            this.colKeys});
            this.dgvVoiceCommands.Location = new System.Drawing.Point(4, 39);
            this.dgvVoiceCommands.MultiSelect = false;
            this.dgvVoiceCommands.Name = "dgvVoiceCommands";
            this.dgvVoiceCommands.ReadOnly = true;
            this.dgvVoiceCommands.RowHeadersVisible = false;
            this.dgvVoiceCommands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoiceCommands.Size = new System.Drawing.Size(350, 384);
            this.dgvVoiceCommands.TabIndex = 30;
            // 
            // colCommandWord
            // 
            this.colCommandWord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCommandWord.HeaderText = "Command Word";
            this.colCommandWord.Name = "colCommandWord";
            this.colCommandWord.ReadOnly = true;
            // 
            // colKeys
            // 
            this.colKeys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKeys.HeaderText = "Keys";
            this.colKeys.Name = "colKeys";
            this.colKeys.ReadOnly = true;
            // 
            // lblKeyBindingTitle
            // 
            this.lblKeyBindingTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyBindingTitle.Location = new System.Drawing.Point(775, 5);
            this.lblKeyBindingTitle.Name = "lblKeyBindingTitle";
            this.lblKeyBindingTitle.Size = new System.Drawing.Size(350, 31);
            this.lblKeyBindingTitle.TabIndex = 10;
            this.lblKeyBindingTitle.Text = "Hotkey Commands";
            this.lblKeyBindingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOpenProgramTitle
            // 
            this.lblOpenProgramTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenProgramTitle.Location = new System.Drawing.Point(391, 5);
            this.lblOpenProgramTitle.Name = "lblOpenProgramTitle";
            this.lblOpenProgramTitle.Size = new System.Drawing.Size(350, 31);
            this.lblOpenProgramTitle.TabIndex = 9;
            this.lblOpenProgramTitle.Text = "Open Program Commands";
            this.lblOpenProgramTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVoiceCommandTitle
            // 
            this.lblVoiceCommandTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoiceCommandTitle.Location = new System.Drawing.Point(7, 5);
            this.lblVoiceCommandTitle.Name = "lblVoiceCommandTitle";
            this.lblVoiceCommandTitle.Size = new System.Drawing.Size(349, 31);
            this.lblVoiceCommandTitle.TabIndex = 8;
            this.lblVoiceCommandTitle.Text = "Voice Commands";
            this.lblVoiceCommandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(1045, 601);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(106, 23);
            this.btnSaveChanges.TabIndex = 2;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Dragon exe File address:";
            // 
            // txtbxDragonFile
            // 
            this.txtbxDragonFile.Enabled = false;
            this.txtbxDragonFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbxDragonFile.Location = new System.Drawing.Point(23, 169);
            this.txtbxDragonFile.Name = "txtbxDragonFile";
            this.txtbxDragonFile.Size = new System.Drawing.Size(133, 20);
            this.txtbxDragonFile.TabIndex = 20;
            // 
            // btnDragonFile
            // 
            this.btnDragonFile.Location = new System.Drawing.Point(156, 167);
            this.btnDragonFile.Name = "btnDragonFile";
            this.btnDragonFile.Size = new System.Drawing.Size(27, 23);
            this.btnDragonFile.TabIndex = 21;
            this.btnDragonFile.Text = "...";
            this.btnDragonFile.UseVisualStyleBackColor = true;
            this.btnDragonFile.Click += new System.EventHandler(this.btnDragonFile_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 632);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Vox Viso Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFormHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFormWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbrMagnificationAmount)).EndInit();
            this.tabEyeTracking.ResumeLayout(false);
            this.tabEyeTracking.PerformLayout();
            this.tabVoiceRecognition.ResumeLayout(false);
            this.tabVoiceRecognition.PerformLayout();
            this.grpbxVoiceOption.ResumeLayout(false);
            this.grpbxVoiceOption.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.gbAddHotkeyCmd.ResumeLayout(false);
            this.gbAddHotkeyCmd.PerformLayout();
            this.gbAddProgramCmd.ResumeLayout(false);
            this.gbAddProgramCmd.PerformLayout();
            this.gbAddVoiceCmd.ResumeLayout(false);
            this.gbAddVoiceCmd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyBinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenProgram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoiceCommands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabGeneral;
        private TabPage tabEyeTracking;
        private TabPage tabVoiceRecognition;
        private TabPage tabPage1;
        private Label lblKeyBindingTitle;
        private Label lblOpenProgramTitle;
        private Label lblVoiceCommandTitle;
        private Button btnClearKeyBinding;
        private Button btnClearOpenProgram;
        private Button btnClearVoiceCommand;
        private Button btnAddKeyBinding;
        private Button btnAddOpenProgramCommand;
        private Button btnAddVoiceCommand;
        private TextBox txtExecutablePath;
        private TextBox txtOpenProgramCommandWord;
        private Button btnOpenProgram;
        private ComboBox cmbxCommandWords;
        private TextBox txtBindKey;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtVoiceCommandKeys;
        private TextBox txtVoiceCommandWord;
        private Button btnDeleteSelectedKeyBinding;
        private Button btnDeleteSelectedOpenProgramCommand;
        private Button btnDeleteSelectedVoiceCommands;
        private DataGridView dgvKeyBinding;
        private DataGridView dgvOpenProgram;
        private DataGridView dgvVoiceCommands;
        private DataGridViewTextBoxColumn colCommandWord;
        private DataGridViewTextBoxColumn colKeys;
        private DataGridViewTextBoxColumn colKeyBind;
        private DataGridViewTextBoxColumn colVoiceCommand;
        private DataGridViewTextBoxColumn colProgramCommandWord;
        private DataGridViewTextBoxColumn colProgramPath;
        private GroupBox gbAddHotkeyCmd;
        private GroupBox gbAddProgramCmd;
        private GroupBox gbAddVoiceCmd;
        private GroupBox groupBox1;
        private Label label7;
        private TrackBar trkbrMagnificationAmount;
        private CheckBox chkbxZoomEnabled;
        private NumericUpDown udFormHeight;
        private NumericUpDown udFormWidth;
        private Label label9;
        private Label label8;
        private CheckBox chkbxDebugEyeTracking;
        private CheckBox btnVisualiseFixations;
        private Button btnCalibrate;
        private Button btnSaveChanges;
        private GroupBox grpbxVoiceOption;
        private RadioButton rbWindowsVoice;
        private RadioButton rbDragon;
        private TextBox txtbxDragonFile;
        private Button btnDragonFile;
        private Label label10;
    }
}