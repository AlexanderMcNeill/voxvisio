using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.Screen_Overlay
{
    class Hotspot : Overlay
    {
        private Rectangle hotspotRect;
        private bool hasFocus = false;
        private int percentFill = 0;
        public Hotspot(Rectangle hotspotRect)
        {
            SharedDataSingleton sharedData = SharedDataSingleton.Instance();
            sharedData.updateTimer.Tick += updateTimer_Tick;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            RunHotspot();
        }

        public void RunHotspot()
        {
            if (hasFocus)
            {
                percentFill++;
            }
            else
            {
                percentFill--;
            }

            if (percentFill >= 100)
            {

            }
        }

        public void update(Point fixation)
        {
            if (hotspotRect.Contains(fixation))
            {
                hasFocus = true;
            }
            else
            {
                hasFocus = false;
            }
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, hotspotRect);

            //Getting how big the progress circle will be
            int fillWidth = (hotspotRect.Width / 100) * percentFill;
            int fillHeight = (hotspotRect.Width / 100) * percentFill;

            //Make the progress circle centered to the hotspot rectangle
            int xPos = (hotspotRect.Width - fillWidth) / 2;
            int yPos = (hotspotRect.Width - fillHeight) / 2;
            g.FillEllipse(Brushes.Red, xPos, yPos, fillWidth, fillHeight);
        }
    }
}
