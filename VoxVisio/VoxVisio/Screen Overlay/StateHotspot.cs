using System;
using System.Drawing;

namespace VoxVisio.Screen_Overlay
{
    public class StateHotspot
    {
        private Image activeImage;
        private Image inactiveImage;
        private string name;
        private Action callback;
        private Rectangle hotspotRect;
        private int counter = 0;
        public bool selected = false;
        private bool focused = false;
        private SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.Red));

        public StateHotspot(string name, Action callback, Rectangle hotspotRect, bool selected, Image activeImage, Image inactiveImage)
        {
            this.name = name;
            this.selected = selected;
            this.callback = callback;
            this.hotspotRect = hotspotRect;
            this.activeImage = activeImage;
            this.inactiveImage = inactiveImage;
        }

        public void Draw(Graphics g)
        {
            if (selected)
            {
               g.DrawImage(activeImage, hotspotRect);
            }
            else
            {
                g.DrawImage(inactiveImage, hotspotRect);

                //Getting how big the progress circle will be
                int fillWidth = (hotspotRect.Width / 100) * counter;
                int fillHeight = (hotspotRect.Width / 100) * counter;

                //Make the progress circle centered to the hotspot rectangle
                int xPos = (hotspotRect.Width - fillWidth) / 2 + hotspotRect.X;
                int yPos = (hotspotRect.Width - fillHeight) / 2 + hotspotRect.Y;

                g.FillEllipse(brush, xPos, yPos, fillWidth, fillHeight);
                
            }
        }

        public void Update()
        {
            if(!selected)
            {
                if (focused)
                {
                    if (counter < 100)
                    {
                        counter += 10;
                    }
                    else
                    {
                        counter = 0;
                        selected = true;
                        callback();
                    }
                }
                else if (counter > 0)
                {
                    counter -= 10;
                }
            }
        }

        public void Fixation(Point fixation)
        {
            focused = hotspotRect.Contains(fixation);
        }
    }
}
