namespace VoxVisio
{
    partial class ToastForm
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
            this.toastTimer = new System.Windows.Forms.Timer(this.components);
            this.lblToast = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toastTimer
            // 
            this.toastTimer.Tick += new System.EventHandler(this.toastTimer_Tick);
            // 
            // lblToast
            // 
            this.lblToast.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblToast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToast.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToast.Location = new System.Drawing.Point(0, 0);
            this.lblToast.Name = "lblToast";
            this.lblToast.Size = new System.Drawing.Size(284, 62);
            this.lblToast.TabIndex = 0;
            this.lblToast.Text = "Toast Text";
            this.lblToast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ToastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(284, 62);
            this.Controls.Add(this.lblToast);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToastForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ToastForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer toastTimer;
        private System.Windows.Forms.Label lblToast;
    }
}