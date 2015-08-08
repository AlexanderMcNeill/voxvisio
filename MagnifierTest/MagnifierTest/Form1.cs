using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Karna.Magnification;

namespace MagnifierTest
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
           
            mag.UpdateMaginifier();
            timer1.Enabled = true;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //mag.SetWindowPos(MousePosition);
            mag.MagnifyCenter = MousePosition;
            mag.UpdateMaginifier();
            mag.setMagnifyWindowPos(MousePosition);
            //timer1.Enabled = !timer1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //mag.baseMagnification = mag.baseMagnification * (float)1.1;
            mag.UpdateMaginifier();
        }

    }
}
