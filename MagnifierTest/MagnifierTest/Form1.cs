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
            mag.Magnification = (float)1.5;
            mag.UpdateMaginifier();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = !timer1.Enabled;
            mag.WindowCenter = MousePosition;
            mag.UpdateMaginifier();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mag.Magnification = mag.Magnification * (float)1.1;
        }
    }
}
