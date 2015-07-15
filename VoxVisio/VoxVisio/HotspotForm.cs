using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoxVisio
{
    public partial class HotspotForm : Form
    {

        private int percentFill = 0;
        private Rectangle hotspotBounds;
        private Graphics canvas;
        public HotspotForm()
        {
            InitializeComponent();

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, Screen.PrimaryScreen.Bounds.Height - Height);
            TopMost = true;
            hotspotBounds = new Rectangle(new Point(0, 0), new Size(Width, Height));

            canvas = CreateGraphics();
        }

        public void updateHotspot(Point fixationLocation)
        {
            if (hotspotBounds.Contains(fixationLocation))
            {
                percentFill++;
            }
            else
            {
                percentFill--;
            }

            drawHotspot();
        }

        public void drawHotspot()
        {
            canvas.FillEllipse(Brushes.AliceBlue, hotspotBounds);
            int fillWidth = (Width / 100) * percentFill;
            int fillHeight = (Height / 100) * percentFill;
            canvas.FillEllipse(Brushes.Red,0,0,fillWidth, fillHeight);
        }

        public int PercentFill
        {
            get { return percentFill; }
            set { percentFill = value; }
        }
    }
}
