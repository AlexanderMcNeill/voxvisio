using System.ComponentModel;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.lvCommandList = new System.Windows.Forms.ListView();
            this.Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvCommandList
            // 
            this.lvCommandList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Command,
            this.Action});
            this.lvCommandList.GridLines = true;
            this.lvCommandList.Location = new System.Drawing.Point(12, 70);
            this.lvCommandList.Name = "lvCommandList";
            this.lvCommandList.Size = new System.Drawing.Size(1056, 505);
            this.lvCommandList.TabIndex = 0;
            this.lvCommandList.UseCompatibleStateImageBehavior = false;
            // 
            // VoiceCommand
            // 
            this.Command.DisplayIndex = 1;
            this.Command.Tag = "1";
            this.Command.Text = "VoiceCommand";
            // 
            // Action
            // 
            this.Action.DisplayIndex = 0;
            this.Action.Tag = "1";
            this.Action.Text = "VoiceCommand";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1059, 58);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 587);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvCommandList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.Text = "Help";
            this.ResumeLayout(false);

        }

        #endregion

        private ListView lvCommandList;
        private Label label1;
        private ColumnHeader Command;
        private ColumnHeader Action;
    }
}