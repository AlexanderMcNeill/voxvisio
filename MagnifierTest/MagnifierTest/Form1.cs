using System;
using System.Drawing;
using System.Windows.Forms;
using Karna.Magnification;

namespace MagnifierForm
{
    public partial class Form1 : Form
    {
        private Magnifier mag;

        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            mag = new Magnifier(this);
            //mag.baseMagnification = (float)1.5;
            mag.MagnifyCenter = new Point((Left + (Width / 2)), (Top + (Height / 2)));
            timer1.Enabled = true;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //mag.SetWindowPos(MousePosition);
            // mag.MagnifyCenter = MousePosition;
            //mag.UpdateMaginifier();
            mag.startZooming();
            //timer1.Enabled = !timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mag.UpdateMaginifier();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mag.currentMousePos = e.Location;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           mag.Dispose();
        }
    }
}
