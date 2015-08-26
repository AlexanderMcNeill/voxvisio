using System.ComponentModel;

namespace VoxVisio
{
    partial class VoxVisio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoxVisio));
            this.lvCommandList = new System.Windows.Forms.ListView();
            this.CommandWord = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HotKeyCombo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvCommandList
            // 
            this.lvCommandList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CommandWord,
            this.HotKeyCombo});
            this.lvCommandList.GridLines = true;
            this.lvCommandList.Location = new System.Drawing.Point(856, 12);
            this.lvCommandList.Name = "lvCommandList";
            this.lvCommandList.Size = new System.Drawing.Size(293, 549);
            this.lvCommandList.TabIndex = 0;
            this.lvCommandList.UseCompatibleStateImageBehavior = false;
            this.lvCommandList.View = System.Windows.Forms.View.Details;
            // 
            // CommandWord
            // 
            this.CommandWord.Tag = "1";
            this.CommandWord.Text = "Command Word";
            this.CommandWord.Width = 160;
            // 
            // HotKeyCombo
            // 
            this.HotKeyCombo.Tag = "1";
            this.HotKeyCombo.Text = "Hot Key Combo";
            this.HotKeyCombo.Width = 129;
            // 
            // VoxVisio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 573);
            this.Controls.Add(this.lvCommandList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VoxVisio";
            this.Text = "VoxVisio";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VoxVisio_FormClosing);
            //this.Load += new System.EventHandler(this.VoxVisio_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCommandList;
        private System.Windows.Forms.ColumnHeader CommandWord;
        private System.Windows.Forms.ColumnHeader HotKeyCombo;
    }
}

