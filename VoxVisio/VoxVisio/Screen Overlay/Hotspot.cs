using System;
using System.Drawing;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.Screen_Overlay
{
    class Hotspot : Overlay
    {
        protected Rectangle hotspotRect;
        protected bool hasFocus = false;
        protected int percentFill = 0;
        protected Action callback;
        protected OverlayForm overlayForm;
        protected bool finished = false;
        protected Timer updateTimer;

        public Hotspot(Rectangle hotspotRect, Action callback)
        {
            updateTimer = EventSingleton.Instance().updateTimer;
            updateTimer.Tick += updateTimer_Tick;
            overlayForm = SharedFormsSingleton.Instance().overlayForm;
            overlayForm.RegisterOverlay(this);

            this.hotspotRect = hotspotRect;
            this.callback = callback;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            RunHotspot();
        }

        public void RunHotspot()
        {
            if (hasFocus)
            {
                percentFill += 10;
            }
            else
            {
                if(percentFill > 0)
                {
                    percentFill -= 10;
                }
                
            }

            if (percentFill >= 100 && !finished)
            {
                finished = true;
                updateTimer.Tick -= updateTimer_Tick;
                overlayForm.RemoveOverlay(this);
                callback();
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
