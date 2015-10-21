using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.Screen_Overlay
{
    class FixationDot:Overlay
    {
        private const int DOTSIZE = 30;
        private Point currentFixationLocation;

        public FixationDot()
        {
            currentFixationLocation = new Point(0, 0);
            EventSingleton.Instance().fixationEvent += fixationEvent;
        }

        private void fixationEvent(Fixation newFixation)
        {
            currentFixationLocation = newFixation.GetFixationLocation();
        }

        public void Draw(System.Drawing.Graphics g)
        {
            if (Screen.PrimaryScreen.Bounds.Contains(currentFixationLocation))
            {
                int xPos = currentFixationLocation.X - DOTSIZE / 2;
                int yPos = currentFixationLocation.Y - DOTSIZE / 2;
                g.FillEllipse(Brushes.Gray, xPos, yPos, DOTSIZE, DOTSIZE);
            }
        }
    }
}
