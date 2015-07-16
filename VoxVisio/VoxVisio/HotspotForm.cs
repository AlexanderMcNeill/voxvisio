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
        private const int GROWSPEED = 10;
        private const int SHRINKSPEED = 5;

        private int percentFill = 0;
        private Rectangle hotspotBounds;
        private Graphics canvas;
        private Graphics bufferGraphics;
        private Bitmap buffer;
        public HotspotForm()
        {
            InitializeComponent();

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width, Screen.PrimaryScreen.Bounds.Height - Height);
            TopMost = true;
            hotspotBounds = new Rectangle(new Point(0, 0), new Size(Width, Height));

            canvas = CreateGraphics();
            buffer = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(buffer);
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawHotspot();
        }
        public void updateHotspot(Point fixationLocation)
        {
            Point relativeLocation = new Point(fixationLocation.X - Left, fixationLocation.Y - Top);
            if (hotspotBounds.Contains(relativeLocation))
            {
                if(percentFill < 100)
                {
                    percentFill += GROWSPEED;
                }
                    
            }
            else
            {
                if(percentFill > 0)
                {
                    percentFill -= SHRINKSPEED;
                }
                    
            }

            drawHotspot();
        }

        public void drawHotspot()
        {
            bufferGraphics.FillRectangle(Brushes.LightCoral, 0, 0, Width, Height);
            bufferGraphics.FillEllipse(Brushes.Blue, hotspotBounds);
            int fillWidth = (Width / 100) * percentFill;
            int fillHeight = (Height / 100) * percentFill;
            int xPos = (Width - fillWidth) / 2;
            int yPos = (Height - fillHeight) / 2;
            bufferGraphics.FillEllipse(Brushes.Red, xPos, yPos, fillWidth, fillHeight);
            canvas.DrawImage(buffer, 0, 0);
        }

        public int PercentFill
        {
            get { return percentFill; }
            set { percentFill = value; }
        }

        public void requestClose()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // close the form on the forms thread
                this.Close();
            });
        }
    }
}
